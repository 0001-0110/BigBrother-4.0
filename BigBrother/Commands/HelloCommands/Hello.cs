using BigBrother.CommandHandling;
using BigBrother.CommandHandling.Attributes;
using BigBrother.CommandHandling.CommandRequest;
using BigBrother.Logger;
using Discord;
using InjectoPatronum;

namespace BigBrother.Commands.HelloCommands
{
    [CommandHandler]
    internal sealed class Hello : SlashCommandHandler
    {
        public override string Name => "hello";
        public override string Description => "Because being polite is always important";

        private readonly SlashCommandOption<IUser> _userOption = new SlashCommandOption<IUser>("user", "Who you want to greet");

        public Hello(IDependencyInjector injector, ILogger logger) : base(injector, logger) { }

        protected override Task Execute(ICommandRequest command)
        {
            IUser user = _userOption.GetValue(command) ?? command.Sender;
            return command.Respond($"Hello {MentionUtils.MentionUser(user.Id)}");
        }
    }
}
