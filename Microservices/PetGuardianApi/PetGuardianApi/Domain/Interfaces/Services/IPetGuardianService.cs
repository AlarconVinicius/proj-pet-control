using PetGuardianApi.Domain.Dtos;

namespace PetGuardianApi.Domain.Interfaces.Services;

public interface IPetGuardianService
{
	Task<PetGuardianResponse> AddPetGuardian(PetGuardianRequest objeto);

	Task UpdatePetGuardian(Guid id, PetGuardianRequest objeto);

	Task DeletePetGuardian(Guid id);

	Task<IEnumerable<PetGuardianResponse>> GetPetGuardians();

	Task<PetGuardianResponse> GetPetGuardianById(Guid id);
}
