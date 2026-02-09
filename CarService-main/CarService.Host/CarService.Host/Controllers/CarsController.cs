using CarService.BL.Interfaces;
using CarService.Models.Dto;
using CarService.Models.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MapsterMapper;

namespace CarService.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IMapper _mapper;
        private readonly IValidator<AddCarRequest> _validator;

        public CarsController(
            ICarService carService,
            IMapper mapper,
            IValidator<AddCarRequest> validator)
        {
            _carService = carService;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpDelete]
        public IActionResult DeleteCar(Guid carId)
        {
            if (carId == Guid.Empty)
                return BadRequest("ID must be a valid Guid.");

            var car = _carService.GetCarById(carId);
            if (car == null)
                return NotFound($"Car with ID {carId} not found.");

            _carService.RemoveCar(carId);
            return Ok();
        }

        [HttpGet(nameof(GetById))]
        public IActionResult GetById(Guid carId)
        {
            if (carId == Guid.Empty)
                return BadRequest("ID must be a valid Guid.");

            var car = _carService.GetCarById(carId);
            if (car == null)
                return NotFound($"Car with ID {carId} not found.");

            return Ok(car);
        }

        [HttpGet(nameof(GetAll))]
        public IActionResult GetAll()
        {
            var cars = _carService.GetAllCars();
            return Ok(cars);
        }

        [HttpPost]
        public IActionResult AddCar([FromBody] AddCarRequest? carRequest)
        {
            if (carRequest == null)
                return BadRequest("Car data is null.");

            var result = _validator.Validate(carRequest);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            var car = _mapper.Map<Car>(carRequest);
            _carService.AddCar(car);

            return Ok();
        }
    }
}
