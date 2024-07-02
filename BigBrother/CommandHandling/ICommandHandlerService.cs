using BigBrother.CommandHandling.CommandRequest;
using BigBrother.Configuration;
using Discord.WebSocket;

namespace BigBrother.CommandHandling
{
    internal interface ICommandHandlerService
	{
		Task CreateCommands(IGlobalConfig config, DiscordSocketClient client);

		Task ExecuteCommand(ICommandRequest command);
	}
}
