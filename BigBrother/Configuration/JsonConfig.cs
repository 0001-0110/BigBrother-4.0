using Newtonsoft.Json;
using RogerRoger.Configuration;

namespace BigBrother.Configuration
{
    public class JsonConfig : IDbConfig, IDiscordConfig
    {
        public static JsonConfig Load(string path)
        {
            return JsonConvert.DeserializeObject<JsonConfig>(File.ReadAllText(path)) ?? throw new Exception("Configuration could not be loaded");
        }

        [JsonProperty("connectionString")]
        [JsonRequired]
        public string ConnectionString { get; private set; }

        [JsonProperty("token")]
        [JsonRequired]
        public string Token { get; private set; }
    }
}
