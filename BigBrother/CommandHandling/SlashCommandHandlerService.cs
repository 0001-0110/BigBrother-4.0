using InjectoPatronum;

namespace BigBrother.CommandHandling
{
	internal class SlashCommandHandlerService : CommandHandlerService
	{
		public SlashCommandHandlerService(IDependencyInjector injector) : base(injector) { }
	}
}
