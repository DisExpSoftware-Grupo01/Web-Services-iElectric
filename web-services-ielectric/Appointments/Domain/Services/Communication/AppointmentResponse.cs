using web_services_ielectric.Appointments.Domain.Models;
using web_services_ielectric.Shared.Domain.Services.Communication;

namespace web_services_ielectric.Appointments.Domain.Services.Communication;

public class AppointmentResponse : BaseResponse<Appointment>
{
    public AppointmentResponse(string message) : base(message) { }

    public AppointmentResponse(Appointment resource) : base(resource) { }
}