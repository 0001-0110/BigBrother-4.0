using BigBrother.CommandHandling;
using BigBrother.CommandHandling.Attributes;
using BigBrother.CommandHandling.CommandRequest;
using BigBrother.Logger;
using Discord;
using Discord.WebSocket;
using InjectoPatronum;
using RogerRoger.DataAccess.Repositories;
using RogerRoger.Models.Settings;
using System.Text.RegularExpressions;
using UtilityMinistry.Extensions;

namespace BigBrother.Commands.QuoteCommands
{
    [SubCommandHandler<Quote>()]
    internal class QuoteRandom : SlashSubCommandHandler
    {
        private readonly GuildSettingsRepository _guildSettingsRepository;

        public override string Name => "random";
        public override string Description => "Get a random quote";

        public QuoteRandom(IDependencyInjector injector, ILogger logger) : base(injector, logger)
        {
            _guildSettingsRepository = injector.Instantiate<GuildSettingsRepository>();
        }

        private async Task<IEnumerable<string>?> LoadQuotes(SocketGuild guild)
        {
            Regex quoteRegex = new Regex("^(?:> )|(?:[\"“«].*[\"“»])");

            GuildSettings? settings = _guildSettingsRepository.GetById(guild.Id);
            if (settings == null || settings.QuoteChannelId == 0
                || guild.GetChannel(settings.QuoteChannelId) is not IMessageChannel quoteChannel)
                return null;

            List<string> quotes = [];
            IEnumerable<IMessage> messages = await quoteChannel.GetMessagesAsync(2000).FlattenAsync();
            foreach (IMessage message in messages.Where(message => quoteRegex.IsMatch(message.Content)))
                quotes.Add(message.Content);
            return quotes;
        }

        private async Task<string?> GetRandomQuote(SocketGuild guild)
        {
            return (await LoadQuotes(guild))?.GetRandom();
        }

        protected override async Task Execute(ICommandRequest command)
        {
            await command.Respond(await GetRandomQuote(command.Guild!) ?? "Could not find any quote\nCheck that the quote channel is set and contains at least one quote");
        }
    }
}
