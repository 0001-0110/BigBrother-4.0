using Discord;
using Discord.WebSocket;

namespace BigBrother.Utilities.Extensions
{
    internal static class DiscordExtensions
    {
        public static Task Reply(this SocketMessage message, string text)
        {
            return message.Channel.SendMessageAsync(text, messageReference: new MessageReference(message.Id));
        }
    }
}
