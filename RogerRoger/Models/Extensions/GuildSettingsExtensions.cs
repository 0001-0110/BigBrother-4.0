using RogerRoger.DataAccess.Repositories;
using RogerRoger.Models.Settings;

namespace RogerRoger.Models.Extensions
{
    public static class GuildSettingsExtensions
    {
        internal static GuildSettingsRepository Repository { get; set; }

        public static GuildSettings OrCreate(this GuildSettings? guildSettings, Func<GuildSettings> builder)
        {
            if (guildSettings != null)
                return guildSettings;

            guildSettings = builder.Invoke();
            Repository.Add(guildSettings);
            return guildSettings;
        }
    }
}
