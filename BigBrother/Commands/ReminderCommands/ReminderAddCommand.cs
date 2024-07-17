using BigBrother.CommandHandling;
using BigBrother.CommandHandling.Attributes;
using BigBrother.CommandHandling.CommandRequest;
using BigBrother.Logger;
using InjectoPatronum;
using RogerRoger.DataAccess;
using RogerRoger.DataAccess.Repositories;
using RogerRoger.Models.ReminderCommand;

namespace BigBrother.Commands.ReminderCommands
{
    [SubCommandHandler<ReminderCommand>]
    internal class ReminderAddCommand : SlashSubCommandHandler
    {
        private readonly ReminderRepository _reminderRepository;

        public override string Name => "add";
        public override string Description => "Create a new reminder";

        private readonly SlashCommandOption<string> _dateOption = new SlashCommandOption<string>("date", "The date", true);
        private readonly SlashCommandOption<string> _textOption = new SlashCommandOption<string>("text", "The text", true);

        public ReminderAddCommand(IDependencyInjector injector, ILogger logger) : base(injector, logger) 
        {
            _reminderRepository = injector.Instantiate<ReminderRepository>();
        }

        protected override Task Execute(ICommandRequest command)
        {
            // TODO Get the real date from the command
            DateTime date = DateTime.Now;
            string text = _textOption.GetValue(command)!;
            _reminderRepository.Add(new Reminder(date, text));
            return command.Respond($"I will remind you {text} the {date}");
        }
    }
}
