using AppLogic.Services;
using Domain;
using Infrastructure.Interfaces;
using AppLogic.Interfaces;
using Moq;
using Utility.ReadLine;

namespace AppLogic.Tests
{
    public class ReservationServiceTests
    {
        private Mock<IReservationRepository> _reservationRepositoryMock;
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IRestaurantRepository> _restaurantRepositoryMock;
        private Mock<ICustomerService> _customerServiceMock;
        private Mock<IRestaurantService> _restaurantServiceMock;
        private Mock<ILineReader> _lineReaderMock;
        private ReservationService _reservationService;

        private Customer _testCustomer;
        private Restaurant _testRestaurant;
        private Guid _customerId;
        private Guid _restaurantId;
        private List<Reservations> _reservations;

        [SetUp]
        public void SetUp()
        {
            _reservationRepositoryMock = new Mock<IReservationRepository>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _restaurantRepositoryMock = new Mock<IRestaurantRepository>();
            _customerServiceMock = new Mock<ICustomerService>();
            _restaurantServiceMock = new Mock<IRestaurantService>();
            _lineReaderMock = new Mock<ILineReader>();

            _reservationService = new ReservationService(
                _reservationRepositoryMock.Object,
                _customerRepositoryMock.Object,
                _restaurantRepositoryMock.Object,
                _customerServiceMock.Object,
                _restaurantServiceMock.Object,
                _lineReaderMock.Object);

            SetUpTestData();
        }

        [Test]
        public void ShowAllReservations_ShouldPrintReservations_WhenReservationsExist()
        {
            _reservationRepositoryMock.Setup(x => x.GetAllReservations()).Returns(_reservations);
            _customerRepositoryMock.Setup(x => x.FindCustomerById(_customerId)).Returns(_testCustomer);
            _restaurantRepositoryMock.Setup(x => x.FindRestaurantById(_restaurantId)).Returns(_testRestaurant);

            _reservationService.ShowAllReservations();

            _reservationRepositoryMock.Verify(x => x.GetAllReservations(), Times.Once);
            _customerRepositoryMock.Verify(x => x.FindCustomerById(_customerId), Times.Once);
            _restaurantRepositoryMock.Verify(x => x.FindRestaurantById(_restaurantId), Times.Once);
        }

        [Test]
        public void ShowReservationsForCustomer_ShouldPrintReservations_WhenCustomerExists()
        {
            _customerRepositoryMock.Setup(x => x.FindCustomerByName("John", "Doe")).Returns(_testCustomer);
            _reservationRepositoryMock.Setup(x => x.GetReservationsByCustomerId(_customerId)).Returns(_reservations);
            _restaurantRepositoryMock.Setup(x => x.FindRestaurantById(_restaurantId)).Returns(_testRestaurant);

            _lineReaderMock.SetupSequence(x => x.ReadLine())
                .Returns("John")
                .Returns("Doe");

            _reservationService.ShowAllReservationsForCustomer();

            _customerRepositoryMock.Verify(x => x.FindCustomerByName("John", "Doe"), Times.Once);
            _reservationRepositoryMock.Verify(x => x.GetReservationsByCustomerId(_customerId), Times.Once);
            _restaurantRepositoryMock.Verify(x => x.FindRestaurantById(_restaurantId), Times.Once);
        }

        [Test]
        public void AddReservation_ShouldAddNewReservation_WhenValidDataProvided()
        {
            var date = DateTime.Now.AddDays(1);
            var appointmentDateInput = $"{date:dd/MM/yyyy}";
            var costInput = "50";
            var descriptionInput = "Family Dinner";

            _lineReaderMock.SetupSequence(x => x.ReadLine())
                .Returns("John")
                .Returns("Doe")
                .Returns("Mama Mia Pizzaria")
                .Returns(appointmentDateInput)
                .Returns(costInput);

            _lineReaderMock.SetupSequence(x => x.ReadOptionalLine())
                .Returns(descriptionInput);

            _customerRepositoryMock.Setup(x => x.FindCustomerByName("John", "Doe")).Returns(_testCustomer);
            _restaurantRepositoryMock.Setup(x => x.FindRestaurantByName("Mama Mia Pizzaria")).Returns(_testRestaurant);

            _reservationService.AddReservations();

            _reservationRepositoryMock.Verify(x => x.AddReservation(It.Is<Reservations>(
                a => a.CustomerId == _customerId
                && a.RestaurantId == _restaurantId
                && a.Date.ToString("dd/MM/yyyy") == appointmentDateInput
                && a.Description == descriptionInput
            )), Times.Once);
        }

        private void SetUpTestData()
        {
            _customerId = Guid.NewGuid();
            _restaurantId = Guid.NewGuid();

            _testCustomer = new Customer
            {
                Id = _customerId,
                FirstName = "John",
                LastName = "Doe"
            };

            _testRestaurant = new Restaurant
            {
                Id = _restaurantId,
                Name = "Mama Mia Pizzaria",
            };

            _reservations = new List<Reservations>
            {
                new Reservations(_customerId, _restaurantId, DateTime.Now.AddDays(1), 50, "Family Dinner")
            };
        }
    }
}
