using Discord;
using Discord.WebSocket;

namespace BigBrother.Extensions
{
    internal static class DiscordExtensions
    {
        public static SocketGuild? GetGuild(this IChannel channel)
        {
            return (channel as SocketGuildChannel)?.Guild;
        }

        public static bool Mentions(this SocketMessage message, SocketUser user)
        {
            SocketGuild? guild = message.Channel.GetGuild();
            return message.MentionedUsers.Any(mention => mention.Id == user.Id)
                || (guild != null && message.MentionedRoles.Any(role => guild.CurrentUser.Roles.Contains(role)));
        }
    }
}
