using BigBrother.Logger;
using BigBrother.Messages;
using BigBrother.Services.ReplyService;
using BigBrother.Utilities.Extensions;
using Discord.WebSocket;

namespace BigBrother.MessageHandling
{
    internal class ReplyHandler : IMessageHandler
    {
        private readonly string _errorMessage = "Oh no! The squirrels have taken over the server room again! 🐿️🚨\n**Error 503**: Server Room Occupied by Squirrels\n```Description: We apologize for the interruption, but it seems our servers are currently experiencing a rodent-induced outage. Our team is frantically chasing them out with acorns and motivational speeches. Please bear with us as we restore order and get back to serving you shortly! If problem persists, please contact our tech support and mention you've encountered the \"Squirrelpocalypse Error.\"```";

        private readonly ILogger _logger;
        private readonly IReplyService _replyService;

        private static DiscordSocketClient Client => BigBrother.Client;

        public ReplyHandler(ILogger logger, IReplyService replyService)
        {
            _replyService = replyService;
            _logger = logger;
        }

        public async Task Handle(SocketMessage message)
        {
            if (!message.Mentions(Client.CurrentUser))
                return;

            using (IDisposable typing = message.Channel.EnterTypingState())
            {
                string? reply = await _replyService.GenerateReply(await message.GetReplyChain());
                await message.Reply(reply ?? _errorMessage);
            }
        }
    }
}
