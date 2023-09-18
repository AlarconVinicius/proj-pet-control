using PetApi.Domain.Dtos;
using PetApi.Domain.Entities;

namespace PetApi.Extensions.PetGuardianExtension;

public static class PetGuardianExtension
{
	public static PetGuardian ToDomain(this PetGuardianRequest value)
	{
		return new PetGuardian(value.firstName, value.lastName);
	}

	public static PetGuardianResponse ToDto(this PetGuardian value)
	{
		return new PetGuardianResponse(value.Id, value.FirstName, value.LastName, value.CreatedAt, value.UpdatedAt);
	}
}
