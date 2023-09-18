using System.ComponentModel.DataAnnotations.Schema;

namespace PetGuardianApi.Domain.Entities;

//[Table("pet_guardians")]
public class PetGuardian : BaseEntity
{
	//[Column("first_name")]
	public string FirstName { get; set; } = string.Empty;
	//[Column("last_name")]
	public string LastName { get; set; } = string.Empty;
}
