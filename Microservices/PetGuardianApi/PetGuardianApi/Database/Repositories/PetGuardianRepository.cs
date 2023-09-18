using Microsoft.EntityFrameworkCore;
using PetGuardianApi.Data.Configuration;
using PetGuardianApi.Domain.Entities;
using PetGuardianApi.Domain.Interfaces.Repositories;

namespace PetGuardianApi.Database.Repositories;

public class PetGuardianRepository : IPetGuardianRepository
{
	protected readonly BaseDbContext _context;

	public PetGuardianRepository(BaseDbContext context)
	{
		_context = context;
	}
	public async Task AddPetGuardian(PetGuardian objeto)
	{
		await _context.PetGuardian
					  .AddAsync(objeto);
		await _context.SaveChangesAsync();
	}

	public async Task UpdatePetGuardian(PetGuardian objeto)
	{
		_context.PetGuardian
				.Update(objeto);
		await _context.SaveChangesAsync();
	}

	public async Task DeletePetGuardian(Guid id)
	{
		var objeto = await GetPetGuardianById(id);
		_context.PetGuardian
				.Remove(objeto);
		await _context.SaveChangesAsync();
	}

	public async Task<List<PetGuardian>> GetPetGuardians()
	{
		return await _context.PetGuardian
							 .AsNoTracking()
							 .ToListAsync();
	}

	public async Task<PetGuardian> GetPetGuardianById(Guid id)
	{
		return await _context.PetGuardian
							 .FindAsync(id);
	}

}