using BigBrother.CommandHandling;
using BigBrother.Logger;
using InjectoPatronum;

namespace BigBrother.Commands.Reminder
{
	[CommandHandler]
	internal class Reminder : SlashCommandHandler
	{
		public override string Name => "reminder";
		public override string Description => "Missing description";

		public Reminder(IDependencyInjector injector, ILogger logger) : base(injector, logger) { }
	}
}
