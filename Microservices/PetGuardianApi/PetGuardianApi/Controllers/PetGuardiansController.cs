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

		/// <summary>
		/// Get a list of all pet guardians.
		/// </summary>
		/// <response code="200">Returns a collection of PetGuardianResponse objects.</response>
		/// <response code="404">If no pet guardians are found.</response>
		/// <response code="500">If an unexpected error occurs.</response>
		[ProducesResponseType(typeof(IEnumerable<PetGuardianResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpGet]
        public async Task<ActionResult<IEnumerable<PetGuardianResponse>>> Get()
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

		/// <summary>
		/// Get a pet guardian by their ID.
		/// </summary>
		/// <param name="id">The ID of the pet guardian.</param>
		/// <response code="200">Returns a PetGuardianResponse object if found.</response>
		/// <response code="404">If no pet guardian is found with the specified ID.</response>
		/// <response code="500">If an unexpected error occurs.</response>
		[ProducesResponseType(typeof(PetGuardianResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpGet("{id}")]
        public async Task<ActionResult<PetGuardianResponse>> Get(Guid id)
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

		/// <summary>
		/// Update a pet guardian by their ID.
		/// </summary>
		/// <param name="id">The ID of the pet guardian.</param>
		/// <param name="petGuardian">The updated pet guardian data.</param>
		/// <response code="204">No Content status code if the update is successful.</response>
		/// <response code="404">If no pet guardian is found with the specified ID.</response>
		/// <response code="500">If an unexpected error occurs.</response>
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, PetGuardianRequest petGuardian)
        {
			try
			{
				await _petGuardianService.UpdatePetGuardian(id, petGuardian);
				return NoContent();
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
        }

		/// <summary>
		/// Add a new pet guardian.
		/// </summary>
		/// <param name="petGuardian">The data of the new pet guardian.</param>
		/// <response code="201">Created status code if the creation is successful.</response>
		/// <response code="500">If an unexpected error occurs.</response>
		[ProducesResponseType(typeof(PetGuardianResponse), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpPost]
        public async Task<ActionResult<PetGuardianResponse>> Post(PetGuardianRequest petGuardian)
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

		/// <summary>
		/// Delete a pet guardian by their ID.
		/// </summary>
		/// <param name="id">The ID of the pet guardian to be deleted.</param>
		/// <response code="204">No Content status code if the deletion is successful.</response>
		/// <response code="404">If no pet guardian is found with the specified ID.</response>
		/// <response code="500">If an unexpected error occurs.</response>
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
			try
			{
				await _petGuardianService.DeletePetGuardian(id);
				return NoContent();
			}
			catch (NotFoundException)
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
