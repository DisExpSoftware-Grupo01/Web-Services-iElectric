using web_services_ielectric.Shared.Domain.Repositories;
using web_services_ielectric.Shared.Persistence.Contexts;

namespace web_services_ielectric.Shared.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}