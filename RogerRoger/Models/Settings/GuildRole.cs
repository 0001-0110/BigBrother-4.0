using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RogerRoger.Models.Settings
{
    [Table("GuildRoles")]
    public class GuildRole : IModel<int>
    {
        [Key]
        public int Id { get; internal set; }

        [Required]
        public GuildSettings Guild { get; set; }

        [Required]
        public ulong RoleId { get; set; }

        internal GuildRole() { }

        public GuildRole(GuildSettings guild, ulong roleId)
        {
            Guild = guild;
            RoleId = roleId;
        }
    }
}
