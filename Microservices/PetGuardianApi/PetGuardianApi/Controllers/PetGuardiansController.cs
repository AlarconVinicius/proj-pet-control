using Microsoft.AspNetCore.Mvc;
using PetGuardianApi.Domain.Dtos;
using PetGuardianApi.Domain.Interfaces.Services;
using PetGuardianApi.Exceptions;

namespace PetGuardianApi.Controllers
{
	[Route("api/pet-guardians")]
    [ApiController]
    public class PetGuardiansController : ControllerBase
    {
		private readonly IPetGuardianService _petGuardianService;

		public PetGuardiansController(IPetGuardianService petGuardianService)
		{
			_petGuardianService = petGuardianService;
		}

		[HttpGet]
        public async Task<IActionResult> Get()
        {
			try
			{
				IEnumerable<PetGuardianResponse> petGuardianDb = await _petGuardianService.GetPetGuardians();
				if (!petGuardianDb.Any())
				{
					return NotFound();
				}
				return Ok(petGuardianDb);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}

		}

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
			try
			{
				var petGuardianResponse = await _petGuardianService.GetPetGuardianById(id);
				return Ok(petGuardianResponse);
			}
			catch (NotFoundException ex)
			{
				return NotFound();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, PetGuardianRequest petGuardian)
        {
			try
			{
				await _petGuardianService.UpdatePetGuardian(id, petGuardian);
				return NoContent();
			}
			catch (NotFoundException ex)
			{
				return NotFound();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
        }

        [HttpPost]
        public async Task<IActionResult> Post(PetGuardianRequest petGuardian)
        {
			try
			{
				PetGuardianResponse petGuardianDb = await _petGuardianService.AddPetGuardian(petGuardian);

				return CreatedAtAction("Get", new { id = petGuardianDb.id }, petGuardianDb);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
			try
			{
				await _petGuardianService.DeletePetGuardian(id);
				return NoContent();
			}
			catch (NotFoundException ex)
			{
				return NotFound();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
        }
    }
}
