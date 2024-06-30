using BigBrother.Logger;
using Discord;
using InjectoPatronum;

namespace BigBrother.CommandHandling
{
	internal abstract class SlashCommandHandler : CommandHandler<SlashSubCommandHandler>
	{
		protected SlashCommandHandler(IDependencyInjector injector, ILogger logger) : base(injector, logger) { }

		public virtual SlashCommandBuilder CreateCommand()
		{
			return new SlashCommandBuilder()
				.WithName(Name)
				.WithDescription(Description)
				.AddOptions(_subCommandHandlers.Values.Select(handler => handler.CreateCommand()).ToArray());
		}
	}
}
