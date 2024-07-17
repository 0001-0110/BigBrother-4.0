using BigBrother.Logger;
using Discord;
using InjectoPatronum;
using System.Reflection;
using UtilityMinistry.Extensions;

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
