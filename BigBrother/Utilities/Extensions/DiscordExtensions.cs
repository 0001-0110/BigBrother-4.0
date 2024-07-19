using System.Text.RegularExpressions;
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

        private static async Task<IEnumerable<IMessage>> GetReplyChain(ISocketMessageChannel channel, IMessage message)
        {
            if (!message.Reference?.MessageId.IsSpecified ?? true)
                return [message];

            IMessage previousMessage = await channel.GetMessageAsync(message.Reference!.MessageId.Value);
            return (await GetReplyChain(channel, previousMessage)).Append(message);
        }

        public static Task<IEnumerable<IMessage>> GetReplyChain(this SocketMessage message)
        {
            return GetReplyChain(message.Channel, message);
        }

        public static string GetPreProcessedContent(this IMessage message)
        {
            return new Regex("<@[0-9]+>").Replace(message.Content,
                match => (message.Channel.GetUserAsync(ulong.Parse(match.Value[2..^1])).AwaitSync() as IGuildUser)!.DisplayName);
        }
    }
}
