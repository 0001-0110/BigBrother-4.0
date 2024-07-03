using BigBrother.CommandHandling;
using BigBrother.CommandHandling.Attributes;
using BigBrother.Logger;
using InjectoPatronum;

namespace BigBrother.Commands.Quote
{
    [CommandHandler]
    internal class Quote : SlashCommandHandler
    {
        public override string Name => "quote";
        public override string Description => "Get a random quote";

        public Quote(IDependencyInjector injector, ILogger logger) : base(injector, logger) { }
    }
}
