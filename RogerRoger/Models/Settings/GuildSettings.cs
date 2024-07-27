using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RogerRoger.Models.Settings
{
    [Table("GuildSettings")]
    public class GuildSettings : IModel<ulong>
    {
        [Key]
        public ulong Id { get; set; }

        public ulong QuoteChannelId { get; set; }

        [InverseProperty("Guild")]
        public List<GuildRole> Roles { get; set; } = new List<GuildRole>();

        internal GuildSettings() { }

        public GuildSettings(ulong id)
        {
            Id = id;
        }
    }
}
