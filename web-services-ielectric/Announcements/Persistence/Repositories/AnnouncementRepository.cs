using Microsoft.EntityFrameworkCore;
using web_services_ielectric.Announcements.Domain.Models;
using web_services_ielectric.Announcements.Domain.Repositories;
using web_services_ielectric.Shared.Persistence.Contexts;
using web_services_ielectric.Shared.Persistence.Repositories;

namespace web_services_ielectric.Announcements.Persistence.Repositories;

public class AnnouncementRepository : BaseRepository, IAnnouncementRepository
{
    public AnnouncementRepository(AppDbContext context) : base(context) { }

    public async Task AddAsync(Announcement announcement)
    {
        await _context.Announcements.AddAsync(announcement);
    }

    public async Task<Announcement> FindByIdAsync(long id)
    {
        return await _context.Announcements.FindAsync(id);
    }

    public async Task<IEnumerable<Announcement>> ListAsync()
    {
        return await _context.Announcements.ToListAsync();
    }

    public void Remove(Announcement announcement)
    {
        _context.Announcements.Remove(announcement);
    }

    public void Update(Announcement announcement)
    {
        _context.Announcements.Update(announcement);
    }
}