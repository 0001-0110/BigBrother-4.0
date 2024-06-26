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
			Console.WriteLine("Hello, World!");

			if (args.Length != 1)
				throw new ArgumentException("I am expecting a single argument: the path of the folder containing all the configuration files");

			IDependencyInjector dependencyInjector = new DependencyInjector();
			dependencyInjector.Map<ILogger, ConsoleLogger>(LogSeverity.Debug);
			dependencyInjector.MapSingleton<IConfigurationService, JsonConfigurationService>(JsonConfigurationService.Load(Path.Combine(args[0], "appsettings.json")));

			BigBrother bot = dependencyInjector.Instantiate<BigBrother>()!;
			await bot.Run();

			return 0;
		}
	}
}
