using System;
using Microsoft.Extensions.DependencyInjection;
using StrawberryShake.Transport.WebSockets;

namespace StrawberryShake
{
    /// <summary>
    /// Common extensions of <see cref="IClientBuilder"/> for <see cref="WebSocketConnection"/>
    /// </summary>
    public static class WebSocketClientBuilderExtensions
    {
        /// <summary>
        /// Adds the <see cref="ISocketClientFactory"/> and related services to the
        /// <see cref="IServiceCollection"/> and configures a <see cref="WebSocketClient"/>
        /// with the correct name
        /// </summary>
        /// <param name="clientBuilder">
        /// The <see cref="IClientBuilder"/>
        /// </param>
        /// <param name="configureClient">
        /// A delegate that is used to configure an <see cref="WebSocketClient"/>.
        /// </param>
        public static IClientBuilder ConfigureWebSocketClient(
            this IClientBuilder clientBuilder,
            Action<ISocketClient> configureClient)
        {
            if (clientBuilder == null)
            {
                throw new ArgumentNullException(nameof(clientBuilder));
            }

            if (configureClient == null)
            {
                throw new ArgumentNullException(nameof(configureClient));
            }

            clientBuilder.Services.AddWebSocketClient(clientBuilder.ClientName, configureClient);
            return clientBuilder;
        }

        /// <summary>
        /// Adds the <see cref="ISocketClientFactory"/> and related services to the
        /// <see cref="IServiceCollection"/> and configures a <see cref="WebSocketClient"/>
        /// with the correct name
        /// </summary>
        /// <param name="clientBuilder">
        /// The <see cref="IClientBuilder"/>
        /// </param>
        /// <param name="configureClient">
        /// A delegate that is used to configure an <see cref="WebSocketClient"/>.
        /// </param>
        public static IClientBuilder ConfigureWebSocketClient(
            this IClientBuilder clientBuilder,
            Action<IServiceProvider, ISocketClient> configureClient)
        {
            if (clientBuilder == null)
            {
                throw new ArgumentNullException(nameof(clientBuilder));
            }

            if (configureClient == null)
            {
                throw new ArgumentNullException(nameof(configureClient));
            }

            clientBuilder.Services.AddWebSocketClient(clientBuilder.ClientName, configureClient);
            return clientBuilder;
        }
    }
}
