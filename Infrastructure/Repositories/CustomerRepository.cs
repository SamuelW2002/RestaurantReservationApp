using Domain;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DatabaseContext _databaseContext;
        public CustomerRepository(DatabaseContext context)
        {
            _databaseContext = context;
        }
        public void AddCustomer(Customer customer)
        {
            _databaseContext.Add(customer);

            SaveChanges();
        }

        public void DeleteCustomer(Customer customer)
        {
            _databaseContext.Customers.Remove(customer);

            _databaseContext.SaveChanges();
        }

        public Customer FindCustomerByName(string firstName, string lastName)
        {
            return _databaseContext.Customers
                .FirstOrDefault(p => p.FirstName.ToLower() == firstName.ToLower() &&
                                     p.LastName.ToLower() == lastName.ToLower());
        }


        public Customer FindCustomerById(Guid customerId)
        {
            return _databaseContext.Customers.Find(customerId);
        }

        private void SaveChanges()
        {
            _databaseContext.SaveChanges();
        }

        public List<Customer> GetAllCustomers()
        {
            return _databaseContext.Customers.ToList();
        }
    }
}
