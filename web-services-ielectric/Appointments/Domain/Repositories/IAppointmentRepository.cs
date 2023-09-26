using web_services_ielectric.Appointments.Domain.Models;

namespace web_services_ielectric.Appointments.Domain.Repositories;

public interface IAppointmentRepository
{
    Task<IEnumerable<Appointment>> ListAsync();
    Task AddAsync(Appointment appointment);
    Task<Appointment> FindByIdAsync(long id);
    void Update(Appointment appointment);
    void Remove(Appointment appointment);
    Task<Appointment> FindByDateTechnicianAndClientAsync(string DateReserve, long technicianId, long clientId);
}