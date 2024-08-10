using BigBrother.Logger;
using Discord.WebSocket;
using InjectoPatronum;
using UtilityMinistry.Extensions;

namespace BigBrother.Messages
{
    // TODO
    // For now, every guild will have all handler enabled, but this should be updated to allow for a customizable experience
    internal class MessageHandlerService : IMessageHandlerService
    {
        private readonly ILogger _logger;

        private readonly IEnumerable<IMessageHandler> _handlers;

        public MessageHandlerService(IDependencyInjector injector, ILogger logger)
        {
            _logger = logger;

            _handlers = typeof(IMessageHandler).GetImplementations().Select(type => injector.Instantiate(type) as IMessageHandler);
        }

        // Send the message to all the message handlers, catching all potential errors at the same time, and do not wait for the handlers to finish
        // Some handlers might take a long time, and waiting for them to finish might block some other operations or cause discord to disconnect
        public Task HandleMessage(SocketMessage message)
        {
            // So that big brother does not start an endless monologue
            if (message.Author.Id == BigBrother.Client.CurrentUser.Id)
                return Task.CompletedTask;

            Task.WhenAll(_handlers.Select(async handler =>
            {
                try
                {
                    await handler.Handle(message);
                }
                catch (Exception exception)
                {
                    await _logger.LogError($"An error occured during the handling of the message ({handler.GetType().Name})", exception);
                }
            }));
            return Task.CompletedTask;
        }
    }
}
