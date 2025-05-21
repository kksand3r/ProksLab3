using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proks_API.DTOs;
using Proks_API.Models;
using Proks_API.Services;

namespace Proks_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransmissionTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TransmissionTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TransmissionTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransmissionTypeDto>>> GetTransmissionTypes()
        {
            var transmissionTypes = await _context.TransmissionTypes
                .Select(t => new TransmissionTypeDto
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync();

            return Ok(transmissionTypes);
        }

        // GET: api/TransmissionTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransmissionTypeDto>> GetTransmissionType(int id)
        {
            var transmissionType = await _context.TransmissionTypes.FindAsync(id);

            if (transmissionType == null)
            {
                return NotFound();
            }

            return new TransmissionTypeDto
            {
                Id = transmissionType.Id,
                Name = transmissionType.Name
            };
        }

        // POST: api/TransmissionTypes
        [HttpPost]
        public async Task<ActionResult<TransmissionTypeDto>> PostTransmissionType(TransmissionTypeDto dto)
        {
            var transmissionType = new TransmissionType
            {
                Name = dto.Name
            };

            _context.TransmissionTypes.Add(transmissionType);
            await _context.SaveChangesAsync();

            dto.Id = transmissionType.Id;

            return CreatedAtAction(nameof(GetTransmissionType), new { id = dto.Id }, dto);
        }

        // PUT: api/TransmissionTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransmissionType(int id, TransmissionTypeDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var transmissionType = await _context.TransmissionTypes.FindAsync(id);
            if (transmissionType == null)
            {
                return NotFound();
            }

            transmissionType.Name = dto.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransmissionTypeExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/TransmissionTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransmissionType(int id)
        {
            var transmissionType = await _context.TransmissionTypes.FindAsync(id);
            if (transmissionType == null)
            {
                return NotFound();
            }

            _context.TransmissionTypes.Remove(transmissionType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransmissionTypeExists(int id)
        {
            return _context.TransmissionTypes.Any(e => e.Id == id);
        }
    }
}
