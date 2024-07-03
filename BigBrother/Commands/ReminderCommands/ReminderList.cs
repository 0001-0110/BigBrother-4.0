using BigBrother.CommandHandling;
using BigBrother.CommandHandling.Attributes;
using BigBrother.CommandHandling.CommandRequest;
using BigBrother.Logger;
using InjectoPatronum;

namespace BigBrother.Commands.Reminder
{
    [SubCommandHandler(typeof(Reminder))]
    internal class ReminderList : SlashSubCommandHandler
    {
        public override string Name => "list";
        public override string Description => "List all of your reminders";

        public ReminderList(IDependencyInjector injector, ILogger logger) : base(injector, logger) { }

        protected override Task Execute(ICommandRequest command)
        {
            return command.Respond("Still working on it");
        }
    }
}
