using BigBrother.CommandHandling.Attributes;
using BigBrother.CommandHandling;
using BigBrother.Logger;
using InjectoPatronum;
using BigBrother.CommandHandling.CommandRequest;
using Discord;
using RogerRoger.DataAccess.Repositories;

namespace BigBrother.Commands.RoleCommands
{
    [SubCommandHandler<Role>]
    internal class RoleRemove : SlashSubCommandHandler
    {
        private readonly GuildSettingsRepository _guildSettingsRepository;

        public override string Name => "remove";
        public override string Description => "Remove your role";

        private readonly SlashCommandOption<IRole> _roleOption = new SlashCommandOption<IRole>("role", "The role you want to remove", true);

        public RoleRemove(IDependencyInjector injector, ILogger logger) : base(injector, logger)
        {
            _guildSettingsRepository = injector.Instantiate<GuildSettingsRepository>();
        }

        protected override async Task Execute(ICommandRequest command)
        {
            IRole role = _roleOption.GetValue(command)!;

            if (!_guildSettingsRepository.GetById(command.Guild.Id).Roles.Any(guildRole => guildRole.RoleId == role.Id))
            {
                await command.Respond("This role is not freely available");
                return;
            }

            await (command.Sender as IGuildUser)!.RemoveRoleAsync(role);
            await command.Respond($"Role {role.Name} was removed");
        }
    }
}
