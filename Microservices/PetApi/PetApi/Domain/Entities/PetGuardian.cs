namespace PetApi.Domain.Entities;

public class PetGuardian : BaseEntity
{
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;

	public PetGuardian(string firstName, string lastName)
	{
		FirstName = firstName;
		LastName = lastName;
	}
}
