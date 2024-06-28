using InjectoPatronum;

namespace BigBrother.CommandHandling
{
	internal abstract class SlashCommandHandler : CommandHandler
	{
		protected SlashCommandHandler(IDependencyInjector injector) : base(injector) { }
	}
}
