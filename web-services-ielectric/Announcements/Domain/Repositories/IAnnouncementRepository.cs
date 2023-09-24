using web_services_ielectric.Announcements.Domain.Models;

namespace web_services_ielectric.Announcements.Domain.Repositories;

public interface IAnnouncementRepository
{
    Task<IEnumerable<Announcement>> ListAsync();
    Task AddAsync(Announcement announcement);
    Task<Announcement> FindByIdAsync(long id);
    void Update(Announcement announcement);
    void Remove(Announcement announcement);
}