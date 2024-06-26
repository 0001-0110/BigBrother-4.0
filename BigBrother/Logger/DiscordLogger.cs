using Discord;

namespace BigBrother.Logger
{
	internal class DiscordLogger : Logger
	{
		public DiscordLogger(LogSeverity logSeverity) : base(logSeverity)
		{
		}

		protected override Task Log(object? message, Exception? exception = null)
		{
			throw new NotImplementedException();
		}
	}
}
