namespace BigBrother.Configuration
{
    internal interface IConfigurationService
    {
        IGlobalConfig Load();

        void Save(IGlobalConfig config);
    }
}
