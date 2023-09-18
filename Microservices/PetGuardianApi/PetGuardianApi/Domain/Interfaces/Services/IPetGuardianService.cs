using PetGuardianApi.Domain.Entities;

namespace PetGuardianApi.Domain.Interfaces.Services;

public interface IPetGuardianService
{
	Task AddPetGuardian(PetGuardian objeto);

	Task UpdatePetGuardian(Guid id, PetGuardian objeto);

	Task DeletePetGuardian(Guid id);

	Task<List<PetGuardian>> GetPetGuardians();

	Task<PetGuardian> GetPetGuardianById(Guid id);
}
