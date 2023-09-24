using Microsoft.EntityFrameworkCore;
using web_services_ielectric.Plans.Domain.Models;
using web_services_ielectric.Plans.Domain.Repositories;
using web_services_ielectric.Shared.Persistence.Contexts;
using web_services_ielectric.Shared.Persistence.Repositories;

namespace web_services_ielectric.Plans.Persistence.Repositories;

public class PlanRepository : BaseRepository, IPlanRepository
{
    public PlanRepository(AppDbContext context) : base(context)
    {
    }

    public async Task AddAsync(Plan plan)
    {
        await _context.Plans.AddAsync(plan);
    }

    public async Task<Plan> FindById(long planId)
    {
        return await _context.Plans.FindAsync(planId);
    }

    public async Task<IEnumerable<Plan>> ListAsync()
    {
        return await _context.Plans.ToListAsync();
    }

    public void Remove(Plan plan)
    {
        _context.Plans.Remove(plan);
    }

    public void Update(Plan plan)
    {
        _context.Plans.Update(plan);
    }
}