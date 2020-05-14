using System.Collections.Generic;
using HotChocolate.Configuration;
using HotChocolate.Language;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors.Definitions;
using NetTopologySuite.Geometries;

namespace Types.Spatial
{
    public class GeoJSONMultiLineStringInput : InputObjectType<MultiLineString>
    {
        private const string _typeFieldName = "type";
        private const string _coordinatesFieldName = "coordinates";
        private IInputField _typeField = default!;
        private IInputField _coordinatesField = default!;

        public GeoJSONMultiLineStringInput() { }

        protected override void Configure(IInputObjectTypeDescriptor<MultiLineString> descriptor)
        {
            descriptor.BindFieldsExplicitly();

            descriptor.Field(_typeFieldName).Type<EnumType<GeoJSONGeometryType>>();

            descriptor.Field(_coordinatesFieldName).Type<ListType<ListType<GeoJSONPositionScalar>>>();
        }

        public override object? ParseLiteral(IValueNode literal)
        {
            if (literal is NullValueNode)
            {
                return null;
            }

            if (!(literal is ObjectValueNode obj) || obj.Fields.Count < 2)
            {
                throw new InputObjectSerializationException(
                    "Failed to serialize MultiLineString. Needs at least type and coordinates " +
                    "fields");
            }

            List<List<Coordinate>>? parts = null;
            GeoJSONGeometryType? type = null;

            for (var i = 0; i < obj.Fields.Count; i++)
            {
                ObjectFieldNode field = obj.Fields[i];

                switch (field.Name.Value)
                {
                    case _coordinatesFieldName:
                        parts = (List<List<Coordinate>>)_coordinatesField.Type.ParseLiteral(field.Value);
                        break;
                    case _typeFieldName:
                        type = (GeoJSONGeometryType)_typeField.Type.ParseLiteral(field.Value);
                        break;
                }
            }

            if (type != GeoJSONGeometryType.MultiLineString || parts is null || parts.Count < 1)
            {
                throw new InputObjectSerializationException(
                    "Failed to serialize MultiLineString. You have to at least specify a type and" +
                    "coordinates array");
            }

            var geometries = new LineString[parts.Count];
            for (var i = 0; i < parts.Count; i++)
            {
                var coordinates = new Coordinate[parts[i].Count];
                for (var j = 0; j < parts[i].Count; j++) {
                    coordinates[j] = new Coordinate(parts[i][j]);
                }

                geometries[i] = new LineString(coordinates);
            }

            return new MultiLineString(geometries);
        }

        protected override void OnAfterCompleteType(
            ICompletionContext context,
            DefinitionBase definition,
            IDictionary<string, object?> contextData)
        {
            _coordinatesField = Fields[_coordinatesFieldName];
            _typeField = Fields[_typeFieldName];
        }
    }
}
