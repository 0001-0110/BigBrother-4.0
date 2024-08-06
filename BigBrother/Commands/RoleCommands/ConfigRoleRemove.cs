using BigBrother.CommandHandling;
using BigBrother.CommandHandling.Attributes;
using BigBrother.CommandHandling.CommandRequest;
using BigBrother.Logger;
using Discord;
using InjectoPatronum;
using RogerRoger.DataAccess.Repositories;

namespace BigBrother.Commands.RoleCommands
{
    [SubCommandHandler<ConfigRole>]
    internal class ConfigRoleRemove : SlashSubCommandHandler
    {
        private readonly GuildRoleRepository _guildRoleRepository;

        public override string Name => "remove";
        public override string Description => "Remove a role from being freely available through the `role` command";

        private readonly SlashCommandOption<IRole> _roleOption = new SlashCommandOption<IRole>("role", "The role you want to remove", true);

        public ConfigRoleRemove(IDependencyInjector injector, ILogger logger) : base(injector, logger)
        {
            _guildRoleRepository = injector.Instantiate<GuildRoleRepository>();
        }

        protected override Task Execute(ICommandRequest command, params object[] args)
        {
            _guildRoleRepository.Remove(_roleOption.GetValue(command)!.Id);
            return Task.CompletedTask;
        }
    }
}
