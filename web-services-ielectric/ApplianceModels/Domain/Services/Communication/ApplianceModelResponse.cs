using web_services_ielectric.ApplianceModels.Domain.Models;
using web_services_ielectric.Shared.Domain.Services.Communication;

namespace web_services_ielectric.ApplianceModels.Domain.Services.Communication;

public class ApplianceModelResponse : BaseResponse<ApplianceModel>
{
    public ApplianceModelResponse(string message) : base(message) { }
    public ApplianceModelResponse(ApplianceModel applianceModel) : base(applianceModel) { }
}