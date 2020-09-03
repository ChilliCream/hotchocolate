using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using HotChocolate.AspNetCore.Subscriptions.Messages;
using HotChocolate.AspNetCore.Utilities;
using Xunit;

namespace HotChocolate.AspNetCore.Subscriptions
{
    public class SubscriptionTestBase : IClassFixture<TestServerFactory>
    {
        public SubscriptionTestBase(TestServerFactory serverFactory)
        {
            ServerFactory = serverFactory;
        }

        protected TestServerFactory ServerFactory { get; }

        protected Uri SubscriptionUri { get; } = new Uri("ws://localhost:5000/graphql");


        protected async Task<IReadOnlyDictionary<string, object>> WaitForMessage(
            WebSocket webSocket, string type, TimeSpan timeout)
        {
            Stopwatch timer = Stopwatch.StartNew();

            try
            {
                while (timer.Elapsed <= timeout)
                {
                    await Task.Delay(50);

                    IReadOnlyDictionary<string, object> message =
                        await webSocket.ReceiveServerMessageAsync();

                    if (message != null && type.Equals(message["type"]))
                    {
                        return message;
                    }

                    if (message != null
                        && !MessageTypes.Connection.KeepAlive.Equals(
                            message["type"]))
                    {
                        throw new InvalidOperationException(
                            $"Unexpected message type: {message["type"]}");
                    }
                }
            }
            finally
            {
                timer.Stop();
            }

            return null;
        }

        protected async Task<WebSocket> ConnectToServerAsync(
            WebSocketClient client)
        {
            WebSocket webSocket = await client.ConnectAsync(
                SubscriptionUri, CancellationToken.None);

            await webSocket.SendConnectionInitializeAsync();

            IReadOnlyDictionary<string, object> message =
                await webSocket.ReceiveServerMessageAsync();
            Assert.NotNull(message);
            Assert.Equal(MessageTypes.Connection.Accept, message["type"]);

            message = await webSocket.ReceiveServerMessageAsync();
            Assert.NotNull(message);
            Assert.Equal(MessageTypes.Connection.KeepAlive, message["type"]);

            return webSocket;
        }

        protected static WebSocketClient CreateWebSocketClient(
            TestServer testServer)
        {
            WebSocketClient client = testServer.CreateWebSocketClient();

            client.ConfigureRequest = r =>
            {
                r.Headers.Add("Sec-WebSocket-Protocol", "graphql-ws");
            };

            return client;
        }
    }
}
