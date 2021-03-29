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

// StarshipEntity

// StrawberryShake.CodeGeneration.CSharp.Generators.EntityTypeGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class StarshipEntity
    {
        public StarshipEntity(global::System.String __typename)
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

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State
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


// DroidEntity

// StrawberryShake.CodeGeneration.CSharp.Generators.EntityTypeGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State
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


// SearchHeroResultFactory

// StrawberryShake.CodeGeneration.CSharp.Generators.ResultDataFactoryGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class SearchHeroResultFactory
        : global::StrawberryShake.IOperationResultDataFactory<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.SearchHeroResult>
    {
        private readonly global::StrawberryShake.IEntityStore _entityStore;
        private readonly global::StrawberryShake.IEntityMapper<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.StarshipEntity, SearchHero_Search_Starship> _searchHero_Search_StarshipFromStarshipEntityMapper;
        private readonly global::StrawberryShake.IEntityMapper<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.HumanEntity, SearchHero_Search_Human> _searchHero_Search_HumanFromHumanEntityMapper;
        private readonly global::StrawberryShake.IEntityMapper<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.DroidEntity, SearchHero_Search_Droid> _searchHero_Search_DroidFromDroidEntityMapper;

        public SearchHeroResultFactory(
            global::StrawberryShake.IEntityStore entityStore,
            global::StrawberryShake.IEntityMapper<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.StarshipEntity, SearchHero_Search_Starship> searchHero_Search_StarshipFromStarshipEntityMapper,
            global::StrawberryShake.IEntityMapper<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.HumanEntity, SearchHero_Search_Human> searchHero_Search_HumanFromHumanEntityMapper,
            global::StrawberryShake.IEntityMapper<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.DroidEntity, SearchHero_Search_Droid> searchHero_Search_DroidFromDroidEntityMapper)
        {
            _entityStore = entityStore
                 ?? throw new global::System.ArgumentNullException(nameof(entityStore));
            _searchHero_Search_StarshipFromStarshipEntityMapper = searchHero_Search_StarshipFromStarshipEntityMapper
                 ?? throw new global::System.ArgumentNullException(nameof(searchHero_Search_StarshipFromStarshipEntityMapper));
            _searchHero_Search_HumanFromHumanEntityMapper = searchHero_Search_HumanFromHumanEntityMapper
                 ?? throw new global::System.ArgumentNullException(nameof(searchHero_Search_HumanFromHumanEntityMapper));
            _searchHero_Search_DroidFromDroidEntityMapper = searchHero_Search_DroidFromDroidEntityMapper
                 ?? throw new global::System.ArgumentNullException(nameof(searchHero_Search_DroidFromDroidEntityMapper));
        }

        global::System.Type global::StrawberryShake.IOperationResultDataFactory.ResultType => typeof(global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.ISearchHeroResult);

        public SearchHeroResult Create(
            global::StrawberryShake.IOperationResultDataInfo dataInfo,
            global::StrawberryShake.IEntityStoreSnapshot? snapshot = null)
        {
            if (snapshot is null)
            {
                snapshot = _entityStore.CurrentSnapshot;
            }

            if (dataInfo is SearchHeroResultInfo info)
            {
                return new SearchHeroResult(MapISearchHero_SearchArray(
                    info.Search,
                    snapshot));
            }

            throw new global::System.ArgumentException("SearchHeroResultInfo expected.");
        }

        private global::System.Collections.Generic.IReadOnlyList<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.ISearchHero_Search?>? MapISearchHero_SearchArray(
            global::System.Collections.Generic.IReadOnlyList<global::StrawberryShake.EntityId?>? list,
            global::StrawberryShake.IEntityStoreSnapshot snapshot)
        {
            if (list is null)
            {
                return null;
            }

            var searchResults = new global::System.Collections.Generic.List<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.ISearchHero_Search?>();

            foreach (global::StrawberryShake.EntityId? child in list)
            {
                searchResults.Add(MapISearchHero_Search(
                    child,
                    snapshot));
            }

            return searchResults;
        }

        private global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.ISearchHero_Search? MapISearchHero_Search(
            global::StrawberryShake.EntityId? entityId,
            global::StrawberryShake.IEntityStoreSnapshot snapshot)
        {
            if (entityId is null)
            {
                return null;
            }


            if (entityId.Value.Name.Equals(
                    "Starship",
                    global::System.StringComparison.Ordinal))
            {
                return _searchHero_Search_StarshipFromStarshipEntityMapper.Map(
                    snapshot.GetEntity<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.StarshipEntity>(entityId.Value)
                        ?? throw new global::StrawberryShake.GraphQLClientException());
            }

            if (entityId.Value.Name.Equals(
                    "Human",
                    global::System.StringComparison.Ordinal))
            {
                return _searchHero_Search_HumanFromHumanEntityMapper.Map(
                    snapshot.GetEntity<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.HumanEntity>(entityId.Value)
                        ?? throw new global::StrawberryShake.GraphQLClientException());
            }

            if (entityId.Value.Name.Equals(
                    "Droid",
                    global::System.StringComparison.Ordinal))
            {
                return _searchHero_Search_DroidFromDroidEntityMapper.Map(
                    snapshot.GetEntity<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.DroidEntity>(entityId.Value)
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


// SearchHeroResultInfo

// StrawberryShake.CodeGeneration.CSharp.Generators.ResultInfoGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class SearchHeroResultInfo
        : global::StrawberryShake.IOperationResultDataInfo
    {
        private readonly global::System.Collections.Generic.IReadOnlyCollection<global::StrawberryShake.EntityId> _entityIds;
        private readonly global::System.UInt64 _version;

        public SearchHeroResultInfo(
            global::System.Collections.Generic.IReadOnlyList<global::StrawberryShake.EntityId?>? search,
            global::System.Collections.Generic.IReadOnlyCollection<global::StrawberryShake.EntityId> entityIds,
            global::System.UInt64 version)
        {
            Search = search;
            _entityIds = entityIds
                 ?? throw new global::System.ArgumentNullException(nameof(entityIds));
            _version = version;
        }

        public global::System.Collections.Generic.IReadOnlyList<global::StrawberryShake.EntityId?>? Search { get; }

        public global::System.Collections.Generic.IReadOnlyCollection<global::StrawberryShake.EntityId> EntityIds => _entityIds;

        public global::System.UInt64 Version => _version;

        public global::StrawberryShake.IOperationResultDataInfo WithVersion(global::System.UInt64 version)
        {
            return new SearchHeroResultInfo(
                Search,
                _entityIds,
                version);
        }
    }
}


// SearchHeroResult

// StrawberryShake.CodeGeneration.CSharp.Generators.ResultTypeGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class SearchHeroResult
        : global::System.IEquatable<SearchHeroResult>
        , ISearchHeroResult
    {
        public SearchHeroResult(global::System.Collections.Generic.IReadOnlyList<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.ISearchHero_Search?>? search)
        {
            Search = search;
        }

        public global::System.Collections.Generic.IReadOnlyList<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.ISearchHero_Search?>? Search { get; }

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

            return Equals((SearchHeroResult)obj);
        }

        public global::System.Boolean Equals(SearchHeroResult? other)
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

            return (global::StrawberryShake.Helper.ComparisonHelper.SequenceEqual(
                        Search,
                        other.Search));
        }

        public override global::System.Int32 GetHashCode()
        {
            unchecked
            {
                int hash = 5;

                if (!(Search is null))
                {
                    foreach (var Search_elm in Search)
                    {
                        if (!(Search_elm is null))
                        {
                            hash ^= 397 * Search_elm.GetHashCode();
                        }
                    }
                }

                return hash;
            }
        }
    }
}


// SearchHero_Search_StarshipFromStarshipEntityMapper

// StrawberryShake.CodeGeneration.CSharp.Generators.ResultFromEntityTypeMapperGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class SearchHero_Search_StarshipFromStarshipEntityMapper
        : global::StrawberryShake.IEntityMapper<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.StarshipEntity, SearchHero_Search_Starship>
    {
        private readonly global::StrawberryShake.IEntityStore _entityStore;

        public SearchHero_Search_StarshipFromStarshipEntityMapper(global::StrawberryShake.IEntityStore entityStore)
        {
            _entityStore = entityStore
                 ?? throw new global::System.ArgumentNullException(nameof(entityStore));
        }

        public SearchHero_Search_Starship Map(
            global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.StarshipEntity entity,
            global::StrawberryShake.IEntityStoreSnapshot? snapshot = null)
        {
            if (snapshot is null)
            {
                snapshot = _entityStore.CurrentSnapshot;
            }

            return new SearchHero_Search_Starship(entity.__typename);
        }
    }
}


// SearchHero_Search_Starship

// StrawberryShake.CodeGeneration.CSharp.Generators.ResultTypeGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class SearchHero_Search_Starship
        : global::System.IEquatable<SearchHero_Search_Starship>
        , ISearchHero_Search_Starship
    {
        public SearchHero_Search_Starship(global::System.String __typename)
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

            return Equals((SearchHero_Search_Starship)obj);
        }

        public global::System.Boolean Equals(SearchHero_Search_Starship? other)
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


// SearchHero_Search_HumanFromHumanEntityMapper

// StrawberryShake.CodeGeneration.CSharp.Generators.ResultFromEntityTypeMapperGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class SearchHero_Search_HumanFromHumanEntityMapper
        : global::StrawberryShake.IEntityMapper<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.HumanEntity, SearchHero_Search_Human>
    {
        private readonly global::StrawberryShake.IEntityStore _entityStore;

        public SearchHero_Search_HumanFromHumanEntityMapper(global::StrawberryShake.IEntityStore entityStore)
        {
            _entityStore = entityStore
                 ?? throw new global::System.ArgumentNullException(nameof(entityStore));
        }

        public SearchHero_Search_Human Map(
            global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.HumanEntity entity,
            global::StrawberryShake.IEntityStoreSnapshot? snapshot = null)
        {
            if (snapshot is null)
            {
                snapshot = _entityStore.CurrentSnapshot;
            }

            return new SearchHero_Search_Human(entity.__typename);
        }
    }
}


// SearchHero_Search_Human

// StrawberryShake.CodeGeneration.CSharp.Generators.ResultTypeGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class SearchHero_Search_Human
        : global::System.IEquatable<SearchHero_Search_Human>
        , ISearchHero_Search_Human
    {
        public SearchHero_Search_Human(global::System.String __typename)
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

            return Equals((SearchHero_Search_Human)obj);
        }

        public global::System.Boolean Equals(SearchHero_Search_Human? other)
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


// SearchHero_Search_DroidFromDroidEntityMapper

// StrawberryShake.CodeGeneration.CSharp.Generators.ResultFromEntityTypeMapperGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class SearchHero_Search_DroidFromDroidEntityMapper
        : global::StrawberryShake.IEntityMapper<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.DroidEntity, SearchHero_Search_Droid>
    {
        private readonly global::StrawberryShake.IEntityStore _entityStore;

        public SearchHero_Search_DroidFromDroidEntityMapper(global::StrawberryShake.IEntityStore entityStore)
        {
            _entityStore = entityStore
                 ?? throw new global::System.ArgumentNullException(nameof(entityStore));
        }

        public SearchHero_Search_Droid Map(
            global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.DroidEntity entity,
            global::StrawberryShake.IEntityStoreSnapshot? snapshot = null)
        {
            if (snapshot is null)
            {
                snapshot = _entityStore.CurrentSnapshot;
            }

            return new SearchHero_Search_Droid(entity.__typename);
        }
    }
}


// SearchHero_Search_Droid

// StrawberryShake.CodeGeneration.CSharp.Generators.ResultTypeGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class SearchHero_Search_Droid
        : global::System.IEquatable<SearchHero_Search_Droid>
        , ISearchHero_Search_Droid
    {
        public SearchHero_Search_Droid(global::System.String __typename)
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

            return Equals((SearchHero_Search_Droid)obj);
        }

        public global::System.Boolean Equals(SearchHero_Search_Droid? other)
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


// ISearchHeroResult

// StrawberryShake.CodeGeneration.CSharp.Generators.ResultInterfaceGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public interface ISearchHeroResult
    {
        public global::System.Collections.Generic.IReadOnlyList<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.ISearchHero_Search?>? Search { get; }
    }
}


// ISearchHero_Search

// StrawberryShake.CodeGeneration.CSharp.Generators.ResultInterfaceGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public interface ISearchHero_Search
    {
        public global::System.String __typename { get; }
    }
}


// ISearchHero_Search_Starship

// StrawberryShake.CodeGeneration.CSharp.Generators.ResultInterfaceGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public interface ISearchHero_Search_Starship
        : ISearchHero_Search
    {
    }
}


// ISearchHero_Search_Human

// StrawberryShake.CodeGeneration.CSharp.Generators.ResultInterfaceGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public interface ISearchHero_Search_Human
        : ISearchHero_Search
    {
    }
}


// ISearchHero_Search_Droid

// StrawberryShake.CodeGeneration.CSharp.Generators.ResultInterfaceGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public interface ISearchHero_Search_Droid
        : ISearchHero_Search
    {
    }
}


// SearchHeroQueryDocument

// StrawberryShake.CodeGeneration.CSharp.Generators.OperationDocumentGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions
{
    /// <summary>
    /// Represents the operation service of the SearchHero GraphQL operation
    /// <code>
    /// query SearchHero {
    ///   search(text: "l") {
    ///     __typename
    ///     ... on Starship {
    ///       id
    ///     }
    ///     ... on Human {
    ///       id
    ///     }
    ///     ... on Droid {
    ///       id
    ///     }
    ///   }
    /// }
    /// </code>
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class SearchHeroQueryDocument
        : global::StrawberryShake.IDocument
    {
        private SearchHeroQueryDocument()
        {
        }

        public static SearchHeroQueryDocument Instance { get; } = new SearchHeroQueryDocument();

        public global::StrawberryShake.OperationKind Kind => global::StrawberryShake.OperationKind.Query;

        public global::System.ReadOnlySpan<global::System.Byte> Body => new global::System.Byte[]{ 0x71, 0x75, 0x65, 0x72, 0x79, 0x20, 0x53, 0x65, 0x61, 0x72, 0x63, 0x68, 0x48, 0x65, 0x72, 0x6f, 0x20, 0x7b, 0x20, 0x73, 0x65, 0x61, 0x72, 0x63, 0x68, 0x28, 0x74, 0x65, 0x78, 0x74, 0x3a, 0x20, 0x22, 0x6c, 0x22, 0x29, 0x20, 0x7b, 0x20, 0x5f, 0x5f, 0x74, 0x79, 0x70, 0x65, 0x6e, 0x61, 0x6d, 0x65, 0x20, 0x2e, 0x2e, 0x2e, 0x20, 0x6f, 0x6e, 0x20, 0x53, 0x74, 0x61, 0x72, 0x73, 0x68, 0x69, 0x70, 0x20, 0x7b, 0x20, 0x69, 0x64, 0x20, 0x7d, 0x20, 0x2e, 0x2e, 0x2e, 0x20, 0x6f, 0x6e, 0x20, 0x48, 0x75, 0x6d, 0x61, 0x6e, 0x20, 0x7b, 0x20, 0x69, 0x64, 0x20, 0x7d, 0x20, 0x2e, 0x2e, 0x2e, 0x20, 0x6f, 0x6e, 0x20, 0x44, 0x72, 0x6f, 0x69, 0x64, 0x20, 0x7b, 0x20, 0x69, 0x64, 0x20, 0x7d, 0x20, 0x7d, 0x20, 0x7d };

        public global::StrawberryShake.DocumentHash Hash { get; } = new global::StrawberryShake.DocumentHash("sha1Hash", "e2347e3fc516d7742122125fa68a1aca286f128c");

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


// SearchHeroQuery

// StrawberryShake.CodeGeneration.CSharp.Generators.OperationServiceGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions
{
    /// <summary>
    /// Represents the operation service of the SearchHero GraphQL operation
    /// <code>
    /// query SearchHero {
    ///   search(text: "l") {
    ///     __typename
    ///     ... on Starship {
    ///       id
    ///     }
    ///     ... on Human {
    ///       id
    ///     }
    ///     ... on Droid {
    ///       id
    ///     }
    ///   }
    /// }
    /// </code>
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class SearchHeroQuery
        : global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.ISearchHeroQuery
    {
        private readonly global::StrawberryShake.IOperationExecutor<ISearchHeroResult> _operationExecutor;

        public SearchHeroQuery(global::StrawberryShake.IOperationExecutor<ISearchHeroResult> operationExecutor)
        {
            _operationExecutor = operationExecutor
                 ?? throw new global::System.ArgumentNullException(nameof(operationExecutor));
        }

        global::System.Type global::StrawberryShake.IOperationRequestFactory.ResultType => typeof(ISearchHeroResult);

        public async global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<ISearchHeroResult>> ExecuteAsync(global::System.Threading.CancellationToken cancellationToken = default)
        {
            var request = CreateRequest();

            return await _operationExecutor
                .ExecuteAsync(
                    request,
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public global::System.IObservable<global::StrawberryShake.IOperationResult<ISearchHeroResult>> Watch(global::StrawberryShake.ExecutionStrategy? strategy = null)
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
                id: SearchHeroQueryDocument.Instance.Hash.Value,
                name: "SearchHero",
                document: SearchHeroQueryDocument.Instance,
                strategy: global::StrawberryShake.RequestStrategy.Default);
        }

        global::StrawberryShake.OperationRequest global::StrawberryShake.IOperationRequestFactory.Create(global::System.Collections.Generic.IReadOnlyDictionary<global::System.String, global::System.Object?>? variables)
        {
            return CreateRequest();
        }
    }
}


// ISearchHeroQuery

// StrawberryShake.CodeGeneration.CSharp.Generators.OperationServiceInterfaceGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions
{
    /// <summary>
    /// Represents the operation service of the SearchHero GraphQL operation
    /// <code>
    /// query SearchHero {
    ///   search(text: "l") {
    ///     __typename
    ///     ... on Starship {
    ///       id
    ///     }
    ///     ... on Human {
    ///       id
    ///     }
    ///     ... on Droid {
    ///       id
    ///     }
    ///   }
    /// }
    /// </code>
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public interface ISearchHeroQuery
        : global::StrawberryShake.IOperationRequestFactory
    {
        global::System.Threading.Tasks.Task<global::StrawberryShake.IOperationResult<ISearchHeroResult>> ExecuteAsync(global::System.Threading.CancellationToken cancellationToken = default);

        global::System.IObservable<global::StrawberryShake.IOperationResult<ISearchHeroResult>> Watch(global::StrawberryShake.ExecutionStrategy? strategy = null);
    }
}


// SearchHeroBuilder

// StrawberryShake.CodeGeneration.CSharp.Generators.JsonResultBuilderGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class SearchHeroBuilder
        : global::StrawberryShake.IOperationResultBuilder<global::System.Text.Json.JsonDocument, global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.ISearchHeroResult>
    {
        private readonly global::StrawberryShake.IEntityStore _entityStore;
        private readonly global::StrawberryShake.IEntityIdSerializer _idSerializer;
        private readonly global::StrawberryShake.IOperationResultDataFactory<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.ISearchHeroResult> _resultDataFactory;
        private readonly global::StrawberryShake.Serialization.ILeafValueParser<global::System.String, global::System.String> _stringParser;

        public SearchHeroBuilder(
            global::StrawberryShake.IEntityStore entityStore,
            global::StrawberryShake.IEntityIdSerializer idSerializer,
            global::StrawberryShake.IOperationResultDataFactory<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.ISearchHeroResult> resultDataFactory,
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

        public global::StrawberryShake.IOperationResult<ISearchHeroResult> Build(global::StrawberryShake.Response<global::System.Text.Json.JsonDocument> response)
        {
            (ISearchHeroResult Result, SearchHeroResultInfo Info)? data = null;
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

            return new global::StrawberryShake.OperationResult<ISearchHeroResult>(
                data?.Result,
                data?.Info,
                _resultDataFactory,
                errors);
        }

        private (ISearchHeroResult, SearchHeroResultInfo) BuildData(global::System.Text.Json.JsonElement obj)
        {
            var entityIds = new global::System.Collections.Generic.HashSet<global::StrawberryShake.EntityId>();
            global::StrawberryShake.IEntityStoreSnapshot snapshot = default!;

            global::System.Collections.Generic.IReadOnlyList<global::StrawberryShake.EntityId?>? searchId = default!;
            _entityStore.Update(session => 
            {
                searchId = UpdateISearchHero_SearchEntityArray(
                    session,
                    global::StrawberryShake.Json.JsonElementExtensions.GetPropertyOrNull(
                        obj,
                        "search"),
                    entityIds);

                snapshot = session.CurrentSnapshot;
            });

            var resultInfo = new SearchHeroResultInfo(
                searchId,
                entityIds,
                snapshot.Version);

            return (
                _resultDataFactory.Create(resultInfo),
                resultInfo
            );
        }

        private global::System.Collections.Generic.IReadOnlyList<global::StrawberryShake.EntityId?>? UpdateISearchHero_SearchEntityArray(
            global::StrawberryShake.IEntityStoreUpdateSession session,
            global::System.Text.Json.JsonElement? obj,
            global::System.Collections.Generic.ISet<global::StrawberryShake.EntityId> entityIds)
        {
            if (!obj.HasValue)
            {
                return null;
            }

            var searchResults = new global::System.Collections.Generic.List<global::StrawberryShake.EntityId?>();

            foreach (global::System.Text.Json.JsonElement child in obj.Value.EnumerateArray())
            {
                searchResults.Add(UpdateISearchHero_SearchEntity(
                    session,
                    child,
                    entityIds));
            }

            return searchResults;
        }

        private global::StrawberryShake.EntityId? UpdateISearchHero_SearchEntity(
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
                    "Starship",
                    global::System.StringComparison.Ordinal))
            {
                if (session.CurrentSnapshot.TryGetEntity(
                        entityId,
                        out global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.StarshipEntity? entity))
                {
                    session.SetEntity(
                        entityId,
                        new global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.StarshipEntity(DeserializeNonNullableString(
                            global::StrawberryShake.Json.JsonElementExtensions.GetPropertyOrNull(
                                obj,
                                "__typename"))));
                }
                else
                {
                    session.SetEntity(
                        entityId,
                        new global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.StarshipEntity(DeserializeNonNullableString(
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
                        out global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.HumanEntity? entity))
                {
                    session.SetEntity(
                        entityId,
                        new global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.HumanEntity(DeserializeNonNullableString(
                            global::StrawberryShake.Json.JsonElementExtensions.GetPropertyOrNull(
                                obj,
                                "__typename"))));
                }
                else
                {
                    session.SetEntity(
                        entityId,
                        new global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.HumanEntity(DeserializeNonNullableString(
                            global::StrawberryShake.Json.JsonElementExtensions.GetPropertyOrNull(
                                obj,
                                "__typename"))));
                }

                return entityId;
            }

            if (entityId.Name.Equals(
                    "Droid",
                    global::System.StringComparison.Ordinal))
            {
                if (session.CurrentSnapshot.TryGetEntity(
                        entityId,
                        out global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.DroidEntity? entity))
                {
                    session.SetEntity(
                        entityId,
                        new global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.DroidEntity(DeserializeNonNullableString(
                            global::StrawberryShake.Json.JsonElementExtensions.GetPropertyOrNull(
                                obj,
                                "__typename"))));
                }
                else
                {
                    session.SetEntity(
                        entityId,
                        new global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.DroidEntity(DeserializeNonNullableString(
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


// StarWarsTypeNameOnUnionsClient

// StrawberryShake.CodeGeneration.CSharp.Generators.ClientGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions
{
    /// <summary>
    /// Represents the StarWarsTypeNameOnUnionsClient GraphQL client
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class StarWarsTypeNameOnUnionsClient
        : global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.IStarWarsTypeNameOnUnionsClient
    {
        private readonly global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.ISearchHeroQuery _searchHero;

        public StarWarsTypeNameOnUnionsClient(global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.ISearchHeroQuery searchHero)
        {
            _searchHero = searchHero
                 ?? throw new global::System.ArgumentNullException(nameof(searchHero));
        }

        public static global::System.String ClientName => "StarWarsTypeNameOnUnionsClient";

        public global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.ISearchHeroQuery SearchHero => _searchHero;
    }
}


// IStarWarsTypeNameOnUnionsClient

// StrawberryShake.CodeGeneration.CSharp.Generators.ClientInterfaceGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions
{
    /// <summary>
    /// Represents the StarWarsTypeNameOnUnionsClient GraphQL client
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public interface IStarWarsTypeNameOnUnionsClient
    {
        global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.ISearchHeroQuery SearchHero { get; }
    }
}


// StarWarsTypeNameOnUnionsClientEntityIdFactory

// StrawberryShake.CodeGeneration.CSharp.Generators.EntityIdFactoryGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class StarWarsTypeNameOnUnionsClientEntityIdFactory
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
                "Starship" => ParseStarshipEntityId(
                    obj,
                    __typename),
                "Human" => ParseHumanEntityId(
                    obj,
                    __typename),
                "Droid" => ParseDroidEntityId(
                    obj,
                    __typename),
                _ => throw new global::System.NotSupportedException()
            };
        }

        public global::System.String Format(global::StrawberryShake.EntityId entityId)
        {
            return entityId.Name switch
            {
                "Starship" => FormatStarshipEntityId(entityId),
                "Human" => FormatHumanEntityId(entityId),
                "Droid" => FormatDroidEntityId(entityId),
                _ => throw new global::System.NotSupportedException()
            };
        }

        private global::StrawberryShake.EntityId ParseStarshipEntityId(
            global::System.Text.Json.JsonElement obj,
            global::System.String type)
        {
            return new global::StrawberryShake.EntityId(
                type,
                obj
                    .GetProperty("id")
                    .GetString()!);
        }

        private global::System.String FormatStarshipEntityId(global::StrawberryShake.EntityId entityId)
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
    }
}


// StarWarsTypeNameOnUnionsClientServiceCollectionExtensions

// StrawberryShake.CodeGeneration.CSharp.Generators.DependencyInjectionGenerator

#nullable enable

namespace Microsoft.Extensions.DependencyInjection
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public static partial class StarWarsTypeNameOnUnionsClientServiceCollectionExtensions
    {
        public static global::StrawberryShake.IClientBuilder<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.StarWarsTypeNameOnUnionsClientStoreAccessor> AddStarWarsTypeNameOnUnionsClient(
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
                sp => new global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.StarWarsTypeNameOnUnionsClientStoreAccessor(
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
                sp => global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.SearchHeroQuery>(
                    global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<ClientServiceProvider>(sp)));

            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton(
                services,
                sp => global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.StarWarsTypeNameOnUnionsClient>(
                    global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<ClientServiceProvider>(sp)));
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton(
                services,
                sp => global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.IStarWarsTypeNameOnUnionsClient>(
                    global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<ClientServiceProvider>(sp)));

            return new global::StrawberryShake.ClientBuilder<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.StarWarsTypeNameOnUnionsClientStoreAccessor>(
                "StarWarsTypeNameOnUnionsClient",
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
                    return new global::StrawberryShake.Transport.Http.HttpConnection(() => clientFactory.CreateClient("StarWarsTypeNameOnUnionsClient"));
                });

            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.IEntityMapper<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.StarshipEntity, global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.SearchHero_Search_Starship>, global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.SearchHero_Search_StarshipFromStarshipEntityMapper>(services);
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.IEntityMapper<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.HumanEntity, global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.SearchHero_Search_Human>, global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.SearchHero_Search_HumanFromHumanEntityMapper>(services);
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.IEntityMapper<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.DroidEntity, global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.SearchHero_Search_Droid>, global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.SearchHero_Search_DroidFromDroidEntityMapper>(services);

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

            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.IOperationResultDataFactory<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.ISearchHeroResult>, global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.SearchHeroResultFactory>(services);
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.IOperationResultDataFactory>(
                services,
                sp => global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<global::StrawberryShake.IOperationResultDataFactory<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.ISearchHeroResult>>(sp));
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.IOperationRequestFactory>(
                services,
                sp => global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.ISearchHeroQuery>(sp));
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.IOperationResultBuilder<global::System.Text.Json.JsonDocument, global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.ISearchHeroResult>, global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.SearchHeroBuilder>(services);
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.IOperationExecutor<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.ISearchHeroResult>>(
                services,
                sp => new global::StrawberryShake.OperationExecutor<global::System.Text.Json.JsonDocument, global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.ISearchHeroResult>(
                    global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<global::StrawberryShake.Transport.Http.HttpConnection>(sp),
                    () => global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<global::StrawberryShake.IOperationResultBuilder<global::System.Text.Json.JsonDocument, global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.ISearchHeroResult>>(sp),
                    global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<global::StrawberryShake.IOperationStore>(sp),
                    strategy));
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.SearchHeroQuery>(services);
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.ISearchHeroQuery>(
                services,
                sp => global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.SearchHeroQuery>(sp));
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.IEntityIdSerializer, global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State.StarWarsTypeNameOnUnionsClientEntityIdFactory>(services);
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.StarWarsTypeNameOnUnionsClient>(services);
            global::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.IStarWarsTypeNameOnUnionsClient>(
                services,
                sp => global::Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<global::StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.StarWarsTypeNameOnUnionsClient>(sp));
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


// StarWarsTypeNameOnUnionsClientStoreAccessor

// StrawberryShake.CodeGeneration.CSharp.Generators.StoreAccessorGenerator

#nullable enable

namespace StrawberryShake.CodeGeneration.CSharp.Integration.StarWarsTypeNameOnUnions.State
{
    [global::System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class StarWarsTypeNameOnUnionsClientStoreAccessor
        : global::StrawberryShake.StoreAccessor
    {
        public StarWarsTypeNameOnUnionsClientStoreAccessor(
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


