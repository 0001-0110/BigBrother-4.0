using BigBrother.CommandHandling;
using BigBrother.CommandHandling.Attributes;
using BigBrother.CommandHandling.CommandRequest;
using BigBrother.Logger;
using Discord;
using InjectoPatronum;
using RogerRoger.DataAccess.Repositories;

namespace BigBrother.Commands.ConfigCommands
{
    [CommandHandler]
    internal class ConfigCommand : SlashCommandHandler
    {
        private readonly GuildSettingsRepository _guildSettingsRepository;

        public override string Name => "config";

        public ConfigCommand(IDependencyInjector injector, ILogger logger) : base(injector, logger) { }

        public override SlashCommandBuilder CreateCommand()
        {
            return base.CreateCommand().WithDefaultMemberPermissions(GuildPermission.Administrator);
        }

        public override Task Call(ICommandRequest command, params object[] args)
        {
            return base.Call(command, _guildSettingsRepository.GetOrCreate(command.Guild.Id));
        }
    }
}
