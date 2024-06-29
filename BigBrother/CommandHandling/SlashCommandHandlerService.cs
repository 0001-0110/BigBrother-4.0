using BigBrother.Configuration;
using Discord.WebSocket;
using InjectoPatronum;

namespace BigBrother.CommandHandling
{
	internal class SlashCommandHandlerService : CommandHandlerService<SlashCommandHandler>
	{
		public SlashCommandHandlerService(IDependencyInjector injector) : base(injector) { }

		public override async Task CreateCommands(IGlobalConfig config, DiscordSocketClient client)
		{
			foreach (IGuildConfig guildConfig in config.GuildConfigs)
			{
				SocketGuild guild = client.GetGuild(guildConfig.Id);
				foreach (string command in guildConfig.ActiveCommands)
					await guild.CreateApplicationCommandAsync(_commandHandlers[command].CreateCommand(guild).Build());
			}
		}

		public override Task ExecuteCommand(ICommandRequest command)
		{
			return _commandHandlers[command.Name].Execute(command);
		}
	}
}
