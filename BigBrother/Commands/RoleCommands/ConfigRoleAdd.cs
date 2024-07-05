using BigBrother.CommandHandling;
using BigBrother.CommandHandling.Attributes;
using BigBrother.Logger;
using InjectoPatronum;

namespace BigBrother.Commands.RoleCommands
{
    [SubCommandHandler<ConfigRole>]
    internal class ConfigRoleAdd : SlashSubCommandHandler
    {
        public override string Name => "add";
        public override string Description => "Add a role to make it freely available through the `role` command";

        public ConfigRoleAdd(IDependencyInjector injector, ILogger logger) : base(injector, logger) { }
    }
}
