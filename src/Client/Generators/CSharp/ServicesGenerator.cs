using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StrawberryShake.Generators.Descriptors;
using StrawberryShake.Generators.Utilities;
using static StrawberryShake.Generators.Utilities.NameUtils;

namespace StrawberryShake.Generators.CSharp
{
    public class ServicesGenerator
        : CodeGenerator<IServicesDescriptor>
        , IUsesComponents
    {
        public IReadOnlyList<string> Components { get; } = new[]
        {
            WellKnownComponents.DI,
            WellKnownComponents.Http,
            WellKnownComponents.HttpExecutor,
            WellKnownComponents.HttpExecutorPipeline,
            WellKnownComponents.Serializer
        };

        protected override Task WriteAsync(
            CodeWriter writer,
            IServicesDescriptor descriptor,
            ITypeLookup typeLookup) =>
            WriteStaticClassAsync(writer, descriptor.Name, async () =>
            {
                await WriteAddClientAsync(writer, descriptor.Client).ConfigureAwait(false);
                await writer.WriteLineAsync().ConfigureAwait(false);

                await WriteAddSerializersAsync(writer, descriptor.Client).ConfigureAwait(false);
                await writer.WriteLineAsync().ConfigureAwait(false);

                await WriteAddEnumSerializersAsync(writer, descriptor).ConfigureAwait(false);
                await writer.WriteLineAsync().ConfigureAwait(false);

                await WriteAddInputSerializersAsync(writer, descriptor).ConfigureAwait(false);
                await writer.WriteLineAsync().ConfigureAwait(false);

                await WriteAddResultParsersAsync(writer, descriptor).ConfigureAwait(false);
                await writer.WriteLineAsync().ConfigureAwait(false);

                await WriteTryAddDefaultOperationSerializerAsync(writer).ConfigureAwait(false);
                await writer.WriteLineAsync().ConfigureAwait(false);

                await WriteTryAddDefaultHttpPipelineAsync(writer).ConfigureAwait(false);
                await writer.WriteLineAsync().ConfigureAwait(false);

                await WriteClientFactoryAsync(writer, descriptor.Client).ConfigureAwait(false);
                await writer.WriteLineAsync().ConfigureAwait(false);

                await WritePipelineFactoryAsync(writer, descriptor.Client).ConfigureAwait(false);
            });

        private async Task WriteAddSerializersAsync(
            CodeWriter writer,
            IClientDescriptor descriptor)
        {
            await WriteMethodAsync(
                writer,
                "AddDefaultScalarSerializers",
                true,
                async () =>
                {
                    await writer.WriteIndentedLineAsync(
                        "foreach (IValueSerializer serializer in ValueSerializers.All)")
                        .ConfigureAwait(false);
                    await writer.WriteIndentedLineAsync("{").ConfigureAwait(false);

                    using (writer.IncreaseIndent())
                    {
                        await writer.WriteIndentedLineAsync(
                            "serviceCollection.AddSingleton(serializer);")
                            .ConfigureAwait(false);
                    }

                    await writer.WriteIndentedLineAsync("}").ConfigureAwait(false);
                    await writer.WriteLineAsync().ConfigureAwait(false);
                }).ConfigureAwait(false);;
        }

        private async Task WriteAddClientAsync(
            CodeWriter writer,
            IClientDescriptor descriptor)
        {
            await WriteMethodAsync(
                writer,
                $"Add{descriptor.Name}",
                true,
                async () =>
                {
                    await writer.WriteIndentedLineAsync(
                        "serviceCollection.AddSingleton<{0}, {1}>();",
                        GetInterfaceName(descriptor.Name),
                        GetClassName(descriptor.Name))
                        .ConfigureAwait(false);

                    await writer.WriteIndentedLineAsync(
                        "serviceCollection.AddSingleton(sp =>")
                        .ConfigureAwait(false);

                    using (writer.IncreaseIndent())
                    {
                        await writer.WriteIndentedLineAsync(
                            "HttpOperationExecutorBuilder.New()")
                            .ConfigureAwait(false);
                        using (writer.IncreaseIndent())
                        {
                            await writer.WriteIndentedLineAsync(
                                ".AddServices(sp)")
                                .ConfigureAwait(false);
                            await writer.WriteIndentedLineAsync(
                                ".SetClient(ClientFactory)")
                                .ConfigureAwait(false);
                            await writer.WriteIndentedLineAsync(
                                ".SetPipeline(PipelineFactory)")
                                .ConfigureAwait(false);
                            await writer.WriteIndentedLineAsync(
                                ".Build());")
                                .ConfigureAwait(false);
                        }
                    }
                    await writer.WriteLineAsync().ConfigureAwait(false);

                    await writer.WriteIndentedLineAsync(
                        "serviceCollection.AddDefaultScalarSerializers();")
                        .ConfigureAwait(false);
                    await writer.WriteIndentedLineAsync(
                        "serviceCollection.AddEnumSerializers();")
                        .ConfigureAwait(false);
                    await writer.WriteIndentedLineAsync(
                        "serviceCollection.AddInputSerializers();")
                        .ConfigureAwait(false);
                    await writer.WriteIndentedLineAsync(
                        "serviceCollection.AddResultParsers();")
                        .ConfigureAwait(false);

                    await writer.WriteLineAsync().ConfigureAwait(false);

                    await writer.WriteIndentedLineAsync(
                        "serviceCollection.TryAddDefaultOperationSerializer();")
                        .ConfigureAwait(false);
                    await writer.WriteIndentedLineAsync(
                        "serviceCollection.TryAddDefaultHttpPipeline();")
                        .ConfigureAwait(false);
                    await writer.WriteLineAsync().ConfigureAwait(false);
                });
        }

        private async Task WriteAddEnumSerializersAsync(
            CodeWriter writer,
            IServicesDescriptor descriptor)
        {
            await WriteMethodAsync(
                writer,
                "AddEnumSerializers",
                false,
                async () =>
                {
                    foreach (IEnumDescriptor enumType in descriptor.EnumTypes)
                    {
                        await writer.WriteIndentedLineAsync(
                            "serviceCollection.AddSingleton<IValueSerializer, {0}>();",
                            GetClassName(enumType.Name + "ValueSerializer"))
                            .ConfigureAwait(false);
                    }
                }).ConfigureAwait(false);
        }

        private async Task WriteAddInputSerializersAsync(
            CodeWriter writer,
            IServicesDescriptor descriptor)
        {
            await WriteMethodAsync(
                writer,
                "AddInputSerializers",
                false,
                async () =>
                {
                    foreach (IInputClassDescriptor inputType in descriptor.InputTypes)
                    {
                        await writer.WriteIndentedLineAsync(
                            "serviceCollection.AddSingleton<IValueSerializer, {0}>();",
                            GetClassName(inputType.Name + "Serializer"))
                            .ConfigureAwait(false);
                    }
                }).ConfigureAwait(false);;
        }

        private async Task WriteAddResultParsersAsync(
            CodeWriter writer,
            IServicesDescriptor descriptor)
        {
            await WriteMethodAsync(
                writer,
                "AddResultParsers",
                false,
                async () =>
                {
                    foreach (IResultParserDescriptor resultParser in descriptor.ResultParsers)
                    {
                        await writer.WriteIndentedLineAsync(
                            "serviceCollection.AddSingleton<IResultParser, {0}>();",
                            GetClassName(resultParser.Name))
                            .ConfigureAwait(false);
                    }
                })
                .ConfigureAwait(false);
        }

        private async Task WriteTryAddDefaultOperationSerializerAsync(CodeWriter writer)
        {
            await WriteMethodAsync(
                writer,
                "TryAddDefaultOperationSerializer",
                false,
                async () =>
                {
                    await writer.WriteIndentedLineAsync(
                        "serviceCollection.TryAddSingleton<" +
                        "IOperationSerializer, JsonOperationSerializer>();")
                        .ConfigureAwait(false);
                })
                .ConfigureAwait(false);
        }

        private async Task WriteTryAddDefaultHttpPipelineAsync(CodeWriter writer)
        {
            await WriteMethodAsync(
                writer,
                "TryAddDefaultHttpPipeline",
                false,
                async () =>
                {
                    await writer.WriteIndentedLineAsync(
                        "serviceCollection.TryAddSingleton<OperationDelegate>(")
                        .ConfigureAwait(false);
                    using (writer.IncreaseIndent())
                    {
                        await writer.WriteIndentedLineAsync(
                            "sp => HttpPipelineBuilder.New()")
                            .ConfigureAwait(false);
                        using (writer.IncreaseIndent())
                        {
                            await writer.WriteIndentedLineAsync(
                                ".Use<CreateStandardRequestMiddleware>()")
                                .ConfigureAwait(false);
                            await writer.WriteIndentedLineAsync(
                                ".Use<SendHttpRequestMiddleware>()")
                                .ConfigureAwait(false);
                            await writer.WriteIndentedLineAsync(
                                ".Use<ParseSingleResultMiddleware>()")
                                .ConfigureAwait(false);
                            await writer.WriteIndentedLineAsync(
                                ".Build(sp));")
                                .ConfigureAwait(false);
                        }
                    }
                });
        }

        private async Task WriteClientFactoryAsync(
            CodeWriter writer,
            IClientDescriptor descriptor)
        {
            await writer.WriteIndentedLineAsync(
                "private static Func<HttpClient> ClientFactory(IServiceProvider services)")
                .ConfigureAwait(false);
            await writer.WriteIndentedLineAsync("{")
                .ConfigureAwait(false);

            using (writer.IncreaseIndent())
            {
                await writer.WriteIndentedLineAsync(
                    "var clientFactory = services.GetRequiredService<IHttpClientFactory>();")
                    .ConfigureAwait(false);
                await writer.WriteIndentedLineAsync(
                    "return () => clientFactory.CreateClient(\"{0}\");",
                    GetClassName(descriptor.Name))
                    .ConfigureAwait(false);
            }

            await writer.WriteIndentedLineAsync("}").ConfigureAwait(false);
        }

        private async Task WritePipelineFactoryAsync(
            CodeWriter writer,
            IClientDescriptor descriptor)
        {
            await writer.WriteIndentedLineAsync(
                "private static OperationDelegate PipelineFactory(IServiceProvider services)")
                .ConfigureAwait(false);
            await writer.WriteIndentedLineAsync("{").ConfigureAwait(false);

            using (writer.IncreaseIndent())
            {
                await writer.WriteIndentedLineAsync(
                    "return services.GetRequiredService<OperationDelegate>();")
                    .ConfigureAwait(false);
            }

            await writer.WriteIndentedLineAsync("}").ConfigureAwait(false);
        }

        private async Task WriteMethodAsync(
            CodeWriter writer,
            string methodName,
            bool isPublic,
            Func<Task> write)
        {
            await writer.WriteIndentedLineAsync(
                "{0} static IServiceCollection {1}(",
                isPublic ? "public" : "private",
                methodName)
                .ConfigureAwait(false);

            using (writer.IncreaseIndent())
            {
                await writer.WriteIndentedLineAsync(
                    "this IServiceCollection serviceCollection)")
                    .ConfigureAwait(false);
            }

            await writer.WriteIndentedLineAsync("{").ConfigureAwait(false);

            using (writer.IncreaseIndent())
            {
                if (isPublic)
                {
                    await writer.WriteIndentedLineAsync(
                        "if (serviceCollection is null)")
                        .ConfigureAwait(false);

                    await writer.WriteIndentedLineAsync("{").ConfigureAwait(false);

                    using (writer.IncreaseIndent())
                    {
                        await writer.WriteIndentedLineAsync(
                            "throw new ArgumentNullException(nameof(serviceCollection));")
                            .ConfigureAwait(false);
                    }

                    await writer.WriteIndentedLineAsync("}").ConfigureAwait(false);
                    await writer.WriteLineAsync().ConfigureAwait(false);
                }

                await write().ConfigureAwait(false);

                await writer.WriteIndentedLineAsync(
                    "return serviceCollection;")
                    .ConfigureAwait(false);
            }

            await writer.WriteIndentedLineAsync("}").ConfigureAwait(false);
        }

        private static string CreateName(IClientDescriptor descriptor) =>
            descriptor.Name + "ServiceCollectionExtensions";
    }
}
