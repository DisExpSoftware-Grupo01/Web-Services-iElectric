using web_services_ielectric.Appointments.Domain.Models;
using web_services_ielectric.Appointments.Domain.Services.Communication;

namespace web_services_ielectric.Appointments.Domain.Services;

public interface IAppointmentService
{
    Task<IEnumerable<Appointment>> ListAsync();
    Task<AppointmentResponse> GetByIdAsync(long id);
    Task<AppointmentResponse> SaveAsync(Appointment appointment);
    Task<AppointmentResponse> UpdateAsync(long id, Appointment appointment);
    Task<AppointmentResponse> DeleteAsync(long id);
}