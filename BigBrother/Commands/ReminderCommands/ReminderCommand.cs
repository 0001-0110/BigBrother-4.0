using BigBrother.CommandHandling;
using BigBrother.CommandHandling.Attributes;
using BigBrother.Logger;
using InjectoPatronum;

namespace BigBrother.Commands.ReminderCommands
{
    //[CommandHandler]
    internal class ReminderCommand : SlashCommandHandler
    {
        public override string Name => "reminder";

        public ReminderCommand(IDependencyInjector injector, ILogger logger) : base(injector, logger) { }
    }
}
