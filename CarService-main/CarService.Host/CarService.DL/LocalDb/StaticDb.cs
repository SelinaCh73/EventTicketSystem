using CarService.Models.Dto;

namespace CarService.DL.LocalDb
{
    // Симулирана база данни за тестове и демонстрации
    internal static class StaticDatabase
    {
        public static List<Car> CarList = new List<Car>
        {
            new Car { Id = Guid.NewGuid(), Model = "Toyota Corolla", Year = 2020 },
            new Car { Id = Guid.NewGuid(), Model = "Honda Civic", Year = 2019 },
            new Car { Id = Guid.NewGuid(), Model = "Ford Focus", Year = 2021 }
        };

        public static List<Customer> CustomerList = new List<Customer>
        {
            new Customer
            {
                Id = Guid.NewGuid(),
                Name = "Ivan Ivanov",
                Email = "ivan@domain.com"
            },
            new Customer
            {
                Id = Guid.NewGuid(),
                Name = "Maria Petrova",
                Email = "maria@domain.com"
            }
        };
    }
}
