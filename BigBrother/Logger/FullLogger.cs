using Discord;
using InjectoPatronum;

namespace BigBrother.Logger
{
	internal class FullLogger : ILogger
	{
		private readonly ILogger _consoleLogger;
		private readonly ILogger _discordLogger;

		public FullLogger(IDependencyInjector injector, LogSeverity severity)
		{
			_consoleLogger = injector.Instantiate<ConsoleLogger>(severity)!;
			_discordLogger = injector.Instantiate<DiscordLogger>(severity)!;
		}

		public async Task Log(LogSeverity severity, object? message, Exception? exception = null)
		{
			await _consoleLogger.Log(severity, message, exception);
			await _discordLogger.Log(severity, message, exception);
		}

		public async Task LogCritical(object? message, Exception? exception = null)
		{
			await _consoleLogger.LogCritical(message, exception);
			await _discordLogger.LogCritical(message, exception);
		}

		public async Task LogError(object? message, Exception? exception = null)
		{
			await _consoleLogger.LogError(message, exception);
			await _discordLogger.LogError(message, exception);
		}

		public async Task LogWarning(object? message, Exception? exception = null)
		{
			await _consoleLogger.LogWarning(message, exception);
			await _discordLogger.LogWarning(message, exception);
		}

		public async Task LogInfo(object? message, Exception? exception = null)
		{
			await _consoleLogger.LogInfo(message, exception);
			await _discordLogger.LogInfo(message, exception);
		}

		public async Task LogVerbose(object? message, Exception? exception = null)
		{
			await _consoleLogger.LogVerbose(message, exception);
			await _discordLogger.LogVerbose(message, exception);
		}

		public async Task LogDebug(object? message, Exception? exception = null)
		{
			await _consoleLogger.LogDebug(message, exception);
			await _discordLogger.LogDebug(message, exception);
		}
	}
}
