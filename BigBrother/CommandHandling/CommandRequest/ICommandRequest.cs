using Discord.WebSocket;

namespace BigBrother.CommandHandling.CommandRequest
{
    internal interface ICommandRequest
    {
        string Name { get; }
        SocketUser Sender { get; }
        public SocketGuild? Guild { get; }

        Task Respond(string text);

        SocketSlashCommandDataOption? GetOption(string name);

        ICommandRequest GetSubCommand();
    }
}
