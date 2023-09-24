using web_services_ielectric.Appliances.Domain.Models;
using web_services_ielectric.Shared.Domain.Services.Communication;

namespace web_services_ielectric.Appliances.Domain.Services.Communication;

public class ApplianceResponse : BaseResponse<Appliance>
{
    public ApplianceResponse(string message) : base(message) { }
    public ApplianceResponse(Appliance appliance) : base(appliance) { }
}