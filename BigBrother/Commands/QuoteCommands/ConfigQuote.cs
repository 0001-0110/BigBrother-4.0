using BigBrother.CommandHandling;
using BigBrother.CommandHandling.Attributes;
using BigBrother.Commands.ConfigCommands;
using BigBrother.Logger;
using InjectoPatronum;

namespace BigBrother.Commands.QuoteCommands
{
    [SubCommandHandler<Config>()]
    internal class ConfigQuote : SlashSubCommandHandler
    {
        public override string Name => "quote";
        public override string Description => "Quote configuration";

        public ConfigQuote(IDependencyInjector injector, ILogger logger) : base(injector, logger) { }
    }
}
