﻿using BigBrother.CommandHandling;
using BigBrother.Logger;
using InjectoPatronum;

namespace BigBrother.Commands.RoleCommands
{
    //[SubCommandHandler<Config>]
    internal class ConfigRole : SlashSubCommandHandler
    {
        public override string Name => "role";

        public ConfigRole(IDependencyInjector injector, ILogger logger) : base(injector, logger) { }
    }
}
