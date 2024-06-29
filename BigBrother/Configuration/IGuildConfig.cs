namespace BigBrother.Configuration
{
	internal interface IGuildConfig
	{
		ulong Id { get; }

		IEnumerable<string> ActiveCommands { get; }
	}
}
