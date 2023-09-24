namespace web_services_ielectric.Announcements.Domain.Models;

public class Announcement
{
    // Properties
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
    public string UrlToImage { get; set; }
    public ETypeOfAnnouncement TypeOfAnnouncement { get; set; }
    public bool Visible { get; set; }
}