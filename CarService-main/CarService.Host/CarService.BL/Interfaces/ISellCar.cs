using CarService.Models.Responses;

namespace CarService.BL.Interfaces
{
    internal interface ICarSaleService
    {
        SellCarResult SellCar(Guid carId, Guid customerId);
    }
}
