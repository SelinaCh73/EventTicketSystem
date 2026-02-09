using CarService.Models.Dto;

namespace CarService.DL.Interfaces
{
    
    public interface ICarDataRepository
    {
        void AddCar(Car car);

        void RemoveCar(Guid? carId);

        List<Car> GetAllCars();

        Car? GetCarById(Guid? carId);
    }
}
