using System.ComponentModel.DataAnnotations;

namespace RogerRoger.Models.Trek
{
    public class TrekPlayer : IModel<int>
    {
        [Key]
        public int Id { get; internal set; }

        public ulong UserId { get; set; }

        public ulong GuildId { get; set; }

        public string Location { get; set; }
    }
}
