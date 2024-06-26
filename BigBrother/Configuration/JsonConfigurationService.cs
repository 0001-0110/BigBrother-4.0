using System.Text.Json;

namespace BigBrother.Configuration
{
	internal class JsonConfigurationService : IConfigurationService
	{
		public static JsonConfigurationService Load(string path)
		{
			using FileStream stream = File.OpenRead(path);
			return new JsonConfigurationService(JsonDocument.Parse(stream).RootElement);
		}

		private readonly JsonElement _config;

		private JsonConfigurationService(JsonElement config)
		{
			_config = config;
		}

		private T GetValue<T>(JsonElement element)
		{
			if (typeof(T) == typeof(short))
				return (T)(object)element.GetInt16();

			if (typeof(T) == typeof(int))
				return (T)(object)element.GetInt32();

			if (typeof(T) == typeof(long))
				return (T)(object)element.GetInt64();

			if (typeof(T) == typeof(decimal))
				return (T)(object)element.GetDecimal();

			if (typeof(T) == typeof(double))
				return (T)(object)element.GetDouble();

			if (typeof(T) == typeof(bool))
				return (T)(object)element.GetBoolean();

			if (typeof(T) == typeof(string))
				return (T)(object)element.GetString()!;

			throw new NotSupportedException("This generic method can only accept primitive types");
		}

		public T Get<T>(string key)
		{
			if (!_config.TryGetProperty(key, out JsonElement element))
				throw new KeyNotFoundException(key);
			return GetValue<T>(element);
		}
	}
}
