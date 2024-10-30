using Domain;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public class RestaurantRespository : IRestaurantRepository
    {
        private readonly DatabaseContext _databaseContext;
        public RestaurantRespository(DatabaseContext context)
        {
            _databaseContext = context;
        }

        public Restaurant FindRestaurantByName(string name)
        {
            return _databaseContext.Restaurants
                                     .FirstOrDefault(p => p.Name.ToLower() == name.ToLower());
        }

        public Restaurant FindRestaurantById(Guid restaurantId)
        {
            return _databaseContext.Restaurants.Find(restaurantId);
        }

        public void DeleteRestaurant(Restaurant restaurant)
        {
            _databaseContext.Restaurants.Remove(restaurant);

            _databaseContext.SaveChanges();
        }

        public void AddRestaurant(Restaurant restaurant)
        {
            _databaseContext.Add(restaurant);

            _databaseContext.SaveChanges();
        }

        private void SaveChanges()
        {
            _databaseContext.SaveChanges();
        }

        public List<Restaurant> GetAllRestaurants()
        {
            return _databaseContext.Restaurants.ToList();
        }
    }
}
