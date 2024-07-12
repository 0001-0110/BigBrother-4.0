using BigBrother.CommandHandling;
using BigBrother.Logger;
using BigBrother.Configuration;
using Discord;
using InjectoPatronum;
using BigBrother.Messages;

namespace BigBrother
{
	internal class Program
	{
		static async Task<int> Main(string[] args)
		{
			if (args.Length != 1)
				throw new ArgumentException("I am expecting a single argument: the path of the folder containing all the configuration files");

			IDependencyInjector injector = new DependencyInjector()
				.MapSingleton<ILogger, ConsoleLogger>(LogSeverity.Debug)
				.MapSingleton<IConfigurationService, JsonConfigurationService>(Path.Combine(args[0], "appsettings.json"))
				.MapSingleton<ICommandHandlerService, SlashCommandHandlerService>()
				.MapSingleton<IMessageHandlerService, MessageHandlerService>();

			BigBrother bot = injector.Instantiate<BigBrother>()!;
			// Graceful exit in case of keyboard interupt
			Console.CancelKeyPress += async (sender, args) => { await bot.Disconnect(); Environment.Exit(0); };
			await bot.Run();

			return 0;
		}
	}
}
