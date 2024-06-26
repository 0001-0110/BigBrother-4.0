﻿using BigBrother.Logger;
using InjectoPatronum;

namespace BigBrother.CommandHandling
{
	internal abstract class CommandHandler<TSubCommandHandler> : CommandHandlerBase<TSubCommandHandler>, ICommandHandler where TSubCommandHandler : class, ISubCommandHandler
	{
		protected CommandHandler(IDependencyInjector injector, ILogger logger) : base(injector, logger) { }
	}
}
