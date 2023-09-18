using PetGuardianApi.Domain.Dtos;
using PetGuardianApi.Domain.Entities;
using PetGuardianApi.Domain.Interfaces.Repositories;
using PetGuardianApi.Domain.Interfaces.Services;
using PetGuardianApi.Exceptions;
using PetGuardianApi.Extensions.PetGuardianExtension;

namespace PetGuardianApi.Domain.Services;

public class PetGuardianService : IPetGuardianService
{
	private readonly IPetGuardianRepository _repository;

	public PetGuardianService(IPetGuardianRepository repository)
	{
		_repository = repository;
	}

	public async Task<PetGuardianResponse> AddPetGuardian(PetGuardianRequest objeto)
	{
		PetGuardian petGuardianDomain = objeto.ToDomain();
		await _repository.AddPetGuardian(petGuardianDomain);
		return (await _repository.GetPetGuardianById(petGuardianDomain.Id)).ToDto();
	}

	public async Task UpdatePetGuardian(Guid id, PetGuardianRequest objeto)
	{
		if (PetGuardianDoesNotExists(id).GetAwaiter().GetResult()) throw new NotFoundException("Pet Guardian not found!");

		PetGuardian petGuardianDb = await _repository.GetPetGuardianById(id);

		petGuardianDb.FirstName = objeto.firstName;
		petGuardianDb.LastName = objeto.lastName;

		await _repository.UpdatePetGuardian(petGuardianDb);
	}

	public async Task DeletePetGuardian(Guid id)
	{
		if (PetGuardianDoesNotExists(id).GetAwaiter().GetResult()) throw new NotFoundException("Pet Guardian not found!");

		await _repository.DeletePetGuardian(id);
	}

	public async Task<PetGuardianResponse> GetPetGuardianById(Guid id)
	{
		if (PetGuardianDoesNotExists(id).GetAwaiter().GetResult()) throw new NotFoundException("Pet Guardian not found!");

		return (await _repository.GetPetGuardianById(id)).ToDto();
	}

	public async Task<IEnumerable<PetGuardianResponse>> GetPetGuardians()
	{
		return (await _repository.GetPetGuardians()).Select(x => x.ToDto());
	}

	private async Task<bool> PetGuardianDoesNotExists(Guid id)
	{
		PetGuardian petGuardianDb = await _repository.GetPetGuardianById(id);
		if (petGuardianDb is null)
		{
			return true;
		}
		return false;
	}
}
