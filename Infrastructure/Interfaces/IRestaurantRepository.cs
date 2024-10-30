using Domain;

namespace Infrastructure.Interfaces
{
    public interface IRestaurantRepository
    {
        void DeleteRestaurant(Restaurant restaurant);
        void AddRestaurant(Restaurant restaurant);
        Restaurant FindRestaurantByName(string name);
        Restaurant FindRestaurantById(Guid restaurantId);
        List<Restaurant> GetAllRestaurants();
    }
}
