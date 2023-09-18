using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetApi.Domain.Entities;

namespace PetApi.Database.Configuration.EntityConfigs;

public class PetGuardianConfiguration : IEntityTypeConfiguration<PetGuardian>
{
	public void Configure(EntityTypeBuilder<PetGuardian> builder)
	{
		builder.ToTable("tbl_pet_guardians");

		builder.HasKey(e => e.Id);

		builder.Property(e => e.FirstName)
			   .HasColumnName("first_name")
			   .HasMaxLength(100)
			   .IsRequired();
		builder.Property(e => e.LastName)
			   .HasColumnName("last_name")
			   .HasMaxLength(100)
			   .IsRequired();
	}
}
