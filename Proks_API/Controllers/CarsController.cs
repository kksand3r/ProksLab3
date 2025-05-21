using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proks_API.DTOs;
using Proks_API.Models;
using Proks_API.Services;

namespace Proks_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDto>>> GetCars()
        {
            var cars = await _context.Cars
                .Include(c => c.Brand)
                .Include(c => c.FuelType)
                .Include(c => c.TransmissionType)
                .ToListAsync();

            var carDtos = cars.Select(c => ToDto(c)).ToList();
            return Ok(carDtos);
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarDto>> GetCar(int id)
        {
            var car = await _context.Cars
                .Include(c => c.Brand)
                .Include(c => c.FuelType)
                .Include(c => c.TransmissionType)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
                return NotFound();

            return Ok(ToDto(car));
        }

        // GET: api/Cars/Brand/3
        [HttpGet("Brand/{brandId}")]
        public async Task<ActionResult<IEnumerable<CarDto>>> GetCarsByBrand(int brandId)
        {
            var cars = await _context.Cars
                .Where(c => c.BrandId == brandId)
                .Include(c => c.Brand)
                .Include(c => c.FuelType)
                .Include(c => c.TransmissionType)
                .ToListAsync();

            var carDtos = cars.Select(c => ToDto(c)).ToList();
            return Ok(carDtos);
        }

        // POST: api/Cars
        [HttpPost]
        public async Task<ActionResult<CarDto>> PostCar(CarDto carDto)
        {
            var car = FromDto(carDto);

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCar), new { id = car.Id }, ToDto(car));
        }

        // PUT: api/Cars/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, CarDto carDto)
        {
            if (id != carDto.Id)
                return BadRequest();

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
                return NotFound();

            // вручну оновлюємо поля
            car.BrandId = carDto.BrandId;
            car.Model = carDto.Model;
            car.Year = carDto.Year;
            car.CarNumber = carDto.CarNumber;
            car.FuelTypeId = carDto.FuelTypeId;
            car.TransmissionTypeId = carDto.TransmissionTypeId;
            car.EngineCapacity = carDto.EngineCapacity;
            car.Seats = carDto.Seats;
            car.ImagePath = carDto.ImagePath;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
                return NotFound();

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }

        // ✨ Мапінг вручну: Car → CarDto
        private CarDto ToDto(Car car)
        {
            return new CarDto
            {
                Id = car.Id,
                BrandId = car.BrandId,
                Model = car.Model,
                Year = car.Year,
                CarNumber = car.CarNumber,
                FuelTypeId = car.FuelTypeId,
                TransmissionTypeId = car.TransmissionTypeId,
                EngineCapacity = car.EngineCapacity,
                Seats = car.Seats,
                ImagePath = car.ImagePath,
            };
        }

        // ✨ Мапінг вручну: CarDto → Car
        private Car FromDto(CarDto dto)
        {
            return new Car
            {
                Id = dto.Id,
                BrandId = dto.BrandId,
                Model = dto.Model,
                Year = dto.Year,
                CarNumber = dto.CarNumber,
                FuelTypeId = dto.FuelTypeId,
                TransmissionTypeId = dto.TransmissionTypeId,
                EngineCapacity = dto.EngineCapacity,
                Seats = dto.Seats,
                ImagePath = dto.ImagePath,
            };
        }
    }
}
