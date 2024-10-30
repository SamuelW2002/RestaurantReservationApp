using Infrastructure.Interfaces;
using AppLogic.Interfaces;
using Domain;
using Utility;
using Utility.ReadLine;

namespace AppLogic.Services
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository _customerRepository;
        private ILineReader _lineReader;
        public CustomerService(ICustomerRepository customerRepository, ILineReader lineReader)
        {
            _customerRepository = customerRepository;
            _lineReader = lineReader;
        }
        public void AddCustomer()
        {
            try
            {
                Console.WriteLine("Enter the customer's first name: ");
                string firstName = _lineReader.ReadLine();

                Console.WriteLine("Enter the customer's last name: ");
                string lastName = _lineReader.ReadLine();

                var customer = new Customer(firstName!, lastName!);
                _customerRepository.AddCustomer(customer);

                Console.WriteLine("Customer added successfully.");
            }
            catch (ContractException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteCustomer()
        {
            try
            {
                Console.Write("Enter the customer's first name: ");
                string firstName = _lineReader.ReadLine();

                Console.Write("Enter the customer's last name: ");
                string lastName = _lineReader.ReadLine();
                
                Customer customerToRemove = _customerRepository.FindCustomerByName(firstName, lastName);

                Contracts.Require(customerToRemove != null, "Customer does not exist.");

                _customerRepository.DeleteCustomer(customerToRemove);
                Console.WriteLine("Customer has been deleted.");
            }
            catch (ContractException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ShowAllCustomers()
        {
            try
            {
                var customers = _customerRepository.GetAllCustomers();

                Contracts.Require(customers != null, "No customers found.");

                Console.WriteLine("List of customers:");
                foreach (var customer in customers)
                {
                    Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
                }
            }
            catch (ContractException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
