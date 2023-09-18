using System.ComponentModel.DataAnnotations.Schema;

namespace PetApi.Domain.Entities;

public abstract class BaseEntity
{
	[Column("id")]
	public Guid Id { get; set; }
	[Column("created_at")]
	public DateTime CreatedAt { get; set; }
	[Column("updated_at")]
	public DateTime UpdatedAt { get; set; }

	public BaseEntity()
	{
		Id = Guid.NewGuid();
		CreatedAt = DateTime.Now;
		UpdatedAt = DateTime.Now;
	}
}
