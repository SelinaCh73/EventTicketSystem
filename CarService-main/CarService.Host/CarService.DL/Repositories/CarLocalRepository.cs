using CarService.DL.Interfaces;
using CarService.DL.LocalDb;
using CarService.Models.Dto;

namespace CarService.DL.Repositories
{
    // Локална реализация на репозитория за автомобили
    [Obsolete($"Please use: {nameof(CarMongoRepository)}")]
    internal class CarRepositoryLocal : ICarDataRepository
    {
        public void AddCar(Car car)
        {
            StaticDatabase.CarList.Add(car);
        }

        public void RemoveCar(Guid? carId)
        {
            StaticDatabase.CarList.RemoveAll(c => c.Id == carId);
        }

        public List<Car> GetAllCars()
        {
            return StaticDatabase.CarList;
        }

        public Car? GetCarById(Guid? carId)
        {
            return StaticDatabase.CarList.FirstOrDefault(c => c.Id == carId);
        }
    }
}
