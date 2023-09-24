using web_services_ielectric.Shared.Domain.Services.Communication;
using web_services_ielectric.SpareRequests.Domain.Models;

namespace web_services_ielectric.SpareRequests.Domain.Services.Communication;

public class SpareRequestResponse : BaseResponse<SpareRequest>
{
    public SpareRequestResponse(string message) : base(message)
    {
    }

    public SpareRequestResponse(SpareRequest resource) : base(resource)
    {
    }
}