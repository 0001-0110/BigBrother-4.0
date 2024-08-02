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

        // TODO
        //[MaxLength()]
        public string? TrekPath { get; set; }
    }
}
