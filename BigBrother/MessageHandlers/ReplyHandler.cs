using BigBrother.Logger;
using BigBrother.Services.ReplyService;
using BigBrother.Utilities.Extensions;
using Discord;
using Discord.WebSocket;
using static BigBrother.Services.ReplyService.OllamaService;
using static BigBrother.Services.ReplyService.OllamaService.OllamaRequest;
using static BigBrother.Services.ReplyService.OllamaService.OllamaRequest.Message;

namespace BigBrother.MessageHandling
{
    internal class ReplyHandler : MessageHandler
    {
        private readonly string _errorMessage = "Oh no! The squirrels have taken over the server room again! 🐿️🚨\n**Error 503**: Server Room Occupied by Squirrels\n```Description: We apologize for the interruption, but it seems our servers are currently experiencing a rodent-induced outage. Our team is frantically chasing them out with acorns and motivational speeches. Please bear with us as we restore order and get back to serving you shortly! If problem persists, please contact our tech support and mention you've encountered the \"Squirrelpocalypse Error.\"```";
        private readonly string _prompt = "You are a discord bot named Big Brother. Your task is to be helpful when someone asks you a question, and to be funny otherwise, using a dry sens of humor. Keep the messages short, and always start by 'User Big Brother:'";

        private readonly OllamaService _ollamaService;

        private static DiscordSocketClient Client => BigBrother.Client;

        public ReplyHandler(ILogger logger, OllamaService ollamaService) : base(logger)
        {
            _ollamaService = ollamaService;
        }

        public override async Task Handle(SocketMessage message)
        {
            if (!message.Mentions(Client.CurrentUser))
                return;

            using (IDisposable typing = message.Channel.EnterTypingState())
            {
                OllamaRequest request = new OllamaRequest((await message.GetReplyChain())
                    .Select(message => new Message(
                        BigBrother.IsCurrentUser(message.Author) ? Role.Assistant : Role.User,
                        // The content of the message (prefixed by the username to allow multi user conversations)
                        $"User {(message.Author as IGuildUser)!.DisplayName}: {message.GetPreProcessedContent()}"))
                    // Add the prompt to give the bot its personnality
                    .Prepend(new Message(Role.System, _prompt)));

                string? reply = await _ollamaService.Generate(request);
                await message.Reply(reply ?? _errorMessage);
            }
        }
    }
}
