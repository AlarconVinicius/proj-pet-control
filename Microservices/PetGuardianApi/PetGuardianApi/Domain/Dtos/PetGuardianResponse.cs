namespace PetGuardianApi.Domain.Dtos;

public record PetGuardianResponse (
	Guid id,
	string firstName,
	string lastName, 
	DateTime createdAt,
	DateTime updatedAt
	);
