using Discord;

namespace BigBrother.Logger
{
	internal class DiscordLogger : Logger
	{
		public DiscordLogger(LogSeverity logSeverity) : base(logSeverity)
		{
		}

		protected override Task LogInternal(LogSeverity severity, object? message, Exception? exception = null)
		{
			throw new NotImplementedException();
		}
	}
}
