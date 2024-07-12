using BigBrother.Extensions;
using BigBrother.Messages;
using Discord.WebSocket;

namespace BigBrother.MessageHandling
{
    internal class SelfMentionHandler : IMessageHandler
    {
        private DiscordSocketClient Client => BigBrother.Client;

        public async Task Handle(SocketMessage message)
        {
            if (message.Mentions(Client.CurrentUser))
                await message.Channel.SendMessageAsync("Nope");
        }
    }
}
