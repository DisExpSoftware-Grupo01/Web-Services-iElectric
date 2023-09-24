using System.ComponentModel.DataAnnotations;

namespace web_services_ielectric.SpareRequests.Resources;

public class SaveSpareRequestResource
{
    [Required]
    [MaxLength(300)]
    public string Description { get; set; }

    [Required]
    [MaxLength(10)]
    public string Date { get; set; }

    [Required]
    [MaxLength(100)]
    public string ImagePath { get; set; }

    [Required]
    public long TechnicianId { get; set; }

    [Required]
    public long AppointmentId { get; set; }
}