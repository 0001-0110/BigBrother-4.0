using BigBrother.CommandHandling;
using BigBrother.Logger;
using Discord;
using InjectoPatronum;

namespace BigBrother.Commands.Hello
{
	[CommandHandler]
	internal sealed class Hello : SlashCommandHandler
	{
		public override string Name => "hello";
		public override string Description => "Because being polite is always important";

		public Hello(IDependencyInjector injector, ILogger logger) : base(injector, logger) { }

		protected override Task Execute(ICommandRequest command)
		{
			return command.Respond($"Hello {MentionUtils.MentionUser(command.Sender.Id)}");
		}
	}
}
