using Discord.WebSocket;
using Discord;

namespace BigBrother.CommandHandling.CommandRequest
{
    internal interface ICommandRequest
    {
        string Name { get; }
        SocketUser Sender { get; }

        Task Respond(string text);

        SocketSlashCommandDataOption GetOption(string name);

        ICommandRequest GetSubCommand();
    }
}
