using Newtonsoft.Json;

namespace BigBrother.Configuration
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    internal class JsonGlobalConfig : IGlobalConfig
    {
        public class JsonGuildConfig : IGuildConfig
        {
            [JsonProperty("id")]
            [JsonRequired]
            public ulong Id { get; set; }

            [JsonProperty("commands")]
            [JsonRequired]
            public IEnumerable<string> ActiveCommands { get; set; }

            [JsonProperty("quoteChannel")]
            public ulong QuoteChannel { get; set; }

            public JsonGuildConfig() { }

            public JsonGuildConfig(ulong id)
            {
                Id = id;
            }
        }

        [JsonProperty("token")]
        [JsonRequired]
        public string Token { get; set; }

        // Needs to be public for the json deserialization
        [JsonProperty("guilds")]
        [JsonRequired]
        public ICollection<JsonGuildConfig> _guildConfigs;

        [JsonIgnore]
        public IEnumerable<IGuildConfig> GuildConfigs => _guildConfigs;

        public IGuildConfig AddGuildConfig(ulong id)
        {
            JsonGuildConfig guildConfig = new JsonGuildConfig(id);
            _guildConfigs.Add(guildConfig);
            return guildConfig;
        }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
