using BigBrother.Logger;
using BigBrother.MessageHandling;
using BigBrother.Services.ReplyService;
using Discord.WebSocket;
using static BigBrother.Services.ReplyService.OllamaService;
using static BigBrother.Services.ReplyService.OllamaService.OllamaRequest;
using static BigBrother.Services.ReplyService.OllamaService.OllamaRequest.Message;

namespace BigBrother.MessageHandlers
{
    internal class ScamDetectionHandler : MessageHandler
    {
        private readonly OllamaService _ollamaService;
        private readonly string _prompt = "You are a discord moderation bot in charge of detecting scams. A user will send you a message. You have to answer yes if the message is clearly a scam, and no otherwise. When in doubt, say no.";

        public ScamDetectionHandler(ILogger logger, OllamaService ollamaService) : base(logger)
        {
            _ollamaService = ollamaService;
        }

        private OllamaRequest GenerateRequest(string message)
        {
            return new OllamaRequest([new Message(Role.System, _prompt), new Message(Role.User, message)]);
        }

        public override async Task Handle(SocketMessage message)
        {
            return;
            string? reply = await _ollamaService.Generate(GenerateRequest(message.Content));
            if (reply != null && reply.ToLower().Contains("yes"))
            {
                await message.DeleteAsync();
                await message.Channel.SendMessageAsync("This message has been detected as a potential scam.");
                //(message.Author as IGuildUser)?.GuildPermissions.Modify(sendMessages: false);
            }
        }
    }
}
