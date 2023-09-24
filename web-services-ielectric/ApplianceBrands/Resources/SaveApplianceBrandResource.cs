using System.ComponentModel.DataAnnotations;

namespace web_services_ielectric.ApplianceBrands.Resources;

public class SaveApplianceBrandResource
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string ImgPath { get; set; }
}