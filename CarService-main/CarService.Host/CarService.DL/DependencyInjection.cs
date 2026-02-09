using CarService.DL.Interfaces;
using CarService.DL.Repositories;
using CarService.Models.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson;

namespace CarService.DL
{
    public static class DependencyInjection
    {
        // Метод за регистрация на Data Layer услугите
        public static IServiceCollection
            AddDataLayer(this IServiceCollection services, IConfiguration configs)
        {
            // Настройка на Guid сериализацията за MongoDB
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

            // Регистрация на репозиториите
            services
                .AddConfigurations(configs)
                .AddSingleton<ICarDataRepository, CarMongoDataRepository>()      // промяна на имена
                .AddSingleton<ICustomerDataRepository, CustomerRepositoryLocal>(); // промяна на имена

            return services;
        }

        // Конфигурации за MongoDB
        private static IServiceCollection
           AddConfigurations(this IServiceCollection services, IConfiguration configs)
        {
            services.Configure<MongoDbConfiguration>(configs.GetSection(nameof(MongoDbConfiguration)));
            return services;
        }
    }
}
