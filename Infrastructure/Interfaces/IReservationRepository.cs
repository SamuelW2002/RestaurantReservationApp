using Domain;

namespace Infrastructure.Interfaces
{
    public interface IReservationRepository
    {
        void AddReservation(Reservations reservation);
        List<Reservations> GetAllReservations();
        List<Reservations> GetReservationsByCustomerId(Guid customerId);
        List<Reservations> GetReservationsByRestaurantId(Guid restaurantId);
    }
}
