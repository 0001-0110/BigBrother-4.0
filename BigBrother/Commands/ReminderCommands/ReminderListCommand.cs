using BigBrother.CommandHandling;
using BigBrother.CommandHandling.Attributes;
using BigBrother.CommandHandling.CommandRequest;
using BigBrother.Logger;
using InjectoPatronum;

namespace BigBrother.Commands.ReminderCommands
{
    [SubCommandHandler<ReminderCommand>()]
    internal class ReminderListCommand : SlashSubCommandHandler
    {
        public override string Name => "list";
        public override string Description => "List all of your reminders";

        public ReminderListCommand(IDependencyInjector injector, ILogger logger) : base(injector, logger) { }

        protected override Task Execute(ICommandRequest command, params object[] args)
        {
            return command.Respond("Still working on it");
        }
    }
}
