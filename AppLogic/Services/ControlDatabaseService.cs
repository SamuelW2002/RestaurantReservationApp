using AppLogic.Interfaces;
using Infrastructure.Interfaces;


namespace AppLogic.Services
{
    public class ControlDatabaseService : IControlDatabaseService
    {
        private IControlDatabaseRepository _controlDatabaseRepository;
        public ControlDatabaseService(IControlDatabaseRepository controlDatabaseRepository)
        {
            _controlDatabaseRepository = controlDatabaseRepository;
        }

        public void ResetDatabase()
        {
            _controlDatabaseRepository.ResetDatabase();
        }
    }
}
