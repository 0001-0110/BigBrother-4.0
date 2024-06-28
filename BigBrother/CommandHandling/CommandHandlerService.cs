using BigBrother.Utilities;
using InjectoPatronum;

namespace BigBrother.CommandHandling
{
	internal abstract class CommandHandlerService : ICommandHandlerService
	{
		private readonly IEnumerable<CommandHandler> _commandHandlers;

		protected CommandHandlerService(IDependencyInjector injector)
		{
			// Instantiate all classes with the correct attribute
			// TODO Handle possible null values
			_commandHandlers = AttributesUtilities.GetAnnotatedClasses<CommandHandlerAttribute>().Select(type => injector.Instantiate(type) as CommandHandler);
		}
	}
}
