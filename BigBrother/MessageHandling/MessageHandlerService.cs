using BigBrother.MessageHandling;
using Discord.WebSocket;
using InjectoPatronum;

namespace BigBrother.Messages
{
    // TODO
    // For now, every guild will have all handler enabled, but this should be updated to allow for a customizable experience
    internal class MessageHandlerService : IMessageHandlerService
    {
        private readonly IEnumerable<IMessageHandler> _handlers;

        public MessageHandlerService(IDependencyInjector injector)
        {
            _handlers = [injector.Instantiate<SelfMentionHandler>()];
        }

        public Task HandleMessage(SocketMessage message)
        {
            return Task.WhenAll(_handlers.Select(handler => handler.Handle(message)));
        }
    }
}
