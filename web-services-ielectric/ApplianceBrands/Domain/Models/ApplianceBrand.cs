using web_services_ielectric.ApplianceModels.Domain.Models;

namespace web_services_ielectric.ApplianceBrands.Domain.Models;

public class ApplianceBrand
{
    // Properties
    public long Id { get; set; }
    public string Name { get; set; }
    public string ImgPath { get; set; }
    public List<ApplianceModel> ApplianceModels { get; set; }
}