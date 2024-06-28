using BigBrother.CommandHandling;
using BigBrother.Configuration;
using BigBrother.Logger;
using Discord;
using Discord.WebSocket;
using InjectoPatronum;

namespace BigBrother
{
	internal class BigBrother
	{
		private readonly ICommandHandlerService _commandHandlerService;
		private readonly IConfigurationService _config;
		private readonly ILogger _logger;

		private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
		private readonly DiscordSocketClient _client;
		//private readonly CommandHandlerCollection commandHandlerCollection

		public BigBrother(IDependencyInjector injector, IConfigurationService config, ICommandHandlerService commandHandlerService, ILogger logger)
		{
			_commandHandlerService = commandHandlerService;
			_config = config;
			_logger = logger;

			// TODO We may not need all intents
			_client = new DiscordSocketClient(
				new DiscordSocketConfig() { GatewayIntents = GatewayIntents.All });
			//commandHandlerCollection = injector.Instantiate<CommandHandlerCollection>()!;

			_client.Log += Client_Log;
			_client.Ready += Client_Ready;
			_client.SlashCommandExecuted += Client_SlashCommandExecuted;
			_client.Connected += async () => { await _logger.LogInfo("Connected"); };
			_client.Disconnected += async (exception) => { await _logger.LogInfo("Disconnected", exception); };
			_config = config;
		}

		private async Task Client_Log(LogMessage logMessage)
		{
			await _logger.Log(logMessage.Severity, $"{logMessage.Source}: {logMessage.Message}", logMessage.Exception);
		}

		private async Task Client_Ready()
		{
			await _client.SetStatusAsync(UserStatus.Online);
			await _client.SetGameAsync("you", type: ActivityType.Watching);

			// Technically, it is not necessary to do it again every time the program is run,
			// But it does not create any problems either, so for now I'll do it like that
			//commandHandlerCollection.BuildSlashCommands(client);

			await _client.Rest.DeleteAllGlobalCommandsAsync();

			var command = new SlashCommandBuilder()
				.WithName("hellotest")
				.WithDescription("say hello")
				.AddOption("user", ApplicationCommandOptionType.User, "The user to say hello to");
			await _client.GetGuild(854747950973452288).CreateApplicationCommandAsync(command.Build());

			await _client.Rest.DeleteAllGlobalCommandsAsync();
			var command2 = new SlashCommandBuilder()
				.WithName("truc2")
				.WithDescription("truc")
				.AddOption(new SlashCommandOptionBuilder()
					.WithName("machinbidule")
					.WithDescription("machin")
					.WithType(ApplicationCommandOptionType.SubCommand));

			await _client.GetGuild(854747950973452288).CreateApplicationCommandAsync(command2.Build());
			await _logger.LogDebug("Created all commands");
		}

		private async Task Client_SlashCommandExecuted(SocketSlashCommand command)
		{
			//await commandHandlerCollection.ExecuteCommand(command);
		}

		private async Task Connect()
		{
			await _logger.LogInfo("Connecting request received");

			// TODO Read token from config file
			await _client.LoginAsync(TokenType.Bot, _config.Get<string>("Token"));
			await _client.StartAsync();
		}

		public async Task Run()
		{
			try
			{
				await Connect();
			}
			catch (Exception exception)
			{
				await _logger.LogCritical("Connection failed", exception);
			}

			try
			{
				await Task.Delay(-1, _cancellationTokenSource.Token);
			}
			catch (TaskCanceledException exception)
			{
				await _logger.LogDebug("Interupted loop", exception);
			}
		}

		public async Task Disconnect()
		{
			await _logger.LogInfo("Disconnecting request received");

			await _client.SetStatusAsync(UserStatus.Offline);
			await _client.StopAsync();
			await _client.LogoutAsync();
			// Stop the endless loop that keeps the program alive
			_cancellationTokenSource.Cancel();
		}
	}
}
