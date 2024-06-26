using Discord;

namespace BigBrother.Logger
{
	internal interface ILogger
	{
		Task Log(LogSeverity severity, object? message, Exception? exception = null);

		Task LogCritical(object? message, Exception? exception = null);

		Task LogError(object? message, Exception? exception = null);

		Task LogWarning(object? message, Exception? exception = null);

		Task LogInfo(object? message, Exception? exception = null);

		Task LogVerbose(object? message, Exception? exception = null);

		Task LogDebug(object? message, Exception? exception = null);
	}
}
