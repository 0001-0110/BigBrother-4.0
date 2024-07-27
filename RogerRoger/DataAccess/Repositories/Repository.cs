namespace RogerRoger.DataAccess.Repositories
{
    public class Repository
    {
        protected readonly RogerRogerContext _context;

        public Repository(RogerRogerContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
