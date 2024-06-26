using Discord;

namespace BigBrother.Logger
{
	internal class ConsoleLogger : Logger
	{
		public ConsoleLogger(LogSeverity logSeverity) : base(logSeverity) { }

		protected override Task Log(object? message, Exception? exception = null)
		{
			Console.WriteLine(message);

			if (exception != null)
				Console.Error.WriteLine(exception.Message);

			return Task.CompletedTask;
		}
	}
}
