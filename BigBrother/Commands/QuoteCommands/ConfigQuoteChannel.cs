using BigBrother.CommandHandling;
using BigBrother.CommandHandling.Attributes;
using BigBrother.CommandHandling.CommandRequest;
using BigBrother.Logger;
using Discord;
using InjectoPatronum;
using RogerRoger.DataAccess.Repositories;
using RogerRoger.Models.Settings;

namespace BigBrother.Commands.QuoteCommands
{
    [SubCommandHandler<ConfigQuote>()]
    internal class ConfigQuoteChannel : SlashSubCommandHandler
    {
        private readonly GuildSettingsRepository _guildSettingsRepository;

        public override string Name => "channel";
        public override string Description => "Set the channel used to fetch the quotes";

        private readonly SlashCommandOption<IChannel> _channelOption = new SlashCommandOption<IChannel>("channel", "The channel used to fetch quotes", true);

        public ConfigQuoteChannel(IDependencyInjector injector, ILogger logger) : base(injector, logger)
        {
            _guildSettingsRepository = injector.Instantiate<GuildSettingsRepository>();
        }

        protected override async Task Execute(ICommandRequest command, params object[] args)
        {
            if (_channelOption.GetValue(command) is not ITextChannel newQuoteChannel)
            {
                await command.Respond("Invalid channel type");
                return;
            }

            GuildSettings? guildSettings = args[0] as GuildSettings;
            guildSettings.QuoteChannelId = newQuoteChannel.Id;
            _guildSettingsRepository.SaveChanges();

            await command.Respond($"Quote channel updated to {newQuoteChannel.Name}");
        }
    }
}
