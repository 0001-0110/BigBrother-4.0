using BigBrother.CommandHandling.Attributes;
using BigBrother.CommandHandling.CommandRequest;
using BigBrother.Logger;
using BigBrother.Utilities;
using Discord.WebSocket;
using InjectoPatronum;
using UtilityMinistry.Extensions;

namespace BigBrother.CommandHandling
{
    internal abstract class CommandHandlerService<TCommandHandler> : ICommandHandlerService where TCommandHandler : class, ICommandHandler
	{
		protected readonly IDependencyInjector _injector;
		protected readonly ILogger _logger;
		protected readonly IDictionary<string, TCommandHandler> _commandHandlers;

		protected CommandHandlerService(IDependencyInjector injector, ILogger logger)
		{
			_injector = injector;
			_logger = logger;

			// Instantiate all sub command handler that are marked with this class as their parent
			_commandHandlers = new Dictionary<string, TCommandHandler>
			{
				AttributesUtilities.GetAnnotatedClasses<CommandHandlerAttribute>().Select(type =>
				{
                    TCommandHandler? commandHandler = _injector.Instantiate(type) as TCommandHandler;
					ArgumentNullException.ThrowIfNull(commandHandler);
					return KeyValuePair.Create(commandHandler.Name, commandHandler);
				})
			};
		}

		public abstract Task CreateCommands(DiscordSocketClient client);

		public abstract Task ExecuteCommand(ICommandRequest command);
	}
}
