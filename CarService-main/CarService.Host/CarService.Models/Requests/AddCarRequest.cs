namespace CarService.Models.Requests
{
    public class AddCarRequest
    {
        public string Model { get; set; } = string.Empty;

        public int Year { get; set; }
    }
}
