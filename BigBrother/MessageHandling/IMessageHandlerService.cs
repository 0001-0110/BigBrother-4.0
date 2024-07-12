using Discord.WebSocket;

namespace BigBrother.Messages
{
    internal interface IMessageHandlerService
    {
        public Task HandleMessage(SocketMessage message);
    }
}
