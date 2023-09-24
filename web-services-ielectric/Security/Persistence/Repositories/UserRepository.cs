using Microsoft.EntityFrameworkCore;
using web_services_ielectric.Security.Domain.Entities;
using web_services_ielectric.Security.Domain.Repositories;
using web_services_ielectric.Shared.Persistence.Contexts;
using web_services_ielectric.Shared.Persistence.Repositories;

namespace web_services_ielectric.Security.Persistence.Repositories;

public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<User>> ListAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<User> FindByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User> FindByEmailAsync(string email)
    {
        return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
    }

    public bool ExistsByEmail(string email)
    {
        return _context.Users.Any(u => u.Email == email);
    }

    public User FindById(int id)
    {
        return _context.Users.Find(id);
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
    }

    public void Remove(User user)
    {
        _context.Users.Remove(user);
    }
}