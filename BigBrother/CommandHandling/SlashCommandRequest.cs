using Discord.WebSocket;

namespace BigBrother.CommandHandling
{
	internal class SlashCommandRequest : ICommandRequest
	{
		private readonly SocketSlashCommand _command;
		private SocketSlashCommandDataOption? _subCommand;

		// If this request is a subcommand, the return the name of the subcommand. If not, return the base command name
		public string Name => _subCommand?.Name ?? _command.CommandName;
		public SocketUser Sender => _command.User;

		// Create a request
		public SlashCommandRequest(SocketSlashCommand command)
		{
			_command = command;
		}

		// Create a sub request
		private SlashCommandRequest(SocketSlashCommand command, SocketSlashCommandDataOption? subCommand)
		{
			_command = command;
			_subCommand = subCommand;
		}

		public Task Respond(string text)
		{
			return _command.RespondAsync(text);
		}

		public ICommandRequest GetSubCommand()
		{
			return new SlashCommandRequest(_command, _subCommand?.Options.First() ?? _command.Data.Options.First());
		}
	}
}
