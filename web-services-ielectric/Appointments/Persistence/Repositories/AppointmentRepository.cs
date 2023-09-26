using Microsoft.EntityFrameworkCore;
using web_services_ielectric.Appointments.Domain.Models;
using web_services_ielectric.Appointments.Domain.Repositories;
using web_services_ielectric.Shared.Persistence.Contexts;
using web_services_ielectric.Shared.Persistence.Repositories;

namespace web_services_ielectric.Appointments.Persistence.Repositories;

public class AppointmentRepository : BaseRepository, IAppointmentRepository
{
    public AppointmentRepository(AppDbContext context) : base(context) { }

    public async Task AddAsync(Appointment appointment)
    {
        await _context.Appointments.AddAsync(appointment);
    }

    public async Task<Appointment> FindByIdAsync(long id)
    {
        return await _context.Appointments.FindAsync(id);
    }

    public async Task<IEnumerable<Appointment>> ListAsync()
    {
        return await _context.Appointments.ToListAsync();
    }

    public void Remove(Appointment appointment)
    {
        _context.Appointments.Remove(appointment);
    }

    public async Task<Appointment> FindByDateTechnicianAndClientAsync(string DateReserve, long technicianId, long clientId)
    {
        return await _context.Appointments
            .FirstOrDefaultAsync(a =>
                a.DateReserve == DateReserve &&
                a.TechnicianId == technicianId &&
                a.ClientId == clientId);
    }

    public void Update(Appointment appointment)
    {
        _context.Appointments.Update(appointment);
    }
}