using web_services_ielectric.Announcements.Domain.Models;
using web_services_ielectric.Shared.Domain.Services.Communication;

namespace web_services_ielectric.Announcements.Domain.Services.Communication;

public class AnnouncementResponse : BaseResponse<Announcement>
{
    public AnnouncementResponse(string message) : base(message) { }
    public AnnouncementResponse(Announcement announcement) : base(announcement) { }
}