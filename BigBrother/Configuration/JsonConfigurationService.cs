using BigBrother.Logger;
using Newtonsoft.Json;

namespace BigBrother.Configuration
{
	internal class JsonConfigurationService : IConfigurationService
	{
        private readonly ILogger _logger;

        private readonly string _path;

        public JsonConfigurationService(ILogger logger, string path)
        {
            _logger = logger;

            _path = path;
        }

        public IGlobalConfig Load()
        {
            return JsonConvert.DeserializeObject<JsonGlobalConfig>(File.ReadAllText(_path));
        }

        public void Save(IGlobalConfig config)
        {
            using (StreamWriter writer = new StreamWriter(File.Open(_path, FileMode.Create)))
            {
                writer.Write(JsonConvert.SerializeObject(config, Formatting.Indented));
            }

            _logger.LogVerbose("Saved configuration");
        }
    }
}
