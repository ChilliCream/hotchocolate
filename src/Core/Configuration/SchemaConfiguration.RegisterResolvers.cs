using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HotChocolate.Internal;
using HotChocolate.Resolvers;
using HotChocolate.Types;

namespace HotChocolate.Configuration
{
    internal partial class SchemaConfiguration
    {
        internal void RegisterResolvers(ISchemaContext schemaContext)
        {
            if (schemaContext == null)
            {
                throw new ArgumentNullException(nameof(schemaContext));
            }

            CompleteDelegateBindings(schemaContext.Types);
            CompleteCollectionBindings(schemaContext.Types);

            HashSet<FieldReference> registeredResolvers = new HashSet<FieldReference>();
            RegisterKnownFieldResolvers(schemaContext);
            TryRegisterMissingResolvers(schemaContext);
        }

        private void CompleteDelegateBindings(ITypeRegistry typeRegistry)
        {
            foreach (ResolverDelegateBindingInfo binding in _resolverBindings
                .OfType<ResolverDelegateBindingInfo>())
            {
                if (binding.ObjectTypeName == null && binding.ObjectType == null)
                {
                    // skip incomplete binding --> todo: maybe an exception?
                    continue;
                }

                if (binding.ObjectTypeName == null)
                {
                    if (typeRegistry.TryGetTypeBinding(
                        binding.ObjectType, out ObjectTypeBinding typeBinding))
                    {
                        FieldBinding fieldBinding = typeBinding?.Fields.Values
                            .FirstOrDefault(t => t.Member == binding.FieldMember);
                        binding.ObjectTypeName = typeBinding.Name;
                        binding.FieldName = fieldBinding?.Name;
                    }
                }
            }
        }

        private void CompleteCollectionBindings(ITypeRegistry typeRegistry)
        {
            foreach (ResolverCollectionBindingInfo binding in _resolverBindings
                .OfType<ResolverCollectionBindingInfo>())
            {
                if (binding.ObjectType == null && binding.ObjectTypeName == null)
                {
                    binding.ObjectType = binding.ResolverType;
                }

                ObjectTypeBinding typeBinding = null;
                if (binding.ObjectType == null && typeRegistry
                    .TryGetTypeBinding(binding.ObjectTypeName, out typeBinding))
                {
                    binding.ObjectType = typeBinding.Type;
                }

                if (binding.ObjectTypeName == null && typeRegistry
                    .TryGetTypeBinding(binding.ObjectType, out typeBinding))
                {
                    binding.ObjectTypeName = typeBinding.Name;
                }

                if (binding.ObjectTypeName == null)
                {
                    binding.ObjectTypeName = binding.ObjectType.GetGraphQLName();
                }

                // TODO : error handling if object type cannot be resolverd
                CompleteFieldResolverBindungs(binding, typeBinding, binding.Fields);
            }
        }

        private void CompleteFieldResolverBindungs(
            ResolverCollectionBindingInfo resolverCollectionBinding,
            ObjectTypeBinding typeBinding,
            IEnumerable<FieldResolverBindungInfo> fieldResolverBindings)
        {
            foreach (FieldResolverBindungInfo binding in
                fieldResolverBindings)
            {
                if (binding.FieldMember == null && binding.FieldName == null)
                {
                    binding.FieldMember = binding.ResolverMember;
                }

                if (binding.FieldMember == null && typeBinding != null
                    && typeBinding.Fields.TryGetValue(
                        binding.FieldName, out FieldBinding fieldBinding))
                {
                    binding.FieldMember = fieldBinding.Member;
                }

                if (binding.FieldName == null && typeBinding != null)
                {
                    fieldBinding = typeBinding.Fields.Values
                        .FirstOrDefault(t => t.Member == binding.FieldMember);
                    binding.FieldName = fieldBinding?.Name;
                }

                // todo : error handling
                if (binding.FieldName == null)
                {
                    binding.FieldName = binding.FieldMember.GetGraphQLName();
                }
            }
        }

        private void RegisterKnownFieldResolvers(ISchemaContext schemaContext)
        {
            ResolverCollectionBindingInfo[] collectionBindings = _resolverBindings
                .OfType<ResolverCollectionBindingInfo>().ToArray();

            IResolverBindingHandler bindingHandler =
                new ResolverCollectionBindingHandler(collectionBindings);
            foreach (ResolverCollectionBindingInfo resolverBinding in collectionBindings)
            {
                bindingHandler.ApplyBinding(schemaContext, resolverBinding);
            }

            bindingHandler = new ResolverDelegateBindingHandler();
            foreach (ResolverDelegateBindingInfo resolverBinding in
                _resolverBindings.OfType<ResolverDelegateBindingInfo>())
            {
                bindingHandler.ApplyBinding(schemaContext, resolverBinding);
            }
        }

        // tries to register resolvers for type bindings that at this point have no explicite resolver.
        private void TryRegisterMissingResolvers(
            ISchemaContext schemaContext)
        {
            FieldResolverDiscoverer discoverer = new FieldResolverDiscoverer();
            foreach (ObjectTypeBinding typeBinding in schemaContext.Types
                .GetTypeBindings().OfType<ObjectTypeBinding>())
            {
                List<FieldResolverMember> missingResolvers = new List<FieldResolverMember>();
                foreach (FieldBinding field in typeBinding.Fields.Values)
                {
                    FieldReference fieldReference = new FieldReference(
                        typeBinding.Name, field.Name);
                    if (!schemaContext.Resolvers.ContainsResolver(fieldReference))
                    {
                        missingResolvers.Add(new FieldResolverMember(
                            typeBinding.Name, field.Name, field.Member));
                    }
                }

                foreach (FieldResolverDescriptor descriptor in discoverer
                    .GetSelectedResolvers(typeBinding.Type, typeBinding.Type,
                        missingResolvers))
                {
                    schemaContext.Resolvers.RegisterResolver(descriptor);
                }
            }
        }
    }
}
