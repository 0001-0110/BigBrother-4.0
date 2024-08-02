using BigBrother.CommandHandling;
using BigBrother.CommandHandling.Attributes;
using BigBrother.Logger;
using InjectoPatronum;

namespace BigBrother.Commands.QuoteCommands
{
    [CommandHandler]
    internal class Quote : SlashCommandHandler
    {
        public override string Name => "quote";

        public Quote(IDependencyInjector injector, ILogger logger) : base(injector, logger) { }
    }
}
