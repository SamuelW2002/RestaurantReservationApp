using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public class ControlDatabaseRepository : IControlDatabaseRepository
    {
        private readonly DatabaseContext _databaseContext;

        public ControlDatabaseRepository(DatabaseContext context)
        {
            _databaseContext = context;
        }

        public void ResetDatabase()
        {
            _databaseContext.ResetDatabase();
        }
    }
}
