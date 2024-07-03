using BigBrother.CommandHandling.CommandRequest;
using BigBrother.Configuration;
using BigBrother.Logger;
using Discord.WebSocket;
using InjectoPatronum;

namespace BigBrother.CommandHandling
{
    internal class SlashCommandHandlerService : CommandHandlerService<SlashCommandHandler>
	{
		public SlashCommandHandlerService(IDependencyInjector injector, ILogger logger) : base(injector, logger) { }

        public override async Task CreateCommands(IGlobalConfig config, DiscordSocketClient client)
		{
			foreach (IGuildConfig guildConfig in config.GuildConfigs)
			{
				// Test if the configuration files mentions that all commands should be active
				IEnumerable<string> activeCommands = guildConfig.ActiveCommands.Contains("all") ? _commandHandlers.Keys : guildConfig.ActiveCommands;

				await client.GetGuild(guildConfig.Id).BulkOverwriteApplicationCommandAsync(activeCommands.Select(command =>
				{
					if (!_commandHandlers.TryGetValue(command, out SlashCommandHandler? commandHandler))
					{
						_logger.LogError($"The guild {client.GetGuild(guildConfig.Id).Name} ({guildConfig.Id}) requested a command that does not exist ({command})");
						return null;
					}

					return commandHandler.CreateCommand().Build();
				}).Where(commandHandler => commandHandler != null).ToArray());
			}
		}

		public override Task ExecuteCommand(ICommandRequest command)
		{
			return _commandHandlers[command.Name].Call(command);
		}
	}
}
