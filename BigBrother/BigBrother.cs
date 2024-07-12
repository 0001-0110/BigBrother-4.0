using BigBrother.CommandHandling;
using BigBrother.CommandHandling.CommandRequest;
using BigBrother.Configuration;
using BigBrother.Logger;
using BigBrother.Messages;
using Discord;
using Discord.WebSocket;
using InjectoPatronum;

namespace BigBrother
{
	internal class BigBrother
	{
		public static DiscordSocketClient Client { get; private set; }

        private readonly IConfigurationService _configurationService;
		private readonly ICommandHandlerService _commandHandlerService;
		private readonly IMessageHandlerService _messageHandlerService;
		private readonly ILogger _logger;

        private readonly IGlobalConfig _config;

		private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
		//private readonly CommandHandlerCollection commandHandlerCollection

		public BigBrother(IDependencyInjector injector, IConfigurationService configurationService, ICommandHandlerService commandHandlerService, IMessageHandlerService messageHandlerService, ILogger logger)
		{
            _configurationService = configurationService;
			_commandHandlerService = commandHandlerService;
			_messageHandlerService = messageHandlerService;
			_logger = logger;

			_config = configurationService.Load();

			// TODO We may not need all intents
			Client = new DiscordSocketClient(
				new DiscordSocketConfig() { GatewayIntents = GatewayIntents.All });
			//commandHandlerCollection = injector.Instantiate<CommandHandlerCollection>()!;

			Client.Log += Client_Log;
			Client.Ready += Client_Ready;
			Client.SlashCommandExecuted += Client_SlashCommandExecuted;
			Client.MessageReceived += Client_MessageReceived;
			Client.Connected += async () => { await _logger.LogInfo("Connected"); };
			Client.Disconnected += async (exception) => { await _logger.LogInfo("Disconnected", exception); };
		}

		private async Task Client_Log(LogMessage logMessage)
		{
			await _logger.Log(logMessage.Severity, $"{logMessage.Source}: {logMessage.Message}", logMessage.Exception);
		}

		private async Task Client_Ready()
		{
			await Client.SetStatusAsync(UserStatus.Online);
			await Client.SetGameAsync("you", type: ActivityType.Watching);

			// Technically, it is not necessary to do it again every time the program is run,
			// But it does not create any problems either, so for now I'll do it like that
			//commandHandlerCollection.BuildSlashCommands(client);

			await _commandHandlerService.CreateCommands(_config, Client);
		}

		private Task Client_SlashCommandExecuted(SocketSlashCommand command)
		{
			return _commandHandlerService.ExecuteCommand(new SlashCommandRequest(command));
		}

		private Task Client_MessageReceived(SocketMessage message)
		{
			return _messageHandlerService.HandleMessage(message);
		}

		private async Task Connect()
		{
			await _logger.LogInfo("Connecting request received");

			// TODO Read token from config file
			await Client.LoginAsync(TokenType.Bot, _config.Token);
			await Client.StartAsync();
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
				await _logger.LogDebug("Interrupted loop", exception);
			}
		}

		public async Task Disconnect()
		{
			await _logger.LogInfo("Disconnecting request received");

			await Client.SetStatusAsync(UserStatus.Offline);
			await Client.StopAsync();
			await Client.LogoutAsync();

			// Stop the endless loop that keeps the program alive
			_cancellationTokenSource.Cancel();
		}
	}
}
