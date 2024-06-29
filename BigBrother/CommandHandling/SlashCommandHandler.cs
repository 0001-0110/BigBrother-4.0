using Discord;
using InjectoPatronum;

namespace BigBrother.CommandHandling
{
	internal abstract class SlashCommandHandler : CommandHandler<SlashSubCommandHandler>
	{
		protected SlashCommandHandler(IDependencyInjector injector) : base(injector) { }

		public virtual SlashCommandBuilder CreateCommand()
		{
			return new SlashCommandBuilder()
				.WithName(Name)
				.WithDescription(Description)
				.AddOptions(_subCommandHandlers.Select(handler => handler.CreateCommand()).ToArray());
		}
	}
}
