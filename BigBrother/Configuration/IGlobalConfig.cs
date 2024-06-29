namespace BigBrother.Configuration
{
	internal interface IGlobalConfig
	{
		string Token { get; }

		IEnumerable<IGuildConfig> GuildConfigs { get; }
	}
}
