using CarService.Models.Configurations;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace CarService.Host.Healthchecks
{
    // Проверка на здравословното състояние на MongoDB
    public class MongoDbHealthCheck : IHealthCheck
    {
        private readonly IOptionsMonitor<MongoDbConfiguration> _mongoConfig;

        public MongoDbHealthCheck(IOptionsMonitor<MongoDbConfiguration> mongoConfig)
        {
            _mongoConfig = mongoConfig;
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            bool isHealthy = false;

            try
            {
                var client = new MongoClient(_mongoConfig.CurrentValue.ConnectionString);
                var database = client.GetDatabase(_mongoConfig.CurrentValue.DatabaseName);

                // Проверка чрез ping команда
                database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(cancellationToken);

                isHealthy = true;
            }
            catch (Exception)
            {
                isHealthy = false;
            }

            return Task.FromResult(isHealthy
                ? HealthCheckResult.Healthy("MongoDB е здрав.")
                : new HealthCheckResult(context.Registration.FailureStatus, "MongoDB е нездрав."));
        }
    }
}
