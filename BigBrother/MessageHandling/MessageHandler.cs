using BigBrother.Logger;
using BigBrother.Messages;
using Discord.WebSocket;

namespace BigBrother.MessageHandling
{
    internal abstract class MessageHandler : IMessageHandler
    {
        protected readonly ILogger _logger;

        protected MessageHandler(ILogger logger)
        {
            _logger = logger;
        }

        public abstract Task Handle(SocketMessage message);
    }
}
