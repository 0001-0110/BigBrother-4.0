using Microsoft.EntityFrameworkCore;
using RogerRoger.Configuration;
using RogerRoger.Models.ReminderCommand;
using RogerRoger.Models.Settings;

namespace RogerRoger.DataAccess
{
    public class RogerRogerContext : DbContext
    {
        private readonly string _connectionString;
        //private readonly ILogger _logger;

        internal DbSet<GuildSettings> GuildSettings { get; private set; }
        internal DbSet<Reminder> Reminders { get; private set; }

        public RogerRogerContext(IDbConfig config)
        {
            _connectionString = config.ConnectionString;
            // Weird to have this in the constructor, but I don't know where else to put it
            Database.EnsureCreated();
            //_logger = logger;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
            //optionsBuilder.LogTo(message => _logger.LogVerbose(message));
        }
    }
}
