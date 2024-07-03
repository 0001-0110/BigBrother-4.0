using BigBrother.CommandHandling;
using BigBrother.CommandHandling.Attributes;
using BigBrother.CommandHandling.CommandRequest;
using BigBrother.Commands.QuoteCommands;
using BigBrother.Configuration;
using BigBrother.Logger;
using Discord;
using InjectoPatronum;

namespace BigBrother.Commands.Quote
{
    [SubCommandHandler(typeof(ConfigQuote))]
    internal class ConfigQuoteChannel : SlashSubCommandHandler
    {
        private readonly IConfigurationService _configurationService;

        public override string Name => "channel";
        public override string Description => "Set the channel used to fetch the quotes";

        private readonly SlashCommandOption<IChannel> _channelOption = new SlashCommandOption<IChannel>("channel", "The channel used to fetch quotes", true);

        public ConfigQuoteChannel(IDependencyInjector injector, IConfigurationService configurationService, ILogger logger) : base(injector, logger)
        {
            _configurationService = configurationService;
        }

        protected override async Task Execute(ICommandRequest command)
        {
            if (_channelOption.GetValue(command) is not ITextChannel newQuoteChannel)
            {
                await command.Respond("Invalid channel type");
                return;
            }

            IGlobalConfig config = _configurationService.Load();
            IGuildConfig? guildConfig = config.GuildConfigs.SingleOrDefault(guild => guild.Id == newQuoteChannel.GuildId)
                ?? config.AddGuildConfig(newQuoteChannel.GuildId);
            guildConfig.QuoteChannel = newQuoteChannel.Id;
            _configurationService.Save(config);

            await command.Respond($"Quote channel updated to {newQuoteChannel.Name}");
        }
    }
}
