using Microsoft.EntityFrameworkCore;
using web_services_ielectric.Administrators.Domain.Models;
using web_services_ielectric.Administrators.Domain.Repositories;
using web_services_ielectric.Shared.Persistence.Contexts;
using web_services_ielectric.Shared.Persistence.Repositories;

namespace web_services_ielectric.Administrators.Persistence.Repositories;

public class AdministratorRepository : BaseRepository, IAdministratorRepository
{
    public AdministratorRepository(AppDbContext context) : base(context) { }

    public async Task AddAsync(Administrator administrator)
    {
        await _context.Administrators.AddAsync(administrator);
    }

    public async Task<Administrator> FindByIdAsync(long id)
    {
        return await _context.Administrators.FindAsync(id);
    }

    public async Task<Administrator> FindByUserIdAsync(long userId)
    {
        return await _context.Administrators
            .Where(p => p.UserId == userId)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Administrator>> ListAsync()
    {
        return await _context.Administrators.ToListAsync();
    }

    public void Remove(Administrator administrator)
    {
        _context.Administrators.Remove(administrator);
    }

    public void Update(Administrator administrator)
    {
        _context.Administrators.Update(administrator);
    }
}