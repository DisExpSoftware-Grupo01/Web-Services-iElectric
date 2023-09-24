using web_services_ielectric.Announcements.Domain.Models;
using web_services_ielectric.Announcements.Domain.Services.Communication;

namespace web_services_ielectric.Announcements.Domain.Services;

public interface IAnnouncementService
{
    Task<IEnumerable<Announcement>> ListAsync();
    Task<AnnouncementResponse> GetByIdAsync(long id);
    Task<AnnouncementResponse> SaveAsync(Announcement announcement);
    Task<AnnouncementResponse> UpdateAsync(long id, Announcement announcement);
    Task<AnnouncementResponse> DeleteAsync(long id);
}