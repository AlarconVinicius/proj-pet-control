using Microsoft.EntityFrameworkCore;
using PetGuardianApi.Database.Configuration.EntityConfigs;
using PetGuardianApi.Domain.Entities;

namespace PetGuardianApi.Data.Configuration;

public class BaseDbContext : DbContext
{
	public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options) { }

	public DbSet<PetGuardian> PetGuardian { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new PetGuardianConfiguration());

		base.OnModelCreating(modelBuilder);
	}
}
