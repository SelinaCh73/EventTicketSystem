internal class CarSaleService : ICarSaleService
{
    private readonly ICarService _carService;
    private readonly ICustomerRepository _customerRepository;

    public CarSaleService(ICarService carService, ICustomerRepository customerRepository)
    {
        _carService = carService;
        _customerRepository = customerRepository;
    }

    public SellCarResult SellCar(Guid carId, Guid customerId)
    {
        var car = _carService.GetCarById(carId);
        var customer = _customerRepository.GetById(customerId);

        if (car == null)
        {
            throw new ArgumentException($"No car found with ID {carId}");
        }

        if (customer == null)
        {
            throw new ArgumentException($"No customer found with ID {customerId}");
        }

        // Определяне на крайната цена с отстъпка
        var finalPrice = car.BasePrice - customer.Discount;

        return new SellCarResult
        {
            Price = finalPrice,
            Car = car,
            Customer = customer
        };
    }
}
