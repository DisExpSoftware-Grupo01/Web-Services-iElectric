using System.ComponentModel;

namespace web_services_ielectric.Announcements.Domain.Models;

public enum ETypeOfAnnouncement : byte
{
    [Description("Informative")]
    Informative = 1,
    [Description("Advertisement")]
    Advertisement = 2,
}