using Infrastructure.Interfaces;
using Domain;

namespace Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {

        private readonly DatabaseContext _databaseContext;

        public ReservationRepository(DatabaseContext context)
        {
            _databaseContext = context;
        }

        public void AddReservation(Reservations reservation)
        {
            _databaseContext.Add(reservation);

            SaveChanges();
        }

        private void SaveChanges()
        {
            _databaseContext.SaveChanges();
        }

        public List<Reservations> GetAllReservations()
        {
            return _databaseContext.Reservations.ToList();
        }

        public List<Reservations> GetReservationsByCustomerId(Guid customerId)
        {
            return _databaseContext.Reservations
                .Where(a => a.CustomerId == customerId)
                .ToList();
        }

        public List<Reservations> GetReservationsByRestaurantId(Guid restaurantId)
        {
            return _databaseContext.Reservations
                .Where(a => a.RestaurantId == restaurantId)
                .ToList();
        }
    }
}
