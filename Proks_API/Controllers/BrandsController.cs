using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proks_API.DTOs;
using Proks_API.Models;
using Proks_API.Services;

namespace Proks_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BrandsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Brands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
        {
            var brands = await _context.Brands
                .Select(b => new BrandDto
                {
                    Id = b.Id,
                    Name = b.Name
                })
                .ToListAsync();

            return Ok(brands);
        }

        // GET: api/Brands/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BrandDto>> GetBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            return new BrandDto
            {
                Id = brand.Id,
                Name = brand.Name
            };
        }

        // POST: api/Brands
        [HttpPost]
        public async Task<ActionResult<BrandDto>> PostBrand(BrandDto brandDto)
        {
            var brand = new Brand
            {
                Name = brandDto.Name
            };

            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();

            brandDto.Id = brand.Id;

            return CreatedAtAction(nameof(GetBrand), new { id = brand.Id }, brandDto);
        }

        // PUT: api/Brands/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrand(int id, BrandDto brandDto)
        {
            if (id != brandDto.Id)
            {
                return BadRequest();
            }

            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            brand.Name = brandDto.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // DELETE: api/Brands/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BrandExists(int id)
        {
            return _context.Brands.Any(e => e.Id == id);
        }
    }
}
