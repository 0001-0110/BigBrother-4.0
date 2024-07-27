using BigBrother.CommandHandling;
using BigBrother.CommandHandling.Attributes;
using BigBrother.CommandHandling.CommandRequest;
using BigBrother.Logger;
using Discord;
using InjectoPatronum;
using RogerRoger.DataAccess.Repositories;
using RogerRoger.Models.Settings;

namespace BigBrother.Commands.RoleCommands
{
    [SubCommandHandler<ConfigRole>]
    internal class ConfigRoleAdd : SlashSubCommandHandler
    {
        private readonly GuildSettingsRepository _guildSettingsRepository;
        private readonly GuildRoleRepository _guildRoleRepository;

        public override string Name => "add";
        public override string Description => "Add a role to make it freely available through the `role` command";

        private readonly SlashCommandOption<IRole> _roleOption = new SlashCommandOption<IRole>("role", "The role you want to add", true);

        public ConfigRoleAdd(IDependencyInjector injector, ILogger logger) : base(injector, logger)
        {
            _guildSettingsRepository = injector.Instantiate<GuildSettingsRepository>();
            _guildRoleRepository = injector.Instantiate<GuildRoleRepository>();
        }

        protected override Task Execute(ICommandRequest command)
        {
            IRole role = _roleOption.GetValue(command)!;
            _guildRoleRepository.Add(new GuildRole(_guildSettingsRepository.GetOrCreate(command.Guild.Id), role.Id));
            command.Respond($"Role {role.Name} was added");
            return Task.CompletedTask;
        }
    }
}
