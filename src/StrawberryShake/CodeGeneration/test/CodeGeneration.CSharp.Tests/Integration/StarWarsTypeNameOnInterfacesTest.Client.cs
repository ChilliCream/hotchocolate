﻿// ReSharper disable BuiltInTypeReferenceStyle
// ReSharper disable RedundantNameQualifier
// ReSharper disable ArrangeObjectCreationWhenTypeEvident
// ReSharper disable UnusedType.Global
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedMethodReturnValue.Local
// ReSharper disable ConvertToAutoProperty
// ReSharper disable UnusedMember.Global
// ReSharper disable SuggestVarOrType_SimpleTypes
// ReSharper disable InconsistentNaming

// DroidEntity

// StrawberryShake.CodeGeneration.CSharp.Generators.EntityTypeGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class DroidEntity
    {
        public DroidEntity(global::System.String __typename)
        {
            this.__typename = __typename;
        }

        /// <summary>
        /// The name of the current Object type at runtime.
        /// </summary>
        public global::System.String __typename { get; }
    }
}


// HumanEntity

// StrawberryShake.CodeGeneration.CSharp.Generators.EntityTypeGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class HumanEntity
    {
        public HumanEntity(global::System.String __typename)
        {
            this.__typename = __typename;
        }

        /// <summary>
        /// The name of the current Object type at runtime.
        /// </summary>
        public global::System.String __typename { get; }
    }
}


// GetHeroResultFactory

// StrawberryShake.CodeGeneration.CSharp.Generators.ResultDataFactoryGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class GetHeroResultFactory
        : global::StrawberryShake.IOperationResultDataFactory<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.GetHeroResult>
    {
        private readonly global::StrawberryShake.IEntityStore _entityStore;
        private readonly global::StrawberryShake.IEntityMapper<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State.DroidEntity, GetHero_Hero_Droid> _getHero_Hero_DroidFromDroidEntityMapper;
        private readonly global::StrawberryShake.IEntityMapper<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State.HumanEntity, GetHero_Hero_Human> _getHero_Hero_HumanFromHumanEntityMapper;

        public GetHeroResultFactory(
            global::StrawberryShake.IEntityStore entityStore,
            global::StrawberryShake.IEntityMapper<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State.DroidEntity, GetHero_Hero_Droid> getHero_Hero_DroidFromDroidEntityMapper,
            global::StrawberryShake.IEntityMapper<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State.HumanEntity, GetHero_Hero_Human> getHero_Hero_HumanFromHumanEntityMapper)
        {
            _entityStore = entityStore
                 ?? throw new global::System.ArgumentNullException(nameof(entityStore));
            _getHero_Hero_DroidFromDroidEntityMapper = getHero_Hero_DroidFromDroidEntityMapper
                 ?? throw new global::System.ArgumentNullException(nameof(getHero_Hero_DroidFromDroidEntityMapper));
            _getHero_Hero_HumanFromHumanEntityMapper = getHero_Hero_HumanFromHumanEntityMapper
                 ?? throw new global::System.ArgumentNullException(nameof(getHero_Hero_HumanFromHumanEntityMapper));
        }

        global::System.Type global::StrawberryShake.IOperationResultDataFactory.ResultType => typeof(global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.IGetHeroResult);

        public GetHeroResult Create(
            global::StrawberryShake.IOperationResultDataInfo dataInfo,
            global::StrawberryShake.IEntityStoreSnapshot? snapshot = null)
        {
            if (snapshot is null)
            {
                snapshot = _entityStore.CurrentSnapshot;
            }

            if (dataInfo is GetHeroResultInfo info)
            {
                return new GetHeroResult(MapIGetHero_Hero(
                    info.Hero,
                    snapshot));
            }

            throw new global::System.ArgumentException("GetHeroResultInfo expected.");
        }

        private global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.IGetHero_Hero? MapIGetHero_Hero(
            global::StrawberryShake.EntityId? entityId,
            global::StrawberryShake.IEntityStoreSnapshot snapshot)
        {
            if (entityId is null)
            {
                return null;
            }


            if (entityId.Value.Name.Equals(
                    "Droid",
                    global::System.StringComparison.Ordinal))
            {
                return _getHero_Hero_DroidFromDroidEntityMapper.Map(
                    snapshot.GetEntity<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State.DroidEntity>(entityId.Value)
                        ?? throw new global::StrawberryShake.GraphQLClientException());
            }

            if (entityId.Value.Name.Equals(
                    "Human",
                    global::System.StringComparison.Ordinal))
            {
                return _getHero_Hero_HumanFromHumanEntityMapper.Map(
                    snapshot.GetEntity<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State.HumanEntity>(entityId.Value)
                        ?? throw new global::StrawberryShake.GraphQLClientException());
            }
            throw new global::System.NotSupportedException();
        }

        global::System.Object global::StrawberryShake.IOperationResultDataFactory.Create(
            global::StrawberryShake.IOperationResultDataInfo dataInfo,
            global::StrawberryShake.IEntityStoreSnapshot? snapshot)
        {
            return Create(
                dataInfo,
                snapshot);
        }
    }
}


// GetHeroResultInfo

// StrawberryShake.CodeGeneration.CSharp.Generators.ResultInfoGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class GetHeroResultInfo
        : global::StrawberryShake.IOperationResultDataInfo
    {
        private readonly global::System.Collections.Generic.IReadOnlyCollection<global::StrawberryShake.EntityId> _entityIds;
        private readonly global::System.UInt64 _version;

        public GetHeroResultInfo(
            global::StrawberryShake.EntityId? hero,
            global::System.Collections.Generic.IReadOnlyCollection<global::StrawberryShake.EntityId> entityIds,
            global::System.UInt64 version)
        {
            Hero = hero;
            _entityIds = entityIds
                 ?? throw new global::System.ArgumentNullException(nameof(entityIds));
            _version = version;
        }

        public global::StrawberryShake.EntityId? Hero { get; }

        public global::System.Collections.Generic.IReadOnlyCollection<global::StrawberryShake.EntityId> EntityIds => _entityIds;

        public global::System.UInt64 Version => _version;

        public global::StrawberryShake.IOperationResultDataInfo WithVersion(global::System.UInt64 version)
        {
            return new GetHeroResultInfo(
                Hero,
                _entityIds,
                version);
        }
    }
}


// GetHeroResult

// StrawberryShake.CodeGeneration.CSharp.Generators.ResultTypeGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class GetHeroResult
        : global::System.IEquatable<GetHeroResult>
        , IGetHeroResult
    {
        public GetHeroResult(global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.IGetHero_Hero? hero)
        {
            Hero = hero;
        }

        public global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.IGetHero_Hero? Hero { get; }

        public override global::System.Boolean Equals(global::System.Object? obj)
        {
            if (ReferenceEquals(
                    null,
                    obj))
            {
                return false;
            }

            if (ReferenceEquals(
                    this,
                    obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((GetHeroResult)obj);
        }

        public global::System.Boolean Equals(GetHeroResult? other)
        {
            if (ReferenceEquals(
                    null,
                    other))
            {
                return false;
            }

            if (ReferenceEquals(
                    this,
                    other))
            {
                return true;
            }

            if (other.GetType() != GetType())
            {
                return false;
            }

            return (((Hero is null && other.Hero is null) ||Hero != null && Hero.Equals(other.Hero)));
        }

        public override global::System.Int32 GetHashCode()
        {
            unchecked
            {
                int hash = 5;

                if (!(Hero is null))
                {
                    hash ^= 397 * Hero.GetHashCode();
                }

                return hash;
            }
        }
    }
}


// GetHero_Hero_DroidFromDroidEntityMapper

// StrawberryShake.CodeGeneration.CSharp.Generators.ResultFromEntityTypeMapperGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class GetHero_Hero_DroidFromDroidEntityMapper
        : global::StrawberryShake.IEntityMapper<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State.DroidEntity, GetHero_Hero_Droid>
    {
        private readonly global::StrawberryShake.IEntityStore _entityStore;

        public GetHero_Hero_DroidFromDroidEntityMapper(global::StrawberryShake.IEntityStore entityStore)
        {
            _entityStore = entityStore
                 ?? throw new global::System.ArgumentNullException(nameof(entityStore));
        }

        public GetHero_Hero_Droid Map(
            global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State.DroidEntity entity,
            global::StrawberryShake.IEntityStoreSnapshot? snapshot = null)
        {
            if (snapshot is null)
            {
                snapshot = _entityStore.CurrentSnapshot;
            }

            return new GetHero_Hero_Droid(entity.__typename);
        }
    }
}


// GetHero_Hero_Droid

// StrawberryShake.CodeGeneration.CSharp.Generators.ResultTypeGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class GetHero_Hero_Droid
        : global::System.IEquatable<GetHero_Hero_Droid>
        , IGetHero_Hero_Droid
    {
        public GetHero_Hero_Droid(global::System.String __typename)
        {
            this.__typename = __typename;
        }

        /// <summary>
        /// The name of the current Object type at runtime.
        /// </summary>
        public global::System.String __typename { get; }

        public override global::System.Boolean Equals(global::System.Object? obj)
        {
            if (ReferenceEquals(
                    null,
                    obj))
            {
                return false;
            }

            if (ReferenceEquals(
                    this,
                    obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((GetHero_Hero_Droid)obj);
        }

        public global::System.Boolean Equals(GetHero_Hero_Droid? other)
        {
            if (ReferenceEquals(
                    null,
                    other))
            {
                return false;
            }

            if (ReferenceEquals(
                    this,
                    other))
            {
                return true;
            }

            if (other.GetType() != GetType())
            {
                return false;
            }

            return (__typename.Equals(other.__typename));
        }

        public override global::System.Int32 GetHashCode()
        {
            unchecked
            {
                int hash = 5;

                hash ^= 397 * __typename.GetHashCode();

                return hash;
            }
        }
    }
}


// GetHero_Hero_HumanFromHumanEntityMapper

// StrawberryShake.CodeGeneration.CSharp.Generators.ResultFromEntityTypeMapperGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class GetHero_Hero_HumanFromHumanEntityMapper
        : global::StrawberryShake.IEntityMapper<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State.HumanEntity, GetHero_Hero_Human>
    {
        private readonly global::StrawberryShake.IEntityStore _entityStore;

        public GetHero_Hero_HumanFromHumanEntityMapper(global::StrawberryShake.IEntityStore entityStore)
        {
            _entityStore = entityStore
                 ?? throw new global::System.ArgumentNullException(nameof(entityStore));
        }

        public GetHero_Hero_Human Map(
            global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State.HumanEntity entity,
            global::StrawberryShake.IEntityStoreSnapshot? snapshot = null)
        {
            if (snapshot is null)
            {
                snapshot = _entityStore.CurrentSnapshot;
            }

            return new GetHero_Hero_Human(entity.__typename);
        }
    }
}


// GetHero_Hero_Human

// StrawberryShake.CodeGeneration.CSharp.Generators.ResultTypeGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class GetHero_Hero_Human
        : global::System.IEquatable<GetHero_Hero_Human>
        , IGetHero_Hero_Human
    {
        public GetHero_Hero_Human(global::System.String __typename)
        {
            this.__typename = __typename;
        }

        /// <summary>
        /// The name of the current Object type at runtime.
        /// </summary>
        public global::System.String __typename { get; }

        public override global::System.Boolean Equals(global::System.Object? obj)
        {
            if (ReferenceEquals(
                    null,
                    obj))
            {
                return false;
            }

            if (ReferenceEquals(
                    this,
                    obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((GetHero_Hero_Human)obj);
        }

        public global::System.Boolean Equals(GetHero_Hero_Human? other)
        {
            if (ReferenceEquals(
                    null,
                    other))
            {
                return false;
            }

            if (ReferenceEquals(
                    this,
                    other))
            {
                return true;
            }

            if (other.GetType() != GetType())
            {
                return false;
            }

            return (__typename.Equals(other.__typename));
        }

        public override global::System.Int32 GetHashCode()
        {
            unchecked
            {
                int hash = 5;

                hash ^= 397 * __typename.GetHashCode();

                return hash;
            }
        }
    }
}


// IGetHeroResult

// StrawberryShake.CodeGeneration.CSharp.Generators.ResultInterfaceGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public interface IGetHeroResult
    {
        public global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.IGetHero_Hero? Hero { get; }
    }
}


// IGetHero_Hero

// StrawberryShake.CodeGeneration.CSharp.Generators.ResultInterfaceGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public interface IGetHero_Hero
    {
        public global::System.String __typename { get; }
    }
}


// IGetHero_Hero_Droid

// StrawberryShake.CodeGeneration.CSharp.Generators.ResultInterfaceGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public interface IGetHero_Hero_Droid
        : IGetHero_Hero
    {
    }
}


// IGetHero_Hero_Human

// StrawberryShake.CodeGeneration.CSharp.Generators.ResultInterfaceGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public interface IGetHero_Hero_Human
        : IGetHero_Hero
    {
    }
}


// GetHeroQueryDocument

// StrawberryShake.CodeGeneration.CSharp.Generators.OperationDocumentGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces
{
    /// <summary>
    /// Represents the operation service of the GetHero GraphQL operation
    /// <code>
    /// query GetHero {
    ///   hero(episode: NEW_HOPE) {
    ///     __typename
    ///     ... on Droid {
    ///       id
    ///     }
    ///     ... on Human {
    ///       id
    ///     }
    ///   }
    /// }
    /// </code>
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class GetHeroQueryDocument
        : global::StrawberryShake.IDocument
    {
        private GetHeroQueryDocument()
        {
        }

        public static GetHeroQueryDocument Instance { get; } = new GetHeroQueryDocument();

        public global::StrawberryShake.OperationKind Kind => global::StrawberryShake.OperationKind.Query;

        public global::System.ReadOnlySpan<global::System.Byte> Body => new global::System.Byte[]{ 0x71, 0x75, 0x65, 0x72, 0x79, 0x20, 0x47, 0x65, 0x74, 0x48, 0x65, 0x72, 0x6f, 0x20, 0x7b, 0x20, 0x68, 0x65, 0x72, 0x6f, 0x28, 0x65, 0x70, 0x69, 0x73, 0x6f, 0x64, 0x65, 0x3a, 0x20, 0x4e, 0x45, 0x57, 0x5f, 0x48, 0x4f, 0x50, 0x45, 0x29, 0x20, 0x7b, 0x20, 0x5f, 0x5f, 0x74, 0x79, 0x70, 0x65, 0x6e, 0x61, 0x6d, 0x65, 0x20, 0x2e, 0x2e, 0x2e, 0x20, 0x6f, 0x6e, 0x20, 0x44, 0x72, 0x6f, 0x69, 0x64, 0x20, 0x7b, 0x20, 0x69, 0x64, 0x20, 0x7d, 0x20, 0x2e, 0x2e, 0x2e, 0x20, 0x6f, 0x6e, 0x20, 0x48, 0x75, 0x6d, 0x61, 0x6e, 0x20, 0x7b, 0x20, 0x69, 0x64, 0x20, 0x7d, 0x20, 0x7d, 0x20, 0x7d };

        public global::StrawberryShake.DocumentHash Hash { get; } = new global::StrawberryShake.DocumentHash("sha1Hash", "f8ce4e3a52ec254e48424fe573eb27a039aece1a");

        public override global::System.String ToString()
        {
            #if NETSTANDARD2_0
            return global::System.Text.Encoding.UTF8.GetString(Body.ToArray());
            #else
            return global::System.Text.Encoding.UTF8.GetString(Body);
            #endif
        }
    }
}


// GetHeroQuery

// StrawberryShake.CodeGeneration.CSharp.Generators.OperationServiceGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces
{
    /// <summary>
    /// Represents the operation service of the GetHero GraphQL operation
    /// <code>
    /// query GetHero {
    ///   hero(episode: NEW_HOPE) {
    ///     __typename
    ///     ... on Droid {
    ///       id
    ///     }
    ///     ... on Human {
    ///       id
    ///     }
    ///   }
    /// }
    /// </code>
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class GetHeroQuery
        : global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.IGetHeroQuery
    {
        private readonly global::StrawberryShake.IOperationExecutor<IGetHeroResult> _operationExecutor;

        public GetHeroQuery(global::StrawberryShake.IOperationExecutor<IGetHeroResult> operationExecutor)
        {
            _operationExecutor = operationExecutor
                 ?? throw new global::System.ArgumentNullException(nameof(operationExecutor));
        }

        global::System.Type global::StrawberryShake.IOperationRequestFactory.ResultType => typeof(IGetHeroResult);

        public async global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<IGetHeroResult>> ExecuteAsync(global::System.Threading.CancellationToken cancellationToken = default)
        {
            var request = CreateRequest();

            return await _operationExecutor
                .ExecuteAsync(
                    request,
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public global::System.IObservable<global::StrawberryShake.IOperationResult<IGetHeroResult>> Watch(global::StrawberryShake.ExecutionStrategy? strategy = null)
        {
            var request = CreateRequest();
            return _operationExecutor.Watch(
                request,
                strategy);
        }

        private global::StrawberryShake.OperationRequest CreateRequest()
        {

            return CreateRequest(null);
        }

        private global::StrawberryShake.OperationRequest CreateRequest(global::System.Collections.Generic.IReadOnlyDictionary<global::System.String, global::System.Object?>? variables)
        {

            return new global::StrawberryShake.OperationRequest(
                id: GetHeroQueryDocument.Instance.Hash.Value,
                name: "GetHero",
                document: GetHeroQueryDocument.Instance,
                strategy: global::StrawberryShake.RequestStrategy.Default);
        }

        global::StrawberryShake.OperationRequest global::StrawberryShake.IOperationRequestFactory.Create(global::System.Collections.Generic.IReadOnlyDictionary<global::System.String, global::System.Object?>? variables)
        {
            return CreateRequest();
        }
    }
}


// IGetHeroQuery

// StrawberryShake.CodeGeneration.CSharp.Generators.OperationServiceInterfaceGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces
{
    /// <summary>
    /// Represents the operation service of the GetHero GraphQL operation
    /// <code>
    /// query GetHero {
    ///   hero(episode: NEW_HOPE) {
    ///     __typename
    ///     ... on Droid {
    ///       id
    ///     }
    ///     ... on Human {
    ///       id
    ///     }
    ///   }
    /// }
    /// </code>
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public interface IGetHeroQuery
        : global::StrawberryShake.IOperationRequestFactory
    {
        global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<IGetHeroResult>> ExecuteAsync(global::System.Threading.CancellationToken cancellationToken = default);

        global::System.IObservable<global::StrawberryShake.IOperationResult<IGetHeroResult>> Watch(global::StrawberryShake.ExecutionStrategy? strategy = null);
    }
}


// GetHeroBuilder

// StrawberryShake.CodeGeneration.CSharp.Generators.JsonResultBuilderGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class GetHeroBuilder
        : global::StrawberryShake.IOperationResultBuilder<global::System.Text.Json.JsonDocument, global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.IGetHeroResult>
    {
        private readonly global::StrawberryShake.IEntityStore _entityStore;
        private readonly global::StrawberryShake.IEntityIdSerializer _idSerializer;
        private readonly global::StrawberryShake.IOperationResultDataFactory<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.IGetHeroResult> _resultDataFactory;
        private readonly global::StrawberryShake.Serialization.ILeafValueParser<global::System.String, global::System.String> _stringParser;

        public GetHeroBuilder(
            global::StrawberryShake.IEntityStore entityStore,
            global::StrawberryShake.IEntityIdSerializer idSerializer,
            global::StrawberryShake.IOperationResultDataFactory<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.IGetHeroResult> resultDataFactory,
            global::StrawberryShake.Serialization.ISerializerResolver serializerResolver)
        {
            _entityStore = entityStore
                 ?? throw new global::System.ArgumentNullException(nameof(entityStore));
            _idSerializer = idSerializer
                 ?? throw new global::System.ArgumentNullException(nameof(idSerializer));
            _resultDataFactory = resultDataFactory
                 ?? throw new global::System.ArgumentNullException(nameof(resultDataFactory));
            _stringParser = serializerResolver.GetLeafValueParser<global::System.String, global::System.String>("String")
                 ?? throw new global::System.ArgumentException("No serializer for type `String` found.");
        }

        public global::StrawberryShake.IOperationResult<IGetHeroResult> Build(global::StrawberryShake.Response<global::System.Text.Json.JsonDocument> response)
        {
            (IGetHeroResult Result, GetHeroResultInfo Info)? data = null;
            global::System.Collections.Generic.IReadOnlyList<global::StrawberryShake.IClientError>? errors = null;

            try
            {
                if (response.Body != null)
                {
                    if (response.Body.RootElement.TryGetProperty("data", out global::System.Text.Json.JsonElement dataElement) && dataElement.ValueKind == global::System.Text.Json.JsonValueKind.Object)
                    {
                        data = BuildData(dataElement);
                    }
                    if (response.Body.RootElement.TryGetProperty("errors", out global::System.Text.Json.JsonElement errorsElement))
                    {
                        errors = global::StrawberryShake.Json.JsonErrorParser.ParseErrors(errorsElement);
                    }
                }
            }
            catch(global::System.Exception ex)
            {
                errors = new global::StrawberryShake.IClientError[] {
                    new global::StrawberryShake.ClientError(
                        ex.Message,
                        exception: ex)
                };
            }

            return new global::StrawberryShake.OperationResult<IGetHeroResult>(
                data?.Result,
                data?.Info,
                _resultDataFactory,
                errors);
        }

        private (IGetHeroResult, GetHeroResultInfo) BuildData(global::System.Text.Json.JsonElement obj)
        {
            var entityIds = new global::System.Collections.Generic.HashSet<global::StrawberryShake.EntityId>();
            global::StrawberryShake.IEntityStoreSnapshot snapshot = default!;

            global::StrawberryShake.EntityId? heroId = default!;
            _entityStore.Update(session => 
            {
                heroId = UpdateIGetHero_HeroEntity(
                    session,
                    global::StrawberryShake.Json.JsonElementExtensions.GetPropertyOrNull(
                        obj,
                        "hero"),
                    entityIds);

                snapshot = session.CurrentSnapshot;
            });

            var resultInfo = new GetHeroResultInfo(
                heroId,
                entityIds,
                snapshot.Version);

            return (
                _resultDataFactory.Create(resultInfo),
                resultInfo
            );
        }

        private global::StrawberryShake.EntityId? UpdateIGetHero_HeroEntity(
            global::StrawberryShake.IEntityStoreUpdateSession session,
            global::System.Text.Json.JsonElement? obj,
            global::System.Collections.Generic.ISet<global::StrawberryShake.EntityId> entityIds)
        {
            if (!obj.HasValue)
            {
                return null;
            }

            global::StrawberryShake.EntityId entityId = _idSerializer.Parse(obj.Value);
            entityIds.Add(entityId);


            if (entityId.Name.Equals(
                    "Droid",
                    global::System.StringComparison.Ordinal))
            {
                if (session.CurrentSnapshot.TryGetEntity(
                        entityId,
                        out global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State.DroidEntity? entity))
                {
                    session.SetEntity(
                        entityId,
                        new global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State.DroidEntity(DeserializeNonNullableString(
                            global::StrawberryShake.Json.JsonElementExtensions.GetPropertyOrNull(
                                obj,
                                "__typename"))));
                }
                else
                {
                    session.SetEntity(
                        entityId,
                        new global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State.DroidEntity(DeserializeNonNullableString(
                            global::StrawberryShake.Json.JsonElementExtensions.GetPropertyOrNull(
                                obj,
                                "__typename"))));
                }

                return entityId;
            }

            if (entityId.Name.Equals(
                    "Human",
                    global::System.StringComparison.Ordinal))
            {
                if (session.CurrentSnapshot.TryGetEntity(
                        entityId,
                        out global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State.HumanEntity? entity))
                {
                    session.SetEntity(
                        entityId,
                        new global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State.HumanEntity(DeserializeNonNullableString(
                            global::StrawberryShake.Json.JsonElementExtensions.GetPropertyOrNull(
                                obj,
                                "__typename"))));
                }
                else
                {
                    session.SetEntity(
                        entityId,
                        new global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State.HumanEntity(DeserializeNonNullableString(
                            global::StrawberryShake.Json.JsonElementExtensions.GetPropertyOrNull(
                                obj,
                                "__typename"))));
                }

                return entityId;
            }

            throw new global::System.NotSupportedException();
        }

        private global::System.String DeserializeNonNullableString(global::System.Text.Json.JsonElement? obj)
        {
            if (!obj.HasValue)
            {
                throw new global::System.ArgumentNullException();
            }

            return _stringParser.Parse(obj.Value.GetString()!);
        }
    }
}


// StarWarsTypeNameOnInterfacesClient

// StrawberryShake.CodeGeneration.CSharp.Generators.ClientGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces
{
    /// <summary>
    /// Represents the StarWarsTypeNameOnInterfacesClient GraphQL client
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class StarWarsTypeNameOnInterfacesClient
        : global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.IStarWarsTypeNameOnInterfacesClient
    {
        private readonly global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.IGetHeroQuery _getHero;

        public StarWarsTypeNameOnInterfacesClient(global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.IGetHeroQuery getHero)
        {
            _getHero = getHero
                 ?? throw new global::System.ArgumentNullException(nameof(getHero));
        }

        public static global::System.String ClientName => "StarWarsTypeNameOnInterfacesClient";

        public global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.IGetHeroQuery GetHero => _getHero;
    }
}


// IStarWarsTypeNameOnInterfacesClient

// StrawberryShake.CodeGeneration.CSharp.Generators.ClientInterfaceGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces
{
    /// <summary>
    /// Represents the StarWarsTypeNameOnInterfacesClient GraphQL client
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public interface IStarWarsTypeNameOnInterfacesClient
    {
        global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.IGetHeroQuery GetHero { get; }
    }
}


// StarWarsTypeNameOnInterfacesClientEntityIdFactory

// StrawberryShake.CodeGeneration.CSharp.Generators.EntityIdFactoryGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class StarWarsTypeNameOnInterfacesClientEntityIdFactory
        : global::StrawberryShake.IEntityIdSerializer
    {
        private static readonly global::System.Text.Json.JsonWriterOptions _options = new global::System.Text.Json.JsonWriterOptions(){ Indented = false };

        public global::StrawberryShake.EntityId Parse(global::System.Text.Json.JsonElement obj)
        {
            global::System.String __typename = obj
                .GetProperty("__typename")
                .GetString()!;

            return __typename switch
            {
                "Droid" => ParseDroidEntityId(
                    obj,
                    __typename),
                "Human" => ParseHumanEntityId(
                    obj,
                    __typename),
                _ => throw new global::System.NotSupportedException()
            };
        }

        public global::System.String Format(global::StrawberryShake.EntityId entityId)
        {
            return entityId.Name switch
            {
                "Droid" => FormatDroidEntityId(entityId),
                "Human" => FormatHumanEntityId(entityId),
                _ => throw new global::System.NotSupportedException()
            };
        }

        private global::StrawberryShake.EntityId ParseDroidEntityId(
            global::System.Text.Json.JsonElement obj,
            global::System.String type)
        {
            return new global::StrawberryShake.EntityId(
                type,
                obj
                    .GetProperty("id")
                    .GetString()!);
        }

        private global::System.String FormatDroidEntityId(global::StrawberryShake.EntityId entityId)
        {
            using var writer = new global::StrawberryShake.Internal.ArrayWriter();
            using var jsonWriter = new global::System.Text.Json.Utf8JsonWriter(
                writer,
                _options);
            jsonWriter.WriteStartObject();

            jsonWriter.WriteString(
                "__typename",
                entityId.Name);

            jsonWriter.WriteString(
                "id",
                (global::System.String)entityId.Value);
            jsonWriter.WriteEndObject();
            jsonWriter.Flush();

            return global::System.Text.Encoding.UTF8.GetString(
                writer.GetInternalBuffer(),
                0,
                writer.Length);
        }

        private global::StrawberryShake.EntityId ParseHumanEntityId(
            global::System.Text.Json.JsonElement obj,
            global::System.String type)
        {
            return new global::StrawberryShake.EntityId(
                type,
                obj
                    .GetProperty("id")
                    .GetString()!);
        }

        private global::System.String FormatHumanEntityId(global::StrawberryShake.EntityId entityId)
        {
            using var writer = new global::StrawberryShake.Internal.ArrayWriter();
            using var jsonWriter = new global::System.Text.Json.Utf8JsonWriter(
                writer,
                _options);
            jsonWriter.WriteStartObject();

            jsonWriter.WriteString(
                "__typename",
                entityId.Name);

            jsonWriter.WriteString(
                "id",
                (global::System.String)entityId.Value);
            jsonWriter.WriteEndObject();
            jsonWriter.Flush();

            return global::System.Text.Encoding.UTF8.GetString(
                writer.GetInternalBuffer(),
                0,
                writer.Length);
        }
    }
}


// StarWarsTypeNameOnInterfacesClientServiceCollectionExtensions

// StrawberryShake.CodeGeneration.CSharp.Generators.DependencyInjectionGenerator

#nullable enable

namespace Microsoft.Extensions.DependencyInjection
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public static partial class StarWarsTypeNameOnInterfacesClientServiceCollectionExtensions
    {
        public static global::StrawberryShake.IClientBuilder<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State.StarWarsTypeNameOnInterfacesClientStoreAccessor> AddStarWarsTypeNameOnInterfacesClient(
            this global::Microsoft.Extensions.DependencyInjection.IServiceCollection services,
            global::StrawberryShake.ExecutionStrategy strategy = global::StrawberryShake.ExecutionStrategy.NetworkOnly)
        {
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton(
                services,
                sp => 
                {
                    var serviceCollection = ConfigureClientDefault(
                        sp,
                        strategy);

                    return new ClientServiceProvider(
                        global::Microsoft.Extensions.DependencyInjection.ServiceCollectionContainerBuilderExtensions.BuildServiceProvider(serviceCollection));
                });

            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton(
                services,
                sp => new global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State.StarWarsTypeNameOnInterfacesClientStoreAccessor(
                    global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<global::StrawberryShake.IOperationStore>(
                        global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<ClientServiceProvider>(sp)),
                    global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<global::StrawberryShake.IEntityStore>(
                        global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<ClientServiceProvider>(sp)),
                    global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<global::StrawberryShake.IEntityIdSerializer>(
                        global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<ClientServiceProvider>(sp)),
                    global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<global::System.Collections.Generic.IEnumerable<global::StrawberryShake.IOperationRequestFactory>>(
                        global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<ClientServiceProvider>(sp)),
                    global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<global::System.Collections.Generic.IEnumerable<global::StrawberryShake.IOperationResultDataFactory>>(
                        global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<ClientServiceProvider>(sp))));

            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton(
                services,
                sp => global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.GetHeroQuery>(
                    global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<ClientServiceProvider>(sp)));

            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton(
                services,
                sp => global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.StarWarsTypeNameOnInterfacesClient>(
                    global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<ClientServiceProvider>(sp)));
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton(
                services,
                sp => global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.IStarWarsTypeNameOnInterfacesClient>(
                    global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<ClientServiceProvider>(sp)));

            return new global::StrawberryShake.ClientBuilder<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State.StarWarsTypeNameOnInterfacesClientStoreAccessor>(
                "StarWarsTypeNameOnInterfacesClient",
                services);
        }

        private static global::Microsoft.Extensions.DependencyInjection.IServiceCollection ConfigureClientDefault(
            global::System.IServiceProvider parentServices,
            global::StrawberryShake.ExecutionStrategy strategy = global::StrawberryShake.ExecutionStrategy.NetworkOnly)
        {
            var services = new global::Microsoft.Extensions.DependencyInjection.ServiceCollection();
            global::Microsoft.Extensions.DependencyInjection.Extensions.ServiceCollectionDescriptorExtensions.TryAddSingleton<global::StrawberryShake.IEntityStore, global::StrawberryShake.EntityStore>(services);
            global::Microsoft.Extensions.DependencyInjection.Extensions.ServiceCollectionDescriptorExtensions.TryAddSingleton<global::StrawberryShake.IOperationStore>(
                services,
                sp => new global::StrawberryShake.OperationStore(global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<global::StrawberryShake.IEntityStore>(sp)));
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton(
                services,
                sp => 
                {
                    var clientFactory = global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<global::System.Net.Http.IHttpClientFactory>(parentServices);
                    return new global::StrawberryShake.Transport.Http.HttpConnection(() => clientFactory.CreateClient("StarWarsTypeNameOnInterfacesClient"));
                });

            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.IEntityMapper<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State.DroidEntity, global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.GetHero_Hero_Droid>, global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State.GetHero_Hero_DroidFromDroidEntityMapper>(services);
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.IEntityMapper<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State.HumanEntity, global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.GetHero_Hero_Human>, global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State.GetHero_Hero_HumanFromHumanEntityMapper>(services);

            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.Serialization.ISerializer, global::StrawberryShake.Serialization.StringSerializer>(services);
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.Serialization.ISerializer, global::StrawberryShake.Serialization.BooleanSerializer>(services);
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.Serialization.ISerializer, global::StrawberryShake.Serialization.ByteSerializer>(services);
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.Serialization.ISerializer, global::StrawberryShake.Serialization.ShortSerializer>(services);
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.Serialization.ISerializer, global::StrawberryShake.Serialization.IntSerializer>(services);
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.Serialization.ISerializer, global::StrawberryShake.Serialization.LongSerializer>(services);
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.Serialization.ISerializer, global::StrawberryShake.Serialization.FloatSerializer>(services);
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.Serialization.ISerializer, global::StrawberryShake.Serialization.DecimalSerializer>(services);
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.Serialization.ISerializer, global::StrawberryShake.Serialization.UrlSerializer>(services);
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.Serialization.ISerializer, global::StrawberryShake.Serialization.UuidSerializer>(services);
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.Serialization.ISerializer, global::StrawberryShake.Serialization.IdSerializer>(services);
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.Serialization.ISerializer, global::StrawberryShake.Serialization.DateTimeSerializer>(services);
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.Serialization.ISerializer, global::StrawberryShake.Serialization.DateSerializer>(services);
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.Serialization.ISerializer, global::StrawberryShake.Serialization.ByteArraySerializer>(services);
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.Serialization.ISerializer, global::StrawberryShake.Serialization.TimeSpanSerializer>(services);
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.Serialization.ISerializerResolver>(
                services,
                sp => new global::StrawberryShake.Serialization.SerializerResolver(
                    global::System.Linq.Enumerable.Concat(
                        global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<global::System.Collections.Generic.IEnumerable<global::StrawberryShake.Serialization.ISerializer>>(
                            parentServices),
                        global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<global::System.Collections.Generic.IEnumerable<global::StrawberryShake.Serialization.ISerializer>>(
                            sp))));

            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.IOperationResultDataFactory<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.IGetHeroResult>, global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State.GetHeroResultFactory>(services);
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.IOperationResultDataFactory>(
                services,
                sp => global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<global::StrawberryShake.IOperationResultDataFactory<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.IGetHeroResult>>(sp));
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.IOperationRequestFactory>(
                services,
                sp => global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.IGetHeroQuery>(sp));
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.IOperationResultBuilder<global::System.Text.Json.JsonDocument, global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.IGetHeroResult>, global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State.GetHeroBuilder>(services);
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.IOperationExecutor<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.IGetHeroResult>>(
                services,
                sp => new global::StrawberryShake.OperationExecutor<global::System.Text.Json.JsonDocument, global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.IGetHeroResult>(
                    global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<global::StrawberryShake.Transport.Http.HttpConnection>(sp),
                    () => global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<global::StrawberryShake.IOperationResultBuilder<global::System.Text.Json.JsonDocument, global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.IGetHeroResult>>(sp),
                    global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<global::StrawberryShake.IOperationStore>(sp),
                    strategy));
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.GetHeroQuery>(services);
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.IGetHeroQuery>(
                services,
                sp => global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.GetHeroQuery>(sp));
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.IEntityIdSerializer, global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State.StarWarsTypeNameOnInterfacesClientEntityIdFactory>(services);
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.StarWarsTypeNameOnInterfacesClient>(services);
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.IStarWarsTypeNameOnInterfacesClient>(
                services,
                sp => global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.StarWarsTypeNameOnInterfacesClient>(sp));
            return services;
        }

        private class ClientServiceProvider
            : System.IServiceProvider
            , System.IDisposable
        {
            private readonly System.IServiceProvider _provider;

            public ClientServiceProvider(System.IServiceProvider provider)
            {
                _provider = provider;
            }

            public object? GetService(System.Type serviceType)
            {
                return _provider.GetService(serviceType);
            }

            public void Dispose()
            {
                if (_provider is System.IDisposable d)
                {
                    d.Dispose();
                }
            }
        }
    }
}


// StarWarsTypeNameOnInterfacesClientStoreAccessor

// StrawberryShake.CodeGeneration.CSharp.Generators.StoreAccessorGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnInterfaces.State
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class StarWarsTypeNameOnInterfacesClientStoreAccessor
        : global::StrawberryShake.StoreAccessor
    {
        public StarWarsTypeNameOnInterfacesClientStoreAccessor(
            global::StrawberryShake.IOperationStore operationStore,
            global::StrawberryShake.IEntityStore entityStore,
            global::StrawberryShake.IEntityIdSerializer entityIdSerializer,
            global::System.Collections.Generic.IEnumerable<global::StrawberryShake.IOperationRequestFactory> requestFactories,
            global::System.Collections.Generic.IEnumerable<global::StrawberryShake.IOperationResultDataFactory> resultDataFactories)
            : base(
                operationStore,
                entityStore,
                entityIdSerializer,
                requestFactories,
                resultDataFactories)
        {
        }
    }
}


