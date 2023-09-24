using web_services_ielectric.Plans.Domain.Models;
using web_services_ielectric.Shared.Domain.Services.Communication;

namespace web_services_ielectric.Plans.Domain.Services.Communication;

public class PlanResponse : BaseResponse<Plan>
{
    public PlanResponse(Plan resource) : base(resource)
    {
    }

    public PlanResponse(string message) : base(message)
    {
    }
}