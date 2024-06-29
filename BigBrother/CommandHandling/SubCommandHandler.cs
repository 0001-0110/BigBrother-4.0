using InjectoPatronum;

namespace BigBrother.CommandHandling
{
	internal abstract class SubCommandHandler<TSubCommandHandler> : CommandHandlerBase<TSubCommandHandler>, ISubCommandHandler where TSubCommandHandler : class, ISubCommandHandler
	{
		protected SubCommandHandler(IDependencyInjector injector) : base(injector) { }
	}
}
