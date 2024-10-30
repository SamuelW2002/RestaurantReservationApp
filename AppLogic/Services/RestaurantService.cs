using AppLogic.Interfaces;
using Infrastructure.Interfaces;
using Domain;
using Utility;
using Utility.ReadLine;

namespace AppLogic.Services
{
    public class RestaurantService : IRestaurantService
    {
        private IRestaurantRepository _restaurantRepository;
        private ILineReader _lineReader;
        public RestaurantService(IRestaurantRepository restaurantRepository, ILineReader lineReader)
        {
            _restaurantRepository = restaurantRepository;
            _lineReader = lineReader;
        }
        public void AddRestaurant()
        {
            try
            {
                Console.WriteLine("Enter restaurant: ");
                string name = _lineReader.ReadLine();

                Console.WriteLine("Enter restaurant's speciality, choose from: ");
                foreach (var specialities in Enum.GetNames(typeof(SpecialityEnum)))
                {
                    Console.WriteLine($"- {specialities}");
                }
                string speciality = _lineReader.ReadLine();

                Restaurant restaurant = new Restaurant(name, speciality);

                _restaurantRepository.AddRestaurant(restaurant);
                Console.WriteLine($"Restaurant {name} added successfully!");
            }
            catch (ContractException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteRestaurant()
        {
            try
            {
                Console.Write("Enter the name of the restaurant to delete: ");
                string name = _lineReader.ReadLine();

                var restaurantToRemove = _restaurantRepository.FindRestaurantByName(name);

                Contracts.Require(restaurantToRemove != null, "This restaurant does not exist");

                _restaurantRepository.DeleteRestaurant(restaurantToRemove);
                Console.WriteLine($"Restaurant {name} deleted successfully!");
            }
            catch (ContractException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ShowAllRestaurants()
        {
            try
            {
                var restaurants = _restaurantRepository.GetAllRestaurants();

                Contracts.Require(restaurants != null, "No restaurants found");

                Console.WriteLine("List of restaurants:");
                foreach (var restaurant in restaurants)
                {
                    Console.WriteLine($"Name: {restaurant.Name}, Speciality: {restaurant.Speciality}");
                }
            }
            catch(ContractException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
