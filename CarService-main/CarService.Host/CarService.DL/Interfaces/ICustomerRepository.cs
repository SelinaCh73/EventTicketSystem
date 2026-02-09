using CarService.Models.Dto;

namespace CarService.DL.Interfaces
{
    // Интерфейс за операции с клиентите в базата данни
    public interface ICustomerDataRepository
    {
        void AddCustomer(Customer customer);

        void RemoveCustomer(Guid customerId);

        List<Customer> GetAllCustomers();

        Customer? GetCustomerById(Guid customerId);
    }
}
