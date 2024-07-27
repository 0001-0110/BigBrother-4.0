using BigBrother.CommandHandling.CommandRequest;
using Discord.WebSocket;

namespace BigBrother.CommandHandling
{
    internal interface ICommandHandlerService
	{
		Task CreateCommands(DiscordSocketClient client);

		Task ExecuteCommand(ICommandRequest command);
	}
}
