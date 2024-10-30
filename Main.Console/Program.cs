using AppLogic.Interfaces;
using AppLogic.Services;
using Infrastructure;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Utility.ReadLine;

namespace RestaurantReservationApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<DatabaseContext>(options =>
                    options.UseSqlite("Data Source=database.db"))
                .AddSingleton<ICustomerRepository, CustomerRepository>()
                .AddSingleton<IRestaurantRepository, RestaurantRespository>()
                .AddSingleton<IReservationRepository, ReservationRepository>()
                .AddSingleton<IControlDatabaseRepository, ControlDatabaseRepository>()
                .AddSingleton<ICustomerService, CustomerService>()
                .AddSingleton<IRestaurantService, RestaurantService>()
                .AddSingleton<IReservationService, ReservationService>()
                .AddSingleton<IControlDatabaseService, ControlDatabaseService>()
                .AddSingleton<ILineReader, LineReader>()
                .AddSingleton<Lazy<ICustomerService>>(sp => new Lazy<ICustomerService>(() => sp.GetRequiredService<ICustomerService>()))
                .AddSingleton<Lazy<IRestaurantService>>(sp => new Lazy<IRestaurantService>(() => sp.GetRequiredService<IRestaurantService>()))
                .AddSingleton<Lazy<IReservationService>>(sp => new Lazy<IReservationService>(() => sp.GetRequiredService<IReservationService>()))
                .AddSingleton<Lazy<IControlDatabaseService>>(sp => new Lazy<IControlDatabaseService>(() => sp.GetRequiredService<IControlDatabaseService>()))
                .BuildServiceProvider();

            var lineReader = serviceProvider.GetService<ILineReader>();

            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                dbContext.Database.EnsureCreated();
            }

            ShowOptions();

            while (ShowMenu(
                lineReader,
                serviceProvider.GetService<Lazy<ICustomerService>>(),
                serviceProvider.GetService<Lazy<IRestaurantService>>(),
                serviceProvider.GetService<Lazy<IReservationService>>(),
                serviceProvider.GetService<Lazy<IControlDatabaseService>>()
                ));
            
        }

        public static bool ShowMenu(
            ILineReader lineReader,
            Lazy<ICustomerService> customerService,
            Lazy<IRestaurantService> restaurantService,
            Lazy<IReservationService> reservationService,
            Lazy<IControlDatabaseService> controlDatabaseService)
        {
            Console.WriteLine();
            Console.WriteLine("Give command: ");

            if (int.TryParse(lineReader.ReadLine(), out int option))
            {
                switch (option)
                {
                    case 1:
                        customerService.Value.AddCustomer();
                        return true;
                    case 2:
                        customerService.Value.DeleteCustomer();
                        return true;
                    case 3:
                        customerService.Value.ShowAllCustomers();
                        return true;
                    case 4:
                        restaurantService.Value.AddRestaurant();
                        return true;
                    case 5:
                        restaurantService.Value.DeleteRestaurant();
                        return true;
                    case 6:
                        restaurantService.Value.ShowAllRestaurants();
                        return true;
                    case 7:
                        reservationService.Value.AddReservations();
                        return true;
                    case 8:
                        reservationService.Value.ShowReservations();
                        return true;
                    case 9:
                        controlDatabaseService.Value.ResetDatabase();
                        return true;
                    case 10:
                        ShowOptions();
                        return true;
                    case 11:
                        return false;

                    default:
                        return true;
                }
            }
            return true;
        }

        private static void ShowOptions()
        {
            Console.Clear();

            Console.WriteLine("");
            Console.WriteLine("1 - Add Customer");
            Console.WriteLine("2 - Delete Customer");
            Console.WriteLine("3 - Show all Customers");
            Console.WriteLine("4 - Add Restaurant");
            Console.WriteLine("5 - Delete Restaurant");
            Console.WriteLine("6 - Show all Restaurants");
            Console.WriteLine("7 - Add Reservation");
            Console.WriteLine("8 - See Reservations");
            Console.WriteLine("9 - Reset db");
            Console.WriteLine("10 - Show Menu");
            Console.WriteLine("11 - Close");
        }
    }
}