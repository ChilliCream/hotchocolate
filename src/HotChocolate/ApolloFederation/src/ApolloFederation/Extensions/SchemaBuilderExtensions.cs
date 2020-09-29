using System;
using HotChocolate.ApolloFederation;

namespace HotChocolate
{
    public static class SchemaBuilderExtensions
    {
        /// <summary>
        /// Adds support to connect to service to an apollo federation gateway.
        /// </summary>
        /// <param name="builder">
        /// The <see cref="ISchemaBuilder"/>.
        /// </param>
        /// <returns>
        /// Returns the <see cref="ISchemaBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <c>null</c>.
        /// </exception>
        public static ISchemaBuilder AddApolloFederation(
            this ISchemaBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.AddType<EntityType>();
            builder.AddType<ExternalDirectiveType>();
            builder.AddType<ProvidesDirectiveType>();
            builder.AddType<KeyDirectiveType>();
            builder.AddType<FieldSetType>();
            builder.AddType<RequiresDirectiveType>();
            builder.TryAddTypeInterceptor<EntityTypeInterceptor>();
            return builder;
        }
    }
}
