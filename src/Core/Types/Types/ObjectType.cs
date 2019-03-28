﻿using System;
using System.Collections.Generic;
using System.Linq;
using HotChocolate.Configuration;
using HotChocolate.Language;
using HotChocolate.Resolvers;
using HotChocolate.Types.Descriptors;
using HotChocolate.Types.Descriptors.Definitions;
using HotChocolate.Types.Introspection;

namespace HotChocolate.Types
{
    public class ObjectType
        : NamedTypeBase<ObjectTypeDefinition>
        , IComplexOutputType
        , IHasClrType
        , IHasSyntaxNode
    {
        private readonly Dictionary<NameString, InterfaceType> _interfaces =
            new Dictionary<NameString, InterfaceType>();
        private readonly Action<IObjectTypeDescriptor> _configure;
        private IsOfType _isOfType;

        protected ObjectType()
        {
            _configure = Configure;
        }

        public ObjectType(Action<IObjectTypeDescriptor> configure)
        {
            _configure = configure;
        }

        public override TypeKind Kind => TypeKind.Object;

        public ObjectTypeDefinitionNode SyntaxNode { get; private set; }

        ISyntaxNode IHasSyntaxNode.SyntaxNode => SyntaxNode;

        public IReadOnlyDictionary<NameString, InterfaceType> Interfaces =>
            _interfaces;

        public FieldCollection<ObjectField> Fields { get; private set; }

        IFieldCollection<IOutputField> IComplexOutputType.Fields => Fields;

        public bool IsOfType(IResolverContext context, object resolverResult)
            => _isOfType(context, resolverResult);

        #region Initialization

        protected override ObjectTypeDefinition CreateDefinition(
            IInitializationContext context)
        {
            ObjectTypeDescriptor descriptor = ObjectTypeDescriptor.New(
                DescriptorContext.Create(context.Services),
                GetType());
            _configure(descriptor);
            return descriptor.CreateDefinition();
        }

        protected virtual void Configure(IObjectTypeDescriptor descriptor) { }

        protected override void OnRegisterDependencies(
            IInitializationContext context,
            ObjectTypeDefinition definition)
        {
            base.OnRegisterDependencies(context, definition);

            context.RegisterDependencyRange(
                definition.GetDependencies(),
                TypeDependencyKind.Default);

            foreach (ObjectFieldDefinition field in definition.Fields)
            {
                if (field.Member != null)
                {
                    context.RegisterResolver(
                        field.Name,
                        field.Member,
                        definition.ClrType,
                        field.ResolverType);
                }
            }
        }

        protected override void OnCompleteType(
            ICompletionContext context,
            ObjectTypeDefinition definition)
        {
            base.OnCompleteType(context, definition);

            _isOfType = definition.IsOfType;
            SyntaxNode = definition.SyntaxNode;

            var fields = new List<ObjectField>();
            AddIntrospectionFields(context, fields);
            fields.AddRange(definition.Fields.Select(t => new ObjectField(t)));

            Fields = new FieldCollection<ObjectField>(fields);

            CompleteInterfaces(context, definition);
            FieldInitHelper.CompleteFields(context, definition, Fields);
            ValidateInterfaceImplementation(context);
        }

        private void AddIntrospectionFields(
            ICompletionContext context,
            ICollection<ObjectField> fields)
        {
            IDescriptorContext descriptorContext =
                DescriptorContext.Create(context.Services);

            if (context.IsQueryType ?? false)
            {
                fields.Add(new __SchemaField(descriptorContext));
                fields.Add(new __TypeField(descriptorContext));
            }

            fields.Add(new __TypeNameField(descriptorContext));
        }

        private void CompleteInterfaces(
            ICompletionContext context,
            ObjectTypeDefinition definition)
        {
            if (ClrType != typeof(object))
            {
                Type[] possibleInterfaceTypes = ClrType.GetInterfaces();
                foreach (Type interfaceType in ClrType.GetInterfaces())
                {
                    if (context.TryGetType<InterfaceType>(
                        new ClrTypeReference(interfaceType, TypeContext.Output),
                        out InterfaceType type))
                    {
                        _interfaces[type.Name] = type;
                    }
                }
            }

            foreach (ITypeReference interfaceRef in definition.Interfaces)
            {
                if (!context.TryGetType<InterfaceType>(
                    interfaceRef,
                    out InterfaceType type))
                {
                    context.ReportError(SchemaErrorBuilder.New()
                        .SetMessage(
                           "COULD NOT RESOLVE INTERFACE")
                        .SetCode(TypeErrorCodes.MissingType)
                        .SetTypeSystemObject(this)
                        .AddSyntaxNode(SyntaxNode)
                        .Build());
                }

                _interfaces[type.Name] = type;
            }
        }

        private void ValidateInterfaceImplementation(
            ICompletionContext context)
        {
            if (_interfaces.Count > 0)
            {
                foreach (IGrouping<NameString, InterfaceField> fieldGroup in
                    _interfaces.Values
                        .SelectMany(t => t.Fields)
                        .GroupBy(t => t.Name))
                {
                    ValidateField(context, fieldGroup);
                }
            }
        }

        private void ValidateField(
            ICompletionContext context,
            IGrouping<NameString, InterfaceField> interfaceField)
        {
            InterfaceField first = interfaceField.First();
            if (ValidateInterfaceFieldGroup(context, first, interfaceField))
            {
                ValidateObjectField(context, first);
            }
        }

        private bool ValidateInterfaceFieldGroup(
            ICompletionContext context,
            InterfaceField first,
            IGrouping<NameString, InterfaceField> interfaceField)
        {
            if (interfaceField.Count() == 1)
            {
                return true;
            }

            foreach (InterfaceField field in interfaceField)
            {
                if (!field.Type.IsEqualTo(first.Type))
                {
                    // TODO : RESOURCES
                    context.ReportError(SchemaErrorBuilder.New()
                        .SetMessage(
                           "The return type of the interface field " +
                            $"{first.Name} from interface " +
                            $"{first.DeclaringType.Name} and " +
                            $"{field.DeclaringType.Name} do not match " +
                            $"and are implemented by object type {Name}.")
                        .SetCode(TypeErrorCodes.MissingType)
                        .SetTypeSystemObject(this)
                        .AddSyntaxNode(SyntaxNode)
                        .Build());
                    return false;
                }

                if (!ArgumentsAreEqual(field.Arguments, first.Arguments))
                {
                    // TODO : RESOURCES
                    context.ReportError(SchemaErrorBuilder.New()
                        .SetMessage(
                            $"The arguments of the interface field {first.Name} " +
                            $"from interface {first.DeclaringType.Name} and " +
                            $"{field.DeclaringType.Name} do not match " +
                            $"and are implemented by object type {Name}.")
                        .SetCode(TypeErrorCodes.MissingType)
                        .SetTypeSystemObject(this)
                        .AddSyntaxNode(SyntaxNode)
                        .Build());
                    return false;
                }
            }

            return true;
        }

        private void ValidateObjectField(
            ICompletionContext context,
            InterfaceField first)
        {
            if (Fields.TryGetField(first.Name, out ObjectField field))
            {
                if (!field.Type.IsEqualTo(first.Type))
                {
                    // TODO : RESOURCES
                    context.ReportError(SchemaErrorBuilder.New()
                        .SetMessage(
                            "The return type of the interface field " +
                            $"{first.Name} does not match the field declared " +
                            $"by object type {Name}.")
                        .SetCode(TypeErrorCodes.MissingType)
                        .SetTypeSystemObject(this)
                        .AddSyntaxNode(SyntaxNode)
                        .Build());
                }

                if (!ArgumentsAreEqual(field.Arguments, first.Arguments))
                {
                    // TODO : RESOURCES
                    context.ReportError(SchemaErrorBuilder.New()
                        .SetMessage(
                            $"Object type {Name} does not implement " +
                            $"all arguments of field {first.Name} " +
                            $"from interface {first.DeclaringType.Name}.")
                        .SetCode(TypeErrorCodes.MissingType)
                        .SetTypeSystemObject(this)
                        .AddSyntaxNode(SyntaxNode)
                        .Build());
                }
            }
            else
            {
                // TODO : RESOURCES
                context.ReportError(SchemaErrorBuilder.New()
                    .SetMessage(
                        $"Object type {Name} does not implement the " +
                        $"field {first.Name} " +
                        $"from interface {first.DeclaringType.Name}.")
                    .SetCode(TypeErrorCodes.MissingType)
                    .SetTypeSystemObject(this)
                    .AddSyntaxNode(SyntaxNode)
                    .Build());
            }
        }

        private bool ArgumentsAreEqual(
            FieldCollection<Argument> x,
            FieldCollection<Argument> y)
        {
            if (x.Count != y.Count)
            {
                return false;
            }

            foreach (Argument xfield in x)
            {
                if (!y.TryGetField(xfield.Name, out Argument yfield)
                    || !xfield.Type.IsEqualTo(yfield.Type))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
