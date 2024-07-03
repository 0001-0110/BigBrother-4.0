using BigBrother.CommandHandling;
using BigBrother.CommandHandling.Attributes;
using BigBrother.CommandHandling.CommandRequest;
using BigBrother.Configuration;
using BigBrother.Extensions;
using BigBrother.Logger;
using Discord;
using Discord.WebSocket;
using InjectoPatronum;
using System.Text.RegularExpressions;

namespace BigBrother.Commands.Quote
{
    [SubCommandHandler(typeof(Quote))]
    internal class QuoteRandom : SlashSubCommandHandler
    {
        private readonly IConfigurationService _configurationService;

        private readonly Dictionary<ulong, ICollection<string>> _quotes;

        public override string Name => "random";
        public override string Description => "Get a random quote";

        public QuoteRandom(IDependencyInjector injector, IConfigurationService configurationService, ILogger logger) : base(injector, logger)
        {
            _configurationService = configurationService;

            _quotes = new Dictionary<ulong, ICollection<string>>();
        }

        private async Task LoadQuotes(SocketGuild guild)
        {
            Regex quoteRegex = new Regex("^(?:> )|(?:[\"“«].*[\"“»])");

            IGuildConfig? config = _configurationService.Load().GuildConfigs.FirstOrDefault(guildConfig => guildConfig.Id == guild.Id);
            if (config == null || config.QuoteChannel == 0)
                return;

            if (guild.GetChannel(config.QuoteChannel) is not IMessageChannel quoteChannel)
                return;

            _quotes[guild.Id] = new List<string>();
            IEnumerable<IMessage> messages = await quoteChannel.GetMessagesAsync(2000).FlattenAsync();
            foreach (IMessage message in messages.Where(message => quoteRegex.IsMatch(message.Content)))
                    _quotes[guild.Id]!.Add(message.Content);
        }

        private async Task<string?> GetRandomQuote(SocketGuild guild)
        {
            if (!_quotes.ContainsKey(guild.Id))
                await LoadQuotes(guild);

            return _quotes[guild.Id].GetRandom();
        }

        protected override async Task Execute(ICommandRequest command)
        {
            await command.Respond(await GetRandomQuote(command.Guild!) ?? "Could not find any quote\nCheck that the quote channel is set and contains at least one quote");
        }
    }
}
