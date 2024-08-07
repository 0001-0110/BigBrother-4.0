using Discord;

namespace BigBrother.Logger
{
    internal class ConsoleLogger : Logger
    {
        public ConsoleLogger(LogSeverity logSeverity) : base(logSeverity) { }

        protected override Task LogInternal(LogSeverity severity, object? message, Exception? exception)
        {
            Console.ForegroundColor = severity switch
            {
                LogSeverity.Critical => ConsoleColor.DarkRed,
                LogSeverity.Error => ConsoleColor.Red,
                LogSeverity.Warning => ConsoleColor.Yellow,
                LogSeverity.Info => ConsoleColor.White,
                LogSeverity.Verbose => ConsoleColor.Blue,
                LogSeverity.Debug => ConsoleColor.Green,
                _ => throw new ArgumentException("How did we get here ?"),
            };
            Console.WriteLine(message);

            if (exception != null)
            {
                Console.Error.WriteLine(exception.ToString());
            }

            Console.ResetColor();
            return Task.CompletedTask;
        }
    }
}
