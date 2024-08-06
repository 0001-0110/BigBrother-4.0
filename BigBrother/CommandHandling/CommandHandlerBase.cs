using BigBrother.CommandHandling.Attributes;
using BigBrother.CommandHandling.CommandRequest;
using BigBrother.Logger;
using BigBrother.Utilities;
using InjectoPatronum;
using UtilityMinistry.Extensions;

namespace BigBrother.CommandHandling
{
    internal abstract class CommandHandlerBase<TSubCommandHandler> : ICommandHandlerBase where TSubCommandHandler : class, ISubCommandHandler
    {
        protected readonly ILogger _logger;

        protected readonly IDictionary<string, TSubCommandHandler> _subCommandHandlers;

        public abstract string Name { get; }
        public virtual string Description => "No description provided";

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

        // TODO Change this so that sub command handlers have args but not top command handlers
        /// <summary>
        /// Traverse the tree of commands to find the correct command to execute
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual Task Call(ICommandRequest command, params object[] args)
        {
            if (_subCommandHandlers.Count == 0)
                return Execute(command, args);

            ICommandRequest subCommand = command.GetSubCommand();
            return _subCommandHandlers[subCommand.Name].Call(subCommand, args);
        }

        protected virtual Task Execute(ICommandRequest command, params object[] args)
        {
            // If you encounter this error, you either forgot to add an implementation to your final command, or you did not
            // add correctly the sub command handlers
            _logger.LogError($"Tried to execute command {Name}, that does not have an implementation");
            return Task.CompletedTask;
        }
    }
}
