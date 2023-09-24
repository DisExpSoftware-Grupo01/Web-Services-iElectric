using Microsoft.EntityFrameworkCore;
using web_services_ielectric.Appliances.Domain.Models;
using web_services_ielectric.Appliances.Domain.Repositories;
using web_services_ielectric.Shared.Persistence.Contexts;
using web_services_ielectric.Shared.Persistence.Repositories;

namespace web_services_ielectric.Appliances.Persistence.Repositories;

public class ApplianceRepository : BaseRepository, IApplianceRepository
{
    public ApplianceRepository(AppDbContext context) : base(context) { }

    public async Task AddAsync(Appliance appliance)
    {
        await _context.Appliances.AddAsync(appliance);
    }

    public async Task<IEnumerable<Appliance>> FindByClientIdAsync(long clientId)
    {
        return await _context.Appliances
            .Where(p => p.ClientId == clientId)
            .ToListAsync();
    }

    public async Task<Appliance> FindByIdAsync(long id)
    {
        return await _context.Appliances.FindAsync(id);
    }

    public void Remove(Appliance appliance)
    {
        _context.Appliances.Remove(appliance);
    }

    public void Update(Appliance appliance)
    {
        _context.Appliances.Update(appliance);
    }
}