using web_services_ielectric.Shared.Persistence.Contexts;

namespace web_services_ielectric.Shared.Persistence.Repositories;

public class BaseRepository
{
    protected readonly AppDbContext _context;

    public BaseRepository (AppDbContext context)
    {
        _context = context;
    }
}