using InjectoPatronum;

namespace BigBrother.CommandHandling
{
	internal abstract class CommandHandler : CommandHandlerBase
	{
		protected CommandHandler(IDependencyInjector injector) : base(injector) { }
	}
}
