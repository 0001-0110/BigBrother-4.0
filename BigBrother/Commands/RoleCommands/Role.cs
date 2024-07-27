using BigBrother.CommandHandling;
using BigBrother.CommandHandling.Attributes;
using BigBrother.Logger;
using InjectoPatronum;

namespace BigBrother.Commands.RoleCommands
{
    //[CommandHandler]
    internal class Role : SlashCommandHandler
    {
        public override string Name => "role";
        public override string Description => "Roles";

        public Role(IDependencyInjector injector, ILogger logger) : base(injector, logger) { }
    }
}
