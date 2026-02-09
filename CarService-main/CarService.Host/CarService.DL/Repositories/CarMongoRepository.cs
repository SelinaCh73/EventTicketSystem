using CarService.DL.Interfaces;
using CarService.Models.Configurations;
using CarService.Models.Dto;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CarService.DL.Repositories
{
    internal class CarMongoDataRepository : ICarDataRepository
    {
        private readonly IOptionsMonitor<MongoDbConfiguration> _mongoConfig;
        private readonly ILogger<CarMongoDataRepository> _logger;
        private readonly IMongoCollection<Car> _carCollection;

        public CarMongoDataRepository(
            IOptionsMonitor<MongoDbConfiguration> mongoConfig,
            ILogger<CarMongoDataRepository> logger)
        {
            _mongoConfig = mongoConfig;
            _logger = logger;

            var client = new MongoClient(_mongoConfig.CurrentValue.ConnectionString);
            var database = client.GetDatabase(_mongoConfig.CurrentValue.DatabaseName);
            _carCollection = database.GetCollection<Car>($"{nameof(Car)}s");
        }

        public void AddCar(Car car)
        {
            if (car == null) return;

            try
            {
                _carCollection.InsertOne(car);
            }
            catch (Exception e)
            {
                _logger.LogError("Error adding car to MongoDB: {0} - {1}", e.Message, e.StackTrace);
            }
        }

        public void RemoveCar(Guid? carId)
        {
            if (carId == null || carId == Guid.Empty) return;

            try
            {
                var result = _carCollection.DeleteOne(c => c.Id == carId);

                if (result.DeletedCount == 0)
                {
                    _logger.LogWarning($"No car found with Id: {carId} to delete.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(RemoveCar)}: {e.Message} - {e.StackTrace}");
            }
        }

        public List<Car> GetAllCars()
        {
            return _carCollection.Find(_ => true).ToList();
        }

        public Car? GetCarById(Guid? carId)
        {
            if (carId == null || carId == Guid.Empty) return default;

            try
            {
                return _carCollection.Find(c => c.Id == carId).FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetCarById)}: {e.Message} - {e.StackTrace}");
            }

            return default;
        }
    }
}
