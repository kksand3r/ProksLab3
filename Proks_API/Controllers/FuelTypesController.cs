using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proks_API.DTOs;
using Proks_API.Models;
using Proks_API.Services;

namespace Proks_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FuelTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FuelTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FuelTypeDto>>> GetFuelTypes()
        {
            var fuelTypes = await _context.FuelTypes
                .Select(f => new FuelTypeDto
                {
                    Id = f.Id,
                    Name = f.Name
                })
                .ToListAsync();

            return Ok(fuelTypes);
        }

        // GET: api/FuelTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FuelTypeDto>> GetFuelType(int id)
        {
            var fuelType = await _context.FuelTypes.FindAsync(id);

            if (fuelType == null)
            {
                return NotFound();
            }

            return new FuelTypeDto
            {
                Id = fuelType.Id,
                Name = fuelType.Name
            };
        }

        // POST: api/FuelTypes
        [HttpPost]
        public async Task<ActionResult<FuelTypeDto>> PostFuelType(FuelTypeDto dto)
        {
            var fuelType = new FuelType
            {
                Name = dto.Name
            };

            _context.FuelTypes.Add(fuelType);
            await _context.SaveChangesAsync();

            dto.Id = fuelType.Id;

            return CreatedAtAction(nameof(GetFuelType), new { id = dto.Id }, dto);
        }

        // PUT: api/FuelTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFuelType(int id, FuelTypeDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var fuelType = await _context.FuelTypes.FindAsync(id);
            if (fuelType == null)
            {
                return NotFound();
            }

            fuelType.Name = dto.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuelTypeExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/FuelTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFuelType(int id)
        {
            var fuelType = await _context.FuelTypes.FindAsync(id);
            if (fuelType == null)
            {
                return NotFound();
            }

            _context.FuelTypes.Remove(fuelType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FuelTypeExists(int id)
        {
            return _context.FuelTypes.Any(e => e.Id == id);
        }
    }
}
