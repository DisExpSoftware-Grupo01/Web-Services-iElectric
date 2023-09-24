using System.ComponentModel.DataAnnotations;

namespace web_services_ielectric.Plans.Resources;

public class SavePlanResource
{
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }
    [Required]
    public int Price { get; set; }
}