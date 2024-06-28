using BigBrother.CommandHandling;
using InjectoPatronum;

namespace BigBrother.Commands.Hello
{
	[CommandHandler]
	internal sealed class HelloCommandHandler : CommandHandler
	{
		public HelloCommandHandler(IDependencyInjector injector) : base(injector) { }
	}
}
