using web_services_ielectric.Reports.Domain.Models;
using web_services_ielectric.Shared.Domain.Services.Communication;

namespace web_services_ielectric.Reports.Domain.Services.Communication;

public class ReportResponse : BaseResponse<Report>
{
    public ReportResponse(string message) : base(message)
    {
    }

    public ReportResponse(Report resource) : base(resource)
    {
    }
}