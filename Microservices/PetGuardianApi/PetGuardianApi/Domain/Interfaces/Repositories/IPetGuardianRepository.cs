using PetGuardianApi.Domain.Entities;

namespace PetGuardianApi.Domain.Interfaces.Repositories;

public interface IPetGuardianRepository
{
	Task AddPetGuardian(PetGuardian objeto);

	Task UpdatePetGuardian(PetGuardian objeto);

	Task DeletePetGuardian(Guid id);

	Task<List<PetGuardian>> GetPetGuardians();

	Task<PetGuardian> GetPetGuardianById(Guid id);
}
