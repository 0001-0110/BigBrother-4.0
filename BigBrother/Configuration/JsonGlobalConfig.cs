
using Newtonsoft.Json;

namespace BigBrother.Configuration
{
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
		}

		[JsonProperty("token")]
		[JsonRequired]
		public string Token { get; set; }

		// Needs to be public for the json deserialization
		[JsonProperty("guilds")]
		[JsonRequired]
		public IEnumerable<JsonGuildConfig> _guildConfigs;

        [JsonIgnore]
		public IEnumerable<IGuildConfig> GuildConfigs => _guildConfigs;
	}
}
