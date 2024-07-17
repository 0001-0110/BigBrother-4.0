namespace RogerRoger.Models.Settings
{
    public class GuildSettings : IModel<ulong>
    {
        public ulong Id { get; set; }    

        public ulong QuoteChannelId { get; set; }
    }
}
