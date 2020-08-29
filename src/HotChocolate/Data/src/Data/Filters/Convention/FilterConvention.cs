using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using System.Linq;
using System.Collections.Generic;
using HotChocolate.Utilities;
using HotChocolate.Language;
using HotChocolate.Configuration;
using System.Threading.Tasks;
using HotChocolate.Resolvers;

namespace HotChocolate.Data.Filters
{
    public class FilterConvention
        : ConventionBase<FilterConventionDefinition>
        , IFilterConvention
    {
        private readonly Action<IFilterConventionDescriptor> _configure;

        protected FilterConvention()
        {
            _configure = Configure;
        }

        public FilterConvention(Action<IFilterConventionDescriptor> configure)
        {
            _configure = configure ??
                throw new ArgumentNullException(nameof(configure));
        }

        public IReadOnlyDictionary<int, OperationConvention> Operations { get; private set; }
            = null!;

        public IReadOnlyDictionary<Type, Type> Bindings { get; private set; } = null!;

        public IReadOnlyDictionary<ITypeReference, Action<IFilterInputTypeDescriptor>[]> Extensions
        { get; private set; } = null!;

        public string ArgumentName { get; private set; } = null!;

        public FilterProviderBase Provider { get; private set; }

        protected override FilterConventionDefinition CreateDefinition(
            IConventionContext context)
        {
            var descriptor = FilterConventionDescriptor.New(context);
            _configure(descriptor);
            return descriptor.CreateDefinition();
        }

        protected virtual void Configure(
            IFilterConventionDescriptor descriptor)
        {
        }

        protected override void OnComplete(
            IConventionContext context,
            FilterConventionDefinition? definition)
        {
            if (definition is { })
            {
                Operations = definition
                    .Operations
                    .ToDictionary(x => x.Operation, x => new OperationConvention(x));
                Bindings = definition.Bindings;
                Extensions = definition.Extensions.ToDictionary(x => x.Key, x => x.Value.ToArray());

                ArgumentName = definition.ArgumentName ??
                    throw ThrowHelper.FilterConvention_NoProviderFound(definition.Scope);

                Provider = definition.Provider ??
                    throw ThrowHelper.FilterConvention_NoProviderFound(definition.Scope);

                IFilterProviderInitializationContext? providerContext =
                    FilterProviderInitializationContext.From(context, this);

                Provider.Initialize(providerContext);
            }
        }

        public NameString GetFieldDescription(IDescriptorContext context, MemberInfo member) =>
            context.Naming.GetMemberDescription(member, MemberKind.InputObjectField);

        public NameString GetFieldName(IDescriptorContext context, MemberInfo member) =>
            context.Naming.GetMemberName(member, MemberKind.InputObjectField);

        public ITypeReference GetFieldType(IDescriptorContext context, MemberInfo member)
        {
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            if (TryGetTypeOfMember(member, out Type? reflectedType))
            {
                return new ClrTypeReference(reflectedType, TypeContext.Input, Scope);
            }

            throw ThrowHelper.FilterConvention_TypeOfMemberIsUnknown(member);
        }

        public NameString GetOperationDescription(IDescriptorContext context, int operation)
        {
            if (Operations.TryGetValue(operation, out OperationConvention? operationConvention))
            {
                return operationConvention.Description;
            }
            return null;
        }

        public NameString GetOperationName(IDescriptorContext context, int operation)
        {
            if (Operations.TryGetValue(operation, out OperationConvention? operationConvention))
            {
                return operationConvention.Name;
            }
            throw ThrowHelper.FilterConvention_OperationNameNotFound(operation);
        }

        public NameString GetTypeDescription(IDescriptorContext context, Type entityType) =>
            context.Naming.GetTypeDescription(entityType, TypeKind.InputObject);

        public NameString GetTypeName(IDescriptorContext context, Type entityType) =>
            context.Naming.GetTypeName(entityType, TypeKind.Object) + "Filter";

        private bool TryGetTypeOfMember(
            MemberInfo member,
            [NotNullWhen(true)] out Type? type)
        {
            if (member is PropertyInfo p &&
                TryGetTypeOfRuntimeType(p.PropertyType, out type))
            {
                return true;
            }
            if (member is MethodInfo m &&
                TryGetTypeOfRuntimeType(m.ReturnType, out type))
            {
                return true;
            }
            type = null;
            return false;
        }

        private bool TryGetTypeOfRuntimeType(
            Type runtimeType,
            [NotNullWhen(true)] out Type? type)
        {
            Type underlyingType = runtimeType;
            if (runtimeType.IsGenericType
                && System.Nullable.GetUnderlyingType(runtimeType) is { } innerNullableType)
            {
                underlyingType = innerNullableType;
            }

            if (Bindings.TryGetValue(runtimeType, out type))
            {
                return true;
            }

            if (DotNetTypeInfoFactory.IsListType(underlyingType))
            {
                if (!TypeInspector.Default.TryCreate(underlyingType,
                                                     out Utilities.TypeInfo typeInfo))
                {
                    throw new ArgumentException(
                        string.Format("The type {0} is unknown", underlyingType.Name),
                        nameof(underlyingType));
                }

                if (TryGetTypeOfRuntimeType(typeInfo.ClrType, out Type? clrType))
                {
                    type = typeof(ListFilterInput<>).MakeGenericType(clrType);
                    return true;
                }
            }

            if (underlyingType.IsEnum)
            {
                type = typeof(EnumOperationInput<>).MakeGenericType(runtimeType);
                return true;
            }

            if (underlyingType.IsClass)
            {
                type = typeof(FilterInputType<>).MakeGenericType(runtimeType);
                return true;
            }

            type = null;
            return false;
        }

        internal static readonly IFilterConvention Default = null!;

        public IEnumerable<Action<IFilterInputTypeDescriptor>> GetExtensions(
            ITypeReference reference,
            NameString temporaryName)
        {
            // TODO: if this it gonna be the final version we can drop the dicitionaries completely
            foreach (KeyValuePair<ITypeReference, Action<IFilterInputTypeDescriptor>[]> element in
                Extensions)
            {
                if (element.Key.Equals(reference))
                {
                    return element.Value;
                }
                else if (element.Key is SyntaxTypeReference key &&
                  key.Type is NamedTypeNode namedKey &&
                  temporaryName.Value == namedKey.Name.Value)
                {
                    return element.Value;
                }
            }
            return Array.Empty<Action<IFilterInputTypeDescriptor>>();
        }

        public bool TryGetHandler(
            ITypeDiscoveryContext context,
            FilterInputTypeDefinition typeDefinition,
            FilterFieldDefinition fieldDefinition,
            [NotNullWhen(true)] out FilterFieldHandler? handler) =>
            Provider.TryGetHandler(context, typeDefinition, fieldDefinition, out handler);

        public Task ExecuteAsync<TEntityType>(FieldDelegate next, IMiddlewareContext context) =>
            Provider.ExecuteAsync<TEntityType>(next, context);

        public NameString GetArgumentName() => ArgumentName;
    }
}
