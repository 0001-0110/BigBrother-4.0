using System.Text;
using BigBrother.Logger;
using Discord;
using Newtonsoft.Json;

namespace BigBrother.Services.ReplyService
{
    internal class LlamaService : IReplyService
    {
        private readonly string _url = "http://ollama:11434/api/chat";

        private class LlamaRequest
        {
            public class Message
            {
                [JsonProperty("role")]
                private string _role;

                [JsonProperty("content")]
                private string _content;

                public Message(string role, string content)
                {
                    _role = role;
                    _content = content;
                }
            }

            [JsonProperty("model")]
            private string _model = "llama3";

            [JsonProperty("stream")]
            private bool _stream = false;

            [JsonProperty("messages")]
            private IEnumerable<Message> _messages;

            public LlamaRequest(IEnumerable<Message> messages)
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
        private readonly ulong _currentUserId;

        public LlamaService(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<string?> GenerateReply(IEnumerable<IMessage> messages)
        {
            string body = new LlamaRequest(messages.Select(message => new LlamaRequest.Message(BigBrother.IsCurrentUser(message.Author) ? "assistant" : "user", message.Content))).ToJson();
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
                return JsonConvert.DeserializeObject<LlamaResponse>(await response.Content.ReadAsStringAsync())?.Message.Content;
            }
        }
    }
}
