using BigBrother.CommandHandling;
using BigBrother.CommandHandling.Attributes;
using BigBrother.Logger;
using InjectoPatronum;

namespace BigBrother.Commands.HelpCommands
{
    //[CommandHandler]
    internal class Help : SlashCommandHandler
    {
        public override string Name => "help";
        public override string Description => "Stop it, get some help";

        public Help(IDependencyInjector injector, ILogger logger) : base(injector, logger)
        {
        }
    }
}
