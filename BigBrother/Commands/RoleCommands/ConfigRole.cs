using BigBrother.CommandHandling;
using BigBrother.CommandHandling.Attributes;
using BigBrother.Commands.ConfigCommands;
using BigBrother.Logger;
using InjectoPatronum;

namespace BigBrother.Commands.RoleCommands
{
    //[SubCommandHandler<Config>]
    internal class ConfigRole : SlashSubCommandHandler
    {
        public override string Name => "role";
        public override string Description => "Handle role configuration";

        public ConfigRole(IDependencyInjector injector, ILogger logger) : base(injector, logger) { }
    }
}
