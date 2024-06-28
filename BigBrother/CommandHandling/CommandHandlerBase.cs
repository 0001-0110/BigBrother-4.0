using BigBrother.Utilities;
using InjectoPatronum;

namespace BigBrother.CommandHandling
{
	internal abstract class CommandHandlerBase : ICommandHandler
	{
		private readonly IEnumerable<SubCommandHandler> _subCommandHandlers;

		protected CommandHandlerBase(IDependencyInjector injector)
		{
			_subCommandHandlers = AttributesUtilities.GetAnnotatedClasses<SubCommandHandlerAttribute>(attribute => attribute.Parent == GetType()).Select(type => injector.Instantiate(type) as SubCommandHandler);
		}
	}
}
