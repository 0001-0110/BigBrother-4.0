using Discord.WebSocket;

namespace BigBrother.Messages
{
    internal interface IMessageHandler
    {
        Task Handle(SocketMessage message);
    }
}
