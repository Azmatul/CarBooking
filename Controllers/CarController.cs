using CarBooking.Data;
using CarBooking.Dtos;
using CarBooking.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public CarController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            var allCars = await context.Cars.ToListAsync();

            return Ok(allCars);
        }

        [HttpGet("{CarId}")]
        public async Task<IActionResult> GetCarById(string CarId)
        {
            var car = await context.Cars.FindAsync(CarId);
            if (car == null) return NotFound();
            return Ok(car);
        }


        [HttpPost]
        public async Task<IActionResult> AddCar([FromBody] CarDto carDto)
        {
            if (carDto == null)
            {
                return BadRequest("Invalid Car Data");
            }

            var car = new Car
            {
                CarId = carDto.CarId,
                CarCompany = carDto.CarCompany,
                CarModel = carDto.CarModel,
                Year = carDto.Year
            };

            context.Add(car);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCarById), new { id = car.CarId }, car);
        }


        [HttpPut("{CarId}")]
        public async Task<IActionResult> UpdateCar(string CarId,[FromBody]CarDto carDto)
        {
            if(carDto == null || CarId != carDto.CarId)
            {
                return BadRequest("Invalid Car data or ID ");
            }

            var car = await context.Cars.FindAsync(CarId);

            if(car == null)
            {
                return NotFound("Car Not Found");
            }

            car.CarId = carDto.CarId;
            car.CarCompany = carDto.CarCompany;
            car.CarModel = carDto.CarModel;
            car.Year = carDto.Year;

            context.Cars.Update(car);
            await context.SaveChangesAsync();

            return Ok(car);
        }
    }
}
