using PetGuardianApi.Domain.Entities;
using PetGuardianApi.Domain.Interfaces.Repositories;
using PetGuardianApi.Domain.Interfaces.Services;

namespace PetGuardianApi.Domain.Services;

public class PetGuardianService : IPetGuardianService
{
	private readonly IPetGuardianRepository _repository;

	public PetGuardianService(IPetGuardianRepository repository)
	{
		_repository = repository;
	}

	public async Task AddPetGuardian(PetGuardian objeto)
	{
		await _repository.AddPetGuardian(objeto);
	}

	public async Task UpdatePetGuardian(Guid id, PetGuardian objeto)
	{
		if (PetGuardianDoesNotExists(id).GetAwaiter().GetResult()) throw new Exception("Pet Guardian not found!");

		PetGuardian petGuardianDb = await _repository.GetPetGuardianById(id);

		petGuardianDb.FirstName = objeto.FirstName;
		petGuardianDb.LastName = objeto.LastName;

		await _repository.UpdatePetGuardian(petGuardianDb);
	}

	public async Task DeletePetGuardian(Guid id)
	{
		if (PetGuardianDoesNotExists(id).GetAwaiter().GetResult()) throw new Exception("Pet Guardian not found!");

		await _repository.DeletePetGuardian(id);
	}

	public async Task<PetGuardian> GetPetGuardianById(Guid id)
	{
		if (PetGuardianDoesNotExists(id).GetAwaiter().GetResult()) throw new Exception("Pet Guardian not found!");

		return await _repository.GetPetGuardianById(id);
	}

	public async Task<List<PetGuardian>> GetPetGuardians()
	{
		return await _repository.GetPetGuardians();
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
