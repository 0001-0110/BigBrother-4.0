using RogerRoger.Models.Settings;

namespace RogerRoger.DataAccess.Repositories
{
    public class GuildRoleRepository : Repository
    {
        public GuildRoleRepository(RogerRogerContext context) : base(context) { }

        public int Add(GuildRole guildRole)
        {
            int id = _context.GuildRoles.Add(guildRole).Entity.Id;
            _context.SaveChanges();
            return id;
        }

        public void Remove(ulong roleId)
        {
            Remove(_context.GuildRoles.FirstOrDefault(role => role.RoleId == roleId));
        }

        public void Remove(GuildRole guildRole)
        {
            _context.GuildRoles.Remove(guildRole);
            _context.SaveChanges();
        }
    }
}
