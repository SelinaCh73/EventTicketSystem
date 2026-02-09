using CarService.DL.Interfaces;
using CarService.DL.LocalDb;
using CarService.Models.Dto;

namespace CarService.DL.Repositories
{
    // Локална реализация на репозитория за клиенти
    internal class CustomerRepositoryLocal : ICustomerDataRepository
    {
        public void AddCustomer(Customer customer)
        {
            StaticDatabase.CustomerList.Add(customer);
        }

        public void RemoveCustomer(Guid customerId)
        {
            StaticDatabase.CustomerList.RemoveAll(c => c.Id == customerId);
        }

        public List<Customer> GetAllCustomers()
        {
            return StaticDatabase.CustomerList;
        }

        public Customer? GetCustomerById(Guid customerId)
        {
            return StaticDatabase.CustomerList
                .FirstOrDefault(c => c.Id == customerId);
        }
    }
}
