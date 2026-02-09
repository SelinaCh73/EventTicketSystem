using CarService.Models.Dto;

namespace CarService.Models.Responses
{
    public class SellCarResult
    {
        public Car Car { get; set; }

        public Customer Customer { get; set; }

        public decimal Price { get; set; }
    }
}
