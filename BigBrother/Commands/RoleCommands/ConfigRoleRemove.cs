using BigBrother.CommandHandling;
using BigBrother.CommandHandling.Attributes;
using BigBrother.Logger;
using InjectoPatronum;

namespace BigBrother.Commands.RoleCommands
{
    [SubCommandHandler<ConfigRole>]
    internal class ConfigRoleRemove : SlashSubCommandHandler
    {
        public override string Name => "remove";
        public override string Description => "Remove a role from being freely available through the `role` command";

        public ConfigRoleRemove(IDependencyInjector injector, ILogger logger) : base(injector, logger) { }
    }
}
