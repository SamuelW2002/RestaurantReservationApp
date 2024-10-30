namespace Domain
{
    public class Customer
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public Customer() { }

        public Customer(string firstName, string lastName)
        {
            Id = new Guid();
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
