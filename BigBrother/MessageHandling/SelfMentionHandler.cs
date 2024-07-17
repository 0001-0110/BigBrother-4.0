using BigBrother.Extensions;
using BigBrother.Logger;
using BigBrother.Messages;
using Discord.WebSocket;
using Newtonsoft.Json;
using System.Text;

namespace BigBrother.MessageHandling
{
    internal class SelfMentionHandler : IMessageHandler
    {
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
            {
                // Send the request
                HttpResponseMessage response = await httpClient.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    // TODO Find a better log message
                    await _logger.LogError("g4f failed");
                    return;
                }

                await _logger.LogError("g4f success");

                var truc = JsonConvert.DeserializeObject<ApiResponse>(await response.Content.ReadAsStringAsync())?.Content;
                if (response == null)
                    return;

                await message.Channel.SendMessageAsync(truc);
            }
        }

        public Task Handle(SocketMessage message)
        {
            if (!message.Mentions(Client.CurrentUser))
                return Task.CompletedTask;

            Task.Run(async () => await Reply(message));
            return Task.CompletedTask;
        }
    }
}
