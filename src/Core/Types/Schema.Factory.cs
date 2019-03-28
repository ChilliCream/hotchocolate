﻿using System;
using System.Collections.Generic;
using System.Linq;
using HotChocolate.Configuration;
using HotChocolate.Utilities;
using HotChocolate.Language;
using HotChocolate.Types;

namespace HotChocolate
{
    public partial class Schema
    {
        public static Schema Create(
            string schema,
            Action<ISchemaConfiguration> configure)
        {
            if (string.IsNullOrEmpty(schema))
            {
                throw new ArgumentNullException(nameof(schema));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            return Create(Parser.Default.Parse(schema), configure);
        }

        public static Schema Create(
            DocumentNode schemaDocument,
            Action<ISchemaConfiguration> configure)
        {
            if (schemaDocument == null)
            {
                throw new ArgumentNullException(nameof(schemaDocument));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            SchemaBuilder builder = CreateSchemaBuilder(configure);
            builder.AddDocument(sp => schemaDocument);
            return builder.Create();
        }

        public static Schema Create(
            Action<ISchemaConfiguration> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            SchemaBuilder builder = CreateSchemaBuilder(configure);
            return builder.Create();
        }

        private static SchemaBuilder CreateSchemaBuilder(
            Action<ISchemaConfiguration> configure)
        {
            var configuration = new SchemaConfiguration();
            configure(configuration);

            return configuration.CreateBuilder();
        }
    }
}
