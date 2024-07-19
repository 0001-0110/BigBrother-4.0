using Discord;
using Discord.WebSocket;

namespace BigBrother.Services.ReplyService
{
    internal interface IReplyService
    {
        Task<string?> GenerateReply(IEnumerable<IMessage> messages);
    }
}
