using BigBrother.Logger;
using Discord;
using InjectoPatronum;
using UtilityMinistry.Extensions;

namespace BigBrother.CommandHandling
{
    internal abstract class SlashSubCommandHandler : SubCommandHandler<SlashSubCommandHandler>
    {
        public SlashSubCommandHandler(IDependencyInjector injector, ILogger logger) : base(injector, logger) { }

        public virtual SlashCommandOptionBuilder CreateCommand()
        {
            return new SlashCommandOptionBuilder()
                .WithName(Name)
                .WithDescription(Description)
                .WithType(_subCommandHandlers.Count == 0 ? ApplicationCommandOptionType.SubCommand : ApplicationCommandOptionType.SubCommandGroup)
                .AddOptions(GetOptions());
        }

        private SlashCommandOptionBuilder[] GetOptions()
        {
            if (_subCommandHandlers.Count == 0)
            {
                // If this command is final, use reflexion to search for all options that need to be added
                return GetType().GetFields(typeof(SlashCommandOption)).Select(property => ((SlashCommandOption)property.GetValue(this)!).CreateOption()).ToArray();
            }

            return _subCommandHandlers.Values.Select(handler => handler.CreateCommand()).ToArray();
        }
    }
}
