using BigBrother.Utilities;
using InjectoPatronum;

namespace BigBrother.CommandHandling
{
	internal abstract class CommandHandlerBase<TSubCommandHandler> : ICommandHandlerBase where TSubCommandHandler : class, ISubCommandHandler
	{
		protected readonly IEnumerable<TSubCommandHandler> _subCommandHandlers;

		public abstract string Name { get; }
		public abstract string Description { get; }

		protected CommandHandlerBase(IDependencyInjector injector)
		{
			_subCommandHandlers = AttributesUtilities.GetAnnotatedClasses<SubCommandHandlerAttribute>(attribute => attribute.Parent == GetType()).Select(type => injector.Instantiate(type) as TSubCommandHandler);
		}

		public abstract Task Execute(ICommandRequest command);
	}
}
