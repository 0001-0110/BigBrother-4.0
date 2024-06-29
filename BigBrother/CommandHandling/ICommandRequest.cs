using Discord.WebSocket;

namespace BigBrother.CommandHandling
{
	internal interface ICommandRequest
	{
		string Name { get; }
		SocketUser Sender { get; }

		Task Respond(string text);
	}
}
