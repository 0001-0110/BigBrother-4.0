using RogerRoger.Models.ReminderCommand;

namespace RogerRoger.DataAccess.Repositories
{
    public class ReminderRepository : Repository
    {
        public ReminderRepository(RogerRogerContext context) : base(context) { }

        public int Add(Reminder model)
        {
            int id = _context.Reminders.Add(model).Entity.Id;
            _context.SaveChanges();
            return id;
        }
    }
}
