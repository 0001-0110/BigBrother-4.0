using BigBrother.CommandHandling.CommandRequest;
using Discord;

namespace BigBrother.CommandHandling
{
    internal abstract class SlashCommandOption
    {
        protected ApplicationCommandOptionType _type;

        protected readonly string _name;
        protected readonly string _description;
        private readonly bool _isRequired;

        public SlashCommandOption(string name, string description, bool isRequired)
        {
            _name = name;
            _description = description;
            _isRequired = isRequired;
        }

        public SlashCommandOptionBuilder CreateOption()
        {
            return new SlashCommandOptionBuilder()
                .WithName(_name)
                .WithDescription(_description)
                .WithType(_type)
                .WithRequired(_isRequired);
        }
    }

    internal class SlashCommandOption<T> : SlashCommandOption
    {
        public SlashCommandOption(string name, string description, bool isRequired = false) : base(name, description, isRequired)
        {
            if (typeof(T) == typeof(string))
                _type = ApplicationCommandOptionType.String;

            if (typeof(T) == typeof(long))
                _type = ApplicationCommandOptionType.Integer;

            if (typeof(T) == typeof(bool))
                _type = ApplicationCommandOptionType.Boolean;

            if (typeof(T) == typeof(IUser))
                _type = ApplicationCommandOptionType.User;

            if (typeof(T) == typeof(IChannel))
                _type = ApplicationCommandOptionType.Channel;

            if (typeof(T) == typeof(IRole))
                _type = ApplicationCommandOptionType.Role;

            if (typeof(T) == typeof(IMentionable))
                _type = ApplicationCommandOptionType.Mentionable;

            if (typeof(T) == typeof(double))
                _type = ApplicationCommandOptionType.Number;

            if (typeof(T) == typeof(IAttachment))
                _type = ApplicationCommandOptionType.Attachment;
        }

        public T? GetValue(ICommandRequest command)
        {
            return (T?)command.GetOption(_name)?.Value;
        }
    }
}
