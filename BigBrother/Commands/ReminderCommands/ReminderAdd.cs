using BigBrother.CommandHandling;
using BigBrother.CommandHandling.Attributes;
using BigBrother.CommandHandling.CommandRequest;
using BigBrother.Logger;
using InjectoPatronum;

namespace BigBrother.Commands.Reminder
{
    [SubCommandHandler<Reminder>()]
	internal class ReminderAdd : SlashSubCommandHandler
	{
		public override string Name => "add";
		public override string Description => "Create a new reminder";

		public ReminderAdd(IDependencyInjector injector, ILogger logger) : base(injector, logger) { }

		protected override Task Execute(ICommandRequest command)
		{
			return command.Respond("Still working on it");
		}
	}
}
