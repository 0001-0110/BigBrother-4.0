using BigBrother.Configuration;
using BigBrother.Logger;
using Discord;
using Discord.WebSocket;
using InjectoPatronum;

namespace BigBrother
{
	internal class BigBrother
	{
		private readonly IConfigurationService _config;
		private readonly ILogger _logger;

		private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
		private readonly DiscordSocketClient _client;
		//private readonly CommandHandlerCollection commandHandlerCollection

		public BigBrother(IDependencyInjector injector, IConfigurationService config, ILogger logger)
		{
			_config = config;
			_logger = logger;

			// TODO We may not need all intents
			_client = new DiscordSocketClient(
				new DiscordSocketConfig() { GatewayIntents = GatewayIntents.All });
			//commandHandlerCollection = injector.Instantiate<CommandHandlerCollection>()!;

			_client.Log += Client_Log;
			_client.Ready += Client_Ready;
			_client.SlashCommandExecuted += Client_SlashCommandExecuted;
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

			await _logger.LogInfo("Succesfully connected");
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

			await Task.Delay(-1, _cancellationTokenSource.Token);
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
