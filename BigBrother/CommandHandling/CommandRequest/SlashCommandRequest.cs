﻿using Discord.WebSocket;

namespace BigBrother.CommandHandling.CommandRequest
{
    internal class SlashCommandRequest : ICommandRequest
    {
        private readonly SocketSlashCommand _command;
        private readonly SocketSlashCommandDataOption? _subCommand;

        // If this request is a subcommand, the return the name of the subcommand. If not, return the base command name
        public string Name => _subCommand?.Name ?? _command.CommandName;
        public SocketUser Sender => _command.User;
        public SocketGuild? Guild => (_command.Channel as SocketGuildChannel)?.Guild;

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

        public SocketSlashCommandDataOption? GetOption(string name)
        {
            var options = _subCommand?.Options ?? _command.Data.Options;
            // If the option is not required and the user ommit it, then it won't be in the options list
            // In this case, we return null
            return options.FirstOrDefault(option => option.Name == name);
        }

        public ICommandRequest GetSubCommand()
        {
            return new SlashCommandRequest(_command, _subCommand?.Options.First() ?? _command.Data.Options.First());
        }
    }
}