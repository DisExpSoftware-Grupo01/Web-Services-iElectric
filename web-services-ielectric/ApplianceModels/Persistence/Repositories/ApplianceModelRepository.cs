using Microsoft.EntityFrameworkCore;
using web_services_ielectric.ApplianceModels.Domain.Models;
using web_services_ielectric.ApplianceModels.Domain.Repositories;
using web_services_ielectric.Shared.Persistence.Contexts;
using web_services_ielectric.Shared.Persistence.Repositories;

namespace web_services_ielectric.ApplianceModels.Persistence.Repositories;

public class ApplianceModelRepository : BaseRepository, IApplianceModelRepository
{
    public ApplianceModelRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<ApplianceModel>> ListAsync()
    {
        return await _context.ApplianceModels.ToListAsync();
    }

    public async Task AddAsync(ApplianceModel applianceModel)
    {
        await _context.ApplianceModels.AddAsync(applianceModel);
    }

    public async Task<ApplianceModel> FindByIdAsync(long id)
    {
        return await _context.ApplianceModels.FindAsync(id);
    }

    public async Task<IEnumerable<ApplianceModel>> FindByApplianceBrandId(long applianceBrandId)
    {
        return await _context.ApplianceModels
            .Where(p => p.ApplianceBrandId == applianceBrandId)
            .Include(p => p.ApplianceBrand)
            .ToListAsync();
    }

    public void Update(ApplianceModel applianceModel)
    {
        _context.ApplianceModels.Update(applianceModel);
    }

    public void Remove(ApplianceModel applianceModel)
    {
        _context.ApplianceModels.Remove(applianceModel);
    }
}