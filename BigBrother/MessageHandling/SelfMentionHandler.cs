using BigBrother.Extensions;
using BigBrother.Messages;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using BigBrother.Logger;

namespace BigBrother.MessageHandling
{
    internal class SelfMentionHandler : IMessageHandler
    {
        private readonly ILogger _logger;

        private static DiscordSocketClient Client => BigBrother.Client;

        public SelfMentionHandler(ILogger logger)
        {
            _logger = logger;
        }

        public async Task Handle(SocketMessage message)
        {
            if (!message.Mentions(Client.CurrentUser))
                return;

            string url = "http://localhost:1337/v1";
            // Serialize the payload to JSON
            string body = JsonConvert.SerializeObject(new
            {
                model = "gpt-3.5-turbo-16k",
                stream = false,
                messages = new[]
                {
                    new { role = "assistant", content = "What can you do?" }
                }
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

                // Extract the 'choices' array
                if (JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync())?["choices"] is not JArray choices)
                    return;

                foreach (JToken choice in choices)
                {
                    string? messageContent = choice["message"]?["content"]?.ToString();
                    if (!string.IsNullOrEmpty(messageContent))
                        return;

                    await message.Channel.SendMessageAsync(messageContent);
                }
            }
        }
    }
}
