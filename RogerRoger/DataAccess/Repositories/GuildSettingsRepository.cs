using RogerRoger.Models.Extensions;
using RogerRoger.Models.Settings;

namespace RogerRoger.DataAccess.Repositories
{
    public class GuildSettingsRepository : Repository
    {
        public GuildSettingsRepository(RogerRogerContext context) : base(context) { }

        public GuildSettings? GetById(ulong id)
        {
            GuildSettingsExtensions.Repository = this;
            return _context.GuildSettings.Find(id);
        }

        public ulong Add(GuildSettings guildSettings)
        {
            ulong id = _context.GuildSettings.Add(guildSettings).Entity.Id;
            _context.SaveChanges();
            return id;
        }
    }
}
