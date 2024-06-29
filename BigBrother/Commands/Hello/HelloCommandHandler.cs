using BigBrother.CommandHandling;
using Discord;
using InjectoPatronum;

namespace BigBrother.Commands.Hello
{
	[CommandHandler]
	internal sealed class HelloCommandHandler : SlashCommandHandler
	{
		public override string Name => "hello";
		public override string Description => "Because being polite is always important";

		public HelloCommandHandler(IDependencyInjector injector) : base(injector) { }

		public override Task Execute(ICommandRequest command)
		{
			return command.Respond($"Hello {MentionUtils.MentionUser(command.Sender.Id)}");
		}
	}
}
