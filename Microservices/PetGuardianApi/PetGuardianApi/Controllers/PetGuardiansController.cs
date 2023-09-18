using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetGuardianApi.Data.Configuration;
using PetGuardianApi.Domain.Entities;

namespace PetGuardianApi.Controllers
{
	[Route("api/pet-guardians")]
    [ApiController]
    public class PetGuardiansController : ControllerBase
    {
        private readonly BaseDbContext _context;

        public PetGuardiansController(BaseDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetGuardian>>> GetPetGuardians()
        {
          if (_context.PetGuardian == null)
          {
              return NotFound();
          }
            return await _context.PetGuardian.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PetGuardian>> GetPetGuardian(Guid id)
        {
          if (_context.PetGuardian == null)
          {
              return NotFound();
          }
            var petGuardian = await _context.PetGuardian.FindAsync(id);

            if (petGuardian == null)
            {
                return NotFound();
            }

            return petGuardian;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPetGuardian(Guid id, PetGuardian petGuardian)
        {
            if (id != petGuardian.Id)
            {
                return BadRequest();
            }

            _context.Entry(petGuardian).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetGuardianExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<PetGuardian>> PostPetGuardian(PetGuardian petGuardian)
        {
          if (_context.PetGuardian == null)
          {
              return Problem("Entity set 'BaseDbContext.PetGuardian'  is null.");
          }
            _context.PetGuardian.Add(petGuardian);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPetGuardian", new { id = petGuardian.Id }, petGuardian);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePetGuardian(Guid id)
        {
            if (_context.PetGuardian == null)
            {
                return NotFound();
            }
            var petGuardian = await _context.PetGuardian.FindAsync(id);
            if (petGuardian == null)
            {
                return NotFound();
            }

            _context.PetGuardian.Remove(petGuardian);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PetGuardianExists(Guid id)
        {
            return (_context.PetGuardian?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
