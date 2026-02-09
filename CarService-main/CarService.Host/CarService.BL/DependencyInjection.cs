using CarService.BL.Interfaces;
using CarService.BL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CarService.BL
{
    public static class DependencyInjection
    {
        // Разширителен метод за регистрация на бизнес слоя
        public static IServiceCollection
            AddBusinessLayer(this IServiceCollection services)
        {
            // Регистрация на сервизи за коли
            services.AddSingleton<ICarService, CarService>();          // промяна от ICarCrudService / CarCrudService
            services.AddSingleton<ICarSaleService, CarSaleService>();  // промяна от ISellCar / SellCar

            return services;
        }
    }
}
