using BigBrother.CommandHandling.Attributes;
using BigBrother.CommandHandling.CommandRequest;
using BigBrother.Extensions;
using BigBrother.Logger;
using BigBrother.Utilities;
using InjectoPatronum;

namespace BigBrother.CommandHandling
{
    internal abstract class CommandHandlerBase<TSubCommandHandler> : ICommandHandlerBase where TSubCommandHandler : class, ISubCommandHandler
	{
		protected readonly ILogger _logger;

		protected readonly IDictionary<string, TSubCommandHandler> _subCommandHandlers;

		public abstract string Name { get; }
		public abstract string Description { get; }

		protected CommandHandlerBase(IDependencyInjector injector, ILogger logger)
		{
			_logger = logger;

			_subCommandHandlers = new Dictionary<string, TSubCommandHandler>
			{
				AttributesUtilities.GetAnnotatedClasses<SubCommandHandlerAttribute>(attribute => attribute.Parent == GetType()).Select(type =>
				{
					TSubCommandHandler? commandHandler = injector.Instantiate(type) as TSubCommandHandler;
					ArgumentNullException.ThrowIfNull(commandHandler);
					return KeyValuePair.Create(commandHandler.Name, commandHandler);
				})
			};
		}

		/// <summary>
		/// Traverse the tree of commands to find the correct command to execute
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		public Task Call(ICommandRequest command)
		{
			if (_subCommandHandlers.Count == 0)
				return Execute(command);

			ICommandRequest subCommand = command.GetSubCommand();
			return _subCommandHandlers[subCommand.Name].Call(subCommand);
		}

		protected virtual Task Execute(ICommandRequest command)
		{
			// If you encounter this error, you either forgot to add an implementation to your final command, or you did not
			// add correctly the sub command handlers
			_logger.LogError($"Tried to execute command {Name}, that does not have an implementation");
			return Task.CompletedTask;
		}
	}
}
