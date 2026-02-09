namespace CarService.Models.Dto
{
    public class Car
    {
        public Guid Id { get; set; }

        public string Model { get; set; } = string.Empty;

        public int Year { get; set; }

        public decimal BasePrice { get; set; }
    }
}
