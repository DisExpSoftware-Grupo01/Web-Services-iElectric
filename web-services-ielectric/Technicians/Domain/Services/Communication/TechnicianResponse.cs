using web_services_ielectric.Shared.Domain.Services.Communication;
using web_services_ielectric.Technicians.Domain.Models;

namespace web_services_ielectric.Technicians.Domain.Services.Communication;

public class TechnicianResponse : BaseResponse<Technician>
{
    public TechnicianResponse(string message) : base(message) { }
    public TechnicianResponse(Technician resource) : base(resource) { }
}