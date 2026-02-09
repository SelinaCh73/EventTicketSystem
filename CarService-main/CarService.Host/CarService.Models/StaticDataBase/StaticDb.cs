using CarService.Models.Dto;

namespace CarService.Models.StaticDataBase
{
    public static class StaticDb
    {
        public static List<Car> Cars { get; set; } = new List<Car>()
        {
            new Car() { Id=Guid.NewGuid(), Model="BMW", Year=2010 },
            new Car() { Id=Guid.NewGuid(), Model="Audi", Year=2012 },
            new Car() { Id=Guid.NewGuid(), Model="Mercedes", Year=2015 },
        };
    }
}
