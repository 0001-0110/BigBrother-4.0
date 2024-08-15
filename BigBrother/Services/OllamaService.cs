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
                private readonly string _role;

                [JsonProperty("content")]
                private readonly string _content;

                [JsonProperty("images", NullValueHandling = NullValueHandling.Ignore)]
                public readonly string[]? Images;

                public Message(Role role, string content, string[]? images = null)
                {
                    _role = role.ToString().ToLower();
                    _content = content;
                    Images = images;
                }
            }

            [JsonProperty("model")]
            private readonly string _model;

            [JsonProperty("stream")]
            private readonly bool _stream = false;

            [JsonProperty("messages")]
            private readonly IEnumerable<Message> _messages;

            public OllamaRequest(IEnumerable<Message> messages)
            {
                _model = messages.Any(message => message.Images != null) ? "llava" : "llama3";
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

                //[JsonProperty("")]
                // TODO Get the image in the answer
            }

            [JsonProperty("message")]
            public ApiMessage Message;
        }

        private readonly ILogger _logger;

        public OllamaService(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<string?> GenerateText(OllamaRequest request)
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
