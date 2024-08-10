using System.Text;
using BigBrother.Logger;
using BigBrother.Utilities.Extensions;
using Newtonsoft.Json;

namespace BigBrother.Services.ReplyService
{
    internal class OllamaService
    {
        private static readonly string _url = "http://ollama:11434/api/chat";

        public class OllamaRequest
        {
            public class Message
            {
                public enum Role
                {
                    System,
                    Assistant,
                    User,
                }

                [JsonProperty("role")]
                private string _role;

                [JsonProperty("content")]
                private string _content;

                public Message(Role role, string content)
                {
                    _role = role.ToString().ToLower();
                    _content = content;
                }
            }

            [JsonProperty("model")]
            private string _model = "llama3";

            [JsonProperty("stream")]
            private bool _stream = false;

            [JsonProperty("messages")]
            private IEnumerable<Message> _messages;

            public OllamaRequest(IEnumerable<Message> messages)
            {
                _messages = messages;
            }

            public string ToJson()
            {
                return JsonConvert.SerializeObject(this);
            }
        }

        private class LlamaResponse
        {
            public class ApiMessage
            {
                [JsonProperty("content")]
                public string Content;
            }

            [JsonProperty("message")]
            public ApiMessage Message;
        }

        private readonly ILogger _logger;

        public OllamaService(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<string?> Generate(OllamaRequest request)
        {
            string body = request.ToJson();
            StringContent content = new StringContent(body, Encoding.UTF8, "application/json");

            await _logger.LogDebug("Sending LLM request");
            using (HttpClient httpClient = new HttpClient())
            {
                // Send the request
                HttpResponseMessage response = await httpClient.PostAsync(_url, content);

                if (!response.IsSuccessStatusCode)
                {
                    await _logger.LogError("LLM request failed");
                    return null;
                }

                await _logger.LogDebug("LLM request succeded");
                return JsonConvert.DeserializeObject<LlamaResponse>(await response.Content.ReadAsStringAsync())?.Message.Content.Remove("User Big Brother:");
            }
        }
    }
}
