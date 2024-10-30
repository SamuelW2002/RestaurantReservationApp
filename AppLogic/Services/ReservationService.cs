using AppLogic.Interfaces;
using Domain;
using Infrastructure.Interfaces;
using Utility;
using Utility.ReadLine;

namespace AppLogic.Services
{
    public class ReservationService : IReservationService
    {
        private IReservationRepository _reservationRepository;
        private ICustomerRepository _customerRepository;
        private IRestaurantRepository _restaurantRepository;
        private ICustomerService _customerService;
        private IRestaurantService _restaurantService;
        private ILineReader _lineReader;
        public ReservationService(IReservationRepository reservationRepository,
            ICustomerRepository customerRepository,
            IRestaurantRepository restaurantRepository,
            ICustomerService customerService,
            IRestaurantService restaurantService,
            ILineReader lineReader)
        {
            _reservationRepository = reservationRepository;
            _customerRepository = customerRepository;
            _restaurantRepository = restaurantRepository;
            _customerService = customerService;
            _restaurantService = restaurantService;
            _lineReader = lineReader;
        }

        public void ShowReservations()
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1 - Show all reservations");
            Console.WriteLine("2 - Show reservations for a certain customer");
            Console.WriteLine("3 - Show reservations for a certain restaurant");

            if (int.TryParse(_lineReader.ReadLine(), out int option))
            {
                switch (option)
                {
                    case 1:
                        ShowAllReservations();
                        break;
                    case 2:
                        ShowAllReservationsForCustomer();
                        break;
                    case 3:
                        ShowAllReservationsForRestaurant();
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please select 1, 2, or 3.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter one of the numbers shown above.");
            }
        }

        internal void ShowAllReservations()
        {
            try
            {
                List<Reservations> reservations = _reservationRepository.GetAllReservations();
                Contracts.Require(reservations.Count != 0, "There are no reservations.");

                foreach (var reservation in reservations)
                {
                    var customer = _customerRepository.FindCustomerById(reservation.CustomerId);
                    var restaurant = _restaurantRepository.FindRestaurantById(reservation.RestaurantId);

                    Console.WriteLine($"Reservation Details:");
                    Console.WriteLine($"- Customer: {customer.FirstName} {customer.LastName}");
                    Console.WriteLine($"- Restaurant: {restaurant.Name}");
                    Console.WriteLine($"- Date: {reservation.Date.ToShortDateString()}");
                    Console.WriteLine($"{(string.IsNullOrEmpty(reservation.Description) ? "" : $"- Description: {reservation.Description}")}");
                    Console.WriteLine($"- Cost: {reservation.Cost}");
                    Console.WriteLine(new string('-', 40));
                }
            }
            catch(ContractException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal void ShowAllReservationsForCustomer()
        {
            try
            {
                _customerService.ShowAllCustomers();
                Console.Write("Enter the customer's first name: ");
                string firstName = _lineReader.ReadLine();

                Console.Write("Enter the customer's last name: ");
                string lastName = _lineReader.ReadLine();

                var customer = _customerRepository.FindCustomerByName(firstName, lastName);
                Contracts.Require(customer != null, "Customer not found.");

                var reservations = _reservationRepository.GetReservationsByCustomerId(customer.Id);
                Contracts.Require(reservations.Count != 0, "There are no reservations for this customer.");

                Console.WriteLine($"Reservations for {firstName} {lastName}:");
                foreach (var reservation in reservations)
                {
                    Restaurant restaurant = _restaurantRepository.FindRestaurantById(reservation.RestaurantId);
                    Console.WriteLine($"- Date: {reservation.Date.ToShortDateString()}" +
                                      $"{(string.IsNullOrEmpty(reservation.Description) ? "" : $", Description: {reservation.Description}")}" +
                                      $", Customer: {customer.FirstName} {customer.LastName}, Reservation cost: {reservation.Cost}");
                }
            }
            catch (ContractException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        internal void ShowAllReservationsForRestaurant()
        {
            try
            {
                _restaurantService.ShowAllRestaurants();
                Console.Write("Enter the restaurant's name: ");
                string name = _lineReader.ReadLine();

                var restaurant = _restaurantRepository.FindRestaurantByName(name);

                if (restaurant != null)
                {
                    var reservations = _reservationRepository.GetReservationsByRestaurantId(restaurant.Id);
                    Contracts.Require(reservations.Count != 0, "There are no reservations for this restaurant.");

                    Console.WriteLine($"Reservations for: {name}:");
                    foreach (var reservation in reservations)
                    {
                        Customer customer = _customerRepository.FindCustomerById(reservation.CustomerId);
                        Console.WriteLine($"- Date: {reservation.Date.ToShortDateString()}" +
                                          $"{(string.IsNullOrEmpty(reservation.Description) ? "" : $", Description: {reservation.Description}")}" +
                                          $", Customer: {customer.FirstName} {customer.LastName}, Reservation cost: {reservation.Cost}");

                    }
                }
                else
                {
                    Console.WriteLine("Restaurant not found.");
                }
            }
            catch (ContractException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AddReservations()
        {
            try
            {
                _customerService.ShowAllCustomers();
                Console.Write("Enter the customer's first name: ");
                string customerFirstName = _lineReader.ReadLine();

                Console.Write("Enter the customer's last name: ");
                string CustomerLastName = _lineReader.ReadLine();

                Customer customer = _customerRepository.FindCustomerByName(customerFirstName, CustomerLastName);
                Contracts.Require(customer != null, "Customer does not exist.");

                _restaurantService.ShowAllRestaurants();
                Console.Write("Enter the restaurant's name: ");
                string restaurantName = _lineReader.ReadLine();

                Restaurant restaurant = _restaurantRepository.FindRestaurantByName(restaurantName);
                Contracts.Require(restaurant != null, "Restaurant does not exist.");

                Console.Write("Enter the reservation date (dd/MM/yyyy): ");
                string dateInput = _lineReader.ReadLine();
                DateTime parsedDate = DateParser.ParseDate(dateInput);
                Contracts.Require(parsedDate >= DateTime.Now, "Reservation date cannot be in the past.");

                Console.Write("Enter the cost for the reservation, use a \",\" for decimals: ");
                string costInput = _lineReader.ReadLine();
                Contracts.Require(double.TryParse(costInput, out double cost), "Please enter a number.");

                Console.Write("Enter a description for the reservation (optional): ");
                string description = _lineReader.ReadOptionalLine();

                var reservation = new Reservations(customer.Id, restaurant.Id, parsedDate, cost, description);

                _reservationRepository.AddReservation(reservation);

                Console.WriteLine("Reservation added successfully.");
            }
            catch (ContractException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
