using Newtonsoft.Json;

namespace BigBrother.Configuration
{
	internal class ConfigurationReader
	{
		public static IGlobalConfig GetConfig(string path)
		{
			return JsonConvert.DeserializeObject<JsonGlobalConfig>(File.ReadAllText(path));
		}
	}
}
