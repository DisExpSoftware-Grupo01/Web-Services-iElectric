using web_services_ielectric.ApplianceBrands.Domain.Models;
using web_services_ielectric.Shared.Domain.Services.Communication;

namespace web_services_ielectric.ApplianceBrands.Domain.Services.Communication;

public class ApplianceBrandResponse : BaseResponse<ApplianceBrand>
{
    public ApplianceBrandResponse(string message) : base(message) { }
    public ApplianceBrandResponse(ApplianceBrand applianceBrand) : base(applianceBrand) { }
}