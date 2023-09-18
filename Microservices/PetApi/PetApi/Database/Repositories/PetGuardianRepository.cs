using Microsoft.EntityFrameworkCore;
using PetApi.Data.Configuration;
using PetApi.Domain.Entities;
using PetApi.Domain.Interfaces.Repositories;

namespace PetApi.Database.Repositories;

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
		objeto.UpdatedAt = DateTime.Now;
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