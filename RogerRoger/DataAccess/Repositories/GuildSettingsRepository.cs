using RogerRoger.Models.Settings;

namespace RogerRoger.DataAccess.Repositories
{
    public class GuildSettingsRepository : Repository
    {
        public GuildSettingsRepository(RogerRogerContext context) : base(context) { }

        public GuildSettings GetOrCreate(ulong id)
        {
            return GetById(id) ?? Create(id);
        }

        public GuildSettings? GetById(ulong id)
        {
            return _context.GuildSettings.Find(id);
        }

        public GuildSettings Create(ulong id)
        {
            return _context.GuildSettings.Add(new GuildSettings(id)).Entity;
        }

        public ulong Add(GuildSettings guildSettings)
        {
            ulong id = _context.GuildSettings.Add(guildSettings).Entity.Id;
            _context.SaveChanges();
            return id;
        }
    }
}
