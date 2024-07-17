using BigBrother.Extensions;
using BigBrother.Logger;
using BigBrother.Messages;
using BigBrother.Utilities.Extensions;
using Discord.WebSocket;
using Newtonsoft.Json;
using System.Text;

namespace BigBrother.MessageHandling
{
    internal class SelfMentionHandler : IMessageHandler
    {
        private readonly string _errorMessage = "Oh no! The squirrels have taken over the server room again! 🐿️🚨\n**Error 503**: Server Room Occupied by Squirrels\n```Description: We apologize for the interruption, but it seems our servers are currently experiencing a rodent-induced outage. Our team is frantically chasing them out with acorns and motivational speeches. Please bear with us as we restore order and get back to serving you shortly! If problem persists, please contact our tech support and mention you've encountered the \"Squirrelpocalypse Error.\"```";

        private class ApiResponse
        {
            [JsonProperty("response")]
            public string Content;
        }

        private readonly ILogger _logger;

        private static DiscordSocketClient Client => BigBrother.Client;

        public SelfMentionHandler(ILogger logger)
        {
            _logger = logger;
        }

        private async Task Reply(SocketMessage message)
        {
            await _logger.LogDebug("Received message");
            string url = "http://ollama:11434/api/generate";
            string body = JsonConvert.SerializeObject(new
            {
                model = "llama3",
                stream = false,
                prompt = message.Content
            });
            StringContent content = new StringContent(body, Encoding.UTF8, "application/json");

            using (HttpClient httpClient = new HttpClient())
            using (IDisposable typing = message.Channel.EnterTypingState())
            {
                // Send the request
                HttpResponseMessage response = await httpClient.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    // TODO Find a better log message
                    await _logger.LogError("LLM request failed");
                    await message.Channel.SendMessageAsync(_errorMessage, messageReference: message.Reference);
                    return;
                }

                await _logger.LogDebug("LLM request succeded");

                string? truc = JsonConvert.DeserializeObject<ApiResponse>(await response.Content.ReadAsStringAsync())?.Content;
                if (response == null)
                    return;

                await message.Reply(truc);
            }
        }

        public Task Handle(SocketMessage message)
        {
            if (!message.Mentions(Client.CurrentUser))
                return Task.CompletedTask;

            return Reply(message);
        }
    }
}
