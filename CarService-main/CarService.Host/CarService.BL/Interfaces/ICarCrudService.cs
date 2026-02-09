using CarService.Models.Dto;

namespace CarService.BL.Interfaces
{
    public interface ICarService
    {
        void CreateCar(Car car);
        void RemoveCar(Guid carId);
        List<Car> GetCars();
        Car? GetCarById(Guid carId);
    }
}
