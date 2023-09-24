namespace web_services_ielectric.Announcements.Resources;

public class AnnouncementResource
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
    public string UrlToImage { get; set; }
    public string TypeOfAnnouncement { get; set; }
    public bool Visible { get; set; }
}