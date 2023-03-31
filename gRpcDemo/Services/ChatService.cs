using System;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using gRpcDemo.Protos;

namespace gRpcDemo.Services
{
	public class ChatService : Chat.ChatBase
	{
        private readonly ILogger<ChatService> _logger;

        public ChatService(ILogger<ChatService> logger)
        {
            _logger = logger;
        }

        public override async Task SendMessage(
            IAsyncStreamReader<ClientToServerMessage> requestStream,
            IServerStreamWriter<ServerToClientMessage> responseStream,
            ServerCallContext context)
        {
            var clientToServerTask =  HandleClientToServerAsync(requestStream, context);
            var serverToClientTask =  HandleServerToClientAsync(responseStream, context);

            await Task.WhenAll(clientToServerTask, serverToClientTask);
        }

        private static async Task HandleServerToClientAsync(IServerStreamWriter<ServerToClientMessage> responseStream, ServerCallContext context)
        {
            var pingCount = 0;
            while (!context.CancellationToken.IsCancellationRequested)
            {
                await responseStream.WriteAsync(new ServerToClientMessage
                {
                    Text = $"Server said hi {++pingCount} times",
                    Timestamp = Timestamp.FromDateTime(DateTime.UtcNow)
                });

            }
        }

        private async Task HandleClientToServerAsync(IAsyncStreamReader<ClientToServerMessage> requestStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext() && !context.CancellationToken.IsCancellationRequested)
            {
                var message = requestStream.Current;
                _logger.LogInformation("Client said: {Text}", message.Text);
            }
        }
    }
}

