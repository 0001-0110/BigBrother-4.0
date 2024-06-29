using Discord.WebSocket;

namespace BigBrother.CommandHandling
{
	internal class SlashCommandRequest : ICommandRequest
	{
		private readonly SocketSlashCommand _command;

		public string Name => _command.CommandName;
		public SocketUser Sender => _command.User;

		public SlashCommandRequest(SocketSlashCommand command)
		{
			_command = command;
		}

		public Task Respond(string text)
		{
			return _command.RespondAsync(text);
		}
	}
}
