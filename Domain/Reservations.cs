namespace Domain
{
    public class Reservations
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public Guid RestaurantId { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }
        public double Cost { get; set;}

        public Reservations() { }
        public Reservations(Guid customerId, Guid restaurantId, DateTime date, double cost, string description = "")
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            RestaurantId = restaurantId;
            Date = date;
            Cost = cost;
            Description = description;
        }
    }
}
