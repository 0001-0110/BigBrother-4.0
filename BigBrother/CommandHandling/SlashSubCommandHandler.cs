using Discord;
using InjectoPatronum;

namespace BigBrother.CommandHandling
{
	internal abstract class SlashSubCommandHandler : SubCommandHandler<SlashSubCommandHandler>
	{
		public SlashSubCommandHandler(IDependencyInjector injector) : base(injector) { }

		public SlashCommandOptionBuilder CreateCommand()
		{
			return new SlashCommandOptionBuilder()
				.WithName(Name)
				.WithDescription(Description)
				.WithType(ApplicationCommandOptionType.SubCommand);
		}
	}
}
