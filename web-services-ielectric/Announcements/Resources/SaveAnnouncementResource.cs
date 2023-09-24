using System.ComponentModel.DataAnnotations;

namespace web_services_ielectric.Announcements.Resources;

public class SaveAnnouncementResource
{
    [Required]
    [MaxLength(30)]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public string Content { get; set; }

    [Required]
    public string UrlToImage { get; set; }

    [Required]
    [Range(1, 2)]
    public int TypeOfAnnouncement { get; set; }

    [Required]
    public bool Visible { get; set; }
}