using Discord;

namespace BigBrother.Logger
{
	internal abstract class Logger : ILogger
	{
		private readonly LogSeverity logSeverity;

		public Logger(LogSeverity logSeverity)
		{
			this.logSeverity = logSeverity;
		}

		protected abstract Task LogInternal(LogSeverity severity, object? message, Exception? exception);

        public async Task Log(LogSeverity severity, object? message, Exception? exception = null)
		{
			if (severity > logSeverity)
				return;
			await LogInternal(severity, $"[{DateTime.Now.ToLongTimeString()}] {message}", exception);
		}

		public Task LogCritical(object? message, Exception? exception = null)
		{
			return Log(LogSeverity.Critical, message, exception);
		}

		public Task LogError(object? message, Exception? exception = null)
		{
			return Log(LogSeverity.Error, message, exception);
		}

		public Task LogWarning(object? message, Exception? exception = null)
		{
			return Log(LogSeverity.Warning, message, exception);
		}

		public Task LogInfo(object? message, Exception? exception = null)
		{
			return Log(LogSeverity.Info, message, exception);
		}

		public Task LogVerbose(object? message, Exception? exception = null)
		{
			return Log(LogSeverity.Verbose, message, exception);
		}

		public Task LogDebug(object? message, Exception? exception = null)
		{
			return Log(LogSeverity.Debug, message, exception);
		}
	}
}
