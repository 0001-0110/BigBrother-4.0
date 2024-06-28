using BigBrother.CommandHandling;
using BigBrother.Configuration;
using BigBrother.Logger;
using Discord;
using InjectoPatronum;

namespace BigBrother
{
	internal class Program
	{
		static async Task<int> Main(string[] args)
		{
			if (args.Length != 1)
				throw new ArgumentException("I am expecting a single argument: the path of the folder containing all the configuration files");

			IDependencyInjector dependencyInjector = new DependencyInjector();
			dependencyInjector.MapSingleton<IConfigurationService, JsonConfigurationService>(JsonConfigurationService.Load(Path.Combine(args[0], "appsettings.json")));
			dependencyInjector.Map<ICommandHandlerService, SlashCommandHandlerService>();
			dependencyInjector.Map<ILogger, ConsoleLogger>(LogSeverity.Debug);

			BigBrother bot = dependencyInjector.Instantiate<BigBrother>()!;
			// Graceful exit in case of keyboard interupt
			Console.CancelKeyPress += async (sender, args) => { await bot.Disconnect(); Environment.Exit(0); };
			await bot.Run();

			return 0;
		}
	}
}
