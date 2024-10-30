using Domain;

namespace Infrastructure.Interfaces
{
    public interface ICustomerRepository
    {
        void AddCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
        Customer FindCustomerByName(string firstName, string lastName);
        Customer FindCustomerById(Guid customerId);
        List<Customer> GetAllCustomers();
    }
}
