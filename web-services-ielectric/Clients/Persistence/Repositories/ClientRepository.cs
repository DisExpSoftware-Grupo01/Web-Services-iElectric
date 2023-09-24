using Microsoft.EntityFrameworkCore;
using web_services_ielectric.Clients.Domain.Models;
using web_services_ielectric.Clients.Domain.Repositories;
using web_services_ielectric.Shared.Persistence.Contexts;
using web_services_ielectric.Shared.Persistence.Repositories;

namespace web_services_ielectric.Clients.Persistence.Repositories;

public class ClientRepository : BaseRepository, IClientRepository
{
    public ClientRepository(AppDbContext context) : base(context) { }
    public async Task AddAsync(Client client)
    {
        await _context.Clients.AddAsync(client);
    }

    public async Task<Client> FindByIdAsync(long id)
    {
        return await _context.Clients.FindAsync(id);
    }

    public async Task<Client> FindByUserIdAsync(long userId)
    {
        return await _context.Clients
            .Where(p => p.UserId == userId)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Client>> ListAsync()
    {
        return await _context.Clients.ToListAsync();
    }

    public void Remove(Client client)
    {
        _context.Clients.Remove(client);
    }

    public void Update(Client client)
    {
        _context.Clients.Update(client);
    }
    
}