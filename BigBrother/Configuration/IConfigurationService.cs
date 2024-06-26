namespace BigBrother.Configuration
{
	internal interface IConfigurationService
	{
		public T Get<T>(string key);
	}
}
