using Microsoft.EntityFrameworkCore;
using web_services_ielectric.Shared.Persistence.Contexts;
using web_services_ielectric.Shared.Persistence.Repositories;
using web_services_ielectric.SpareRequests.Domain.Models;
using web_services_ielectric.SpareRequests.Domain.Repositories;

namespace web_services_ielectric.SpareRequests.Persistence.Repositories;

public class SpareRequestRepository : BaseRepository, ISpareRequestRepository
{
    public SpareRequestRepository(AppDbContext context) : base(context)
    {
    }

    public async Task AddAsync(SpareRequest spareRequest)
    {
        await _context.SpareRequests.AddAsync(spareRequest);
    }

    public async Task<SpareRequest> FindByIdAsync(long id)
    {
        return await _context.SpareRequests.FindAsync(id);
    }

    public async Task<IEnumerable<SpareRequest>> ListAsync()
    {
        return await _context.SpareRequests.ToListAsync();
    }

    public void Remove(SpareRequest spareRequest)
    {
        _context.SpareRequests.Remove(spareRequest);
    }

    public void Update(SpareRequest spareRequest)
    {
        _context.SpareRequests.Update(spareRequest);
    }
}