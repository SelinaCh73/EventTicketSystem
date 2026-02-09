internal class CarService : ICarService
{
    private readonly ICarRepository _repository;

    public CarService(ICarRepository repository)
    {
        _repository = repository;
    }

    public void CreateCar(Car car)
    {
        if (car == null) return;

        if (car.Id == Guid.Empty)
        {
            car.Id = Guid.NewGuid();
        }

        _repository.AddCar(car);
    }

    public void RemoveCar(Guid carId)
    {
        _repository.DeleteCar(carId);
    }

    public List<Car> GetCars()
    {
        return _repository.GetAllCars();
    }

    public Car? GetCarById(Guid carId)
    {
        return _repository.GetById(carId);
    }
}
