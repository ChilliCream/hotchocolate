using System;
using System.Collections.Generic;
using System.Xml;
using HotChocolate.Language;
using HotChocolate.Properties;

#nullable enable

namespace HotChocolate.Types
{
    /// <summary>
    /// The TimeSpan scalar type represented in two formats:
    /// <see cref="TimeSpanFormat.Iso8601"/> and <see cref="TimeSpanFormat.DotNet"/>
    /// </summary>
    public sealed class TimeSpanType
        : ScalarType<TimeSpan, StringValueNode>
    {
        private static readonly Dictionary<TimeSpanFormat, Func<TimeSpan, string>> _timeSpanToString =
            new Dictionary<TimeSpanFormat, Func<TimeSpan, string>>
            {
                {TimeSpanFormat.Iso8601, timeSpan => XmlConvert.ToString(timeSpan)},
                {TimeSpanFormat.DotNet, timeSpan => timeSpan.ToString("c")}
            };

        private readonly TimeSpanFormat _format;

        public TimeSpanType()
            : this(TimeSpanFormat.Iso8601)
        {
        }

        public TimeSpanType(TimeSpanFormat format)
            : this(ScalarNames.TimeSpan, format)
        {
            Description = TypeResources.TimeSpanType_Description;
        }

        public TimeSpanType(NameString name, TimeSpanFormat format)
            : this(name, null, format)
        {
        }

        public TimeSpanType(NameString name, string? description, TimeSpanFormat format)
            : base(name, BindingBehavior.Implicit)
        {
            _format = format;
            Description = description;
        }

        protected override TimeSpan ParseLiteral(StringValueNode literal)
        {
            if (TryDeserializeFromString(literal.Value, _format, out TimeSpan? value) &&
                value != null)
            {
                return value.Value;
            }

            throw new ScalarSerializationException(
                TypeResourceHelper.Scalar_Cannot_ParseLiteral(
                    Name, literal.GetType()));
        }

        protected override StringValueNode ParseValue(TimeSpan value)
        {
            return new StringValueNode(_timeSpanToString[_format](value));
        }

        public override bool TrySerialize(object value, out object? serialized)
        {
            if (value is null)
            {
                serialized = null;
                return true;
            }

            if (value is TimeSpan timeSpan)
            {
                serialized = _timeSpanToString[_format](timeSpan);
                return true;
            }

            serialized = null;
            return false;
        }

        public override bool TryDeserialize(object serialized, out object? value)
        {
            if (serialized is null)
            {
                value = null;
                return true;
            }

            if (serialized is string s &&
                TryDeserializeFromString(s, _format, out TimeSpan? timeSpan))
            {
                value = timeSpan;
                return true;
            }

            if (serialized is TimeSpan ts)
            {
                value = ts;
                return true;
            }

            value = null;
            return false;
        }

        private static bool TryDeserializeFromString(
            string serialized,
            TimeSpanFormat format,
            out TimeSpan? value)
        {
            if (format == TimeSpanFormat.Iso8601)
            {
                return TryDeserializeIso8601(serialized, out value);
            }
            else
            {
                return TryDeserializeDotNet(serialized, out value);
            }
        }

        private static bool TryDeserializeIso8601(string serialized, out TimeSpan? value)
        {
            try
            {
                return Iso8601Duration.TryParse(serialized, out value);
            }
            catch (FormatException)
            {
                value = null;
                return false;
            }
        }

        private static bool TryDeserializeDotNet(string serialized, out TimeSpan? value)
        {
            if (TimeSpan.TryParse(serialized, out TimeSpan timeSpan))
            {
                value = timeSpan;
                return true;
            }

            value = null;
            return false;
        }
    }
}
