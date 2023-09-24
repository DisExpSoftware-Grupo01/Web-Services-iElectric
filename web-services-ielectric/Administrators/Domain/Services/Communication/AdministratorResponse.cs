using web_services_ielectric.Administrators.Domain.Models;
using web_services_ielectric.Shared.Domain.Services.Communication;

namespace web_services_ielectric.Administrators.Domain.Services.Communication;

public class AdministratorResponse : BaseResponse<Administrator>
{
    public AdministratorResponse(string message) : base(message) { }
    public AdministratorResponse(Administrator resource) : base(resource) { }
}