using Microsoft.EntityFrameworkCore;
using PetApi.Database.Configuration.EntityConfigs;
using PetApi.Domain.Entities;

namespace PetApi.Data.Configuration;

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
