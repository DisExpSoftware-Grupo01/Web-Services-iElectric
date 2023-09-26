using web_services_ielectric.Appointments.Domain.Models;
using web_services_ielectric.Appointments.Domain.Repositories;
using web_services_ielectric.Appointments.Domain.Services;
using web_services_ielectric.Appointments.Domain.Services.Communication;
using web_services_ielectric.Shared.Domain.Repositories;

namespace web_services_ielectric.Appointments.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AppointmentService(IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork)
    {
        _appointmentRepository = appointmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<AppointmentResponse> DeleteAsync(long id)
    {
        var existingAppointment = await _appointmentRepository.FindByIdAsync(id);

        if (existingAppointment == null)
            return new AppointmentResponse("Appointment not found.");

        try
        {
            _appointmentRepository.Remove(existingAppointment);
            await _unitOfWork.CompleteAsync();

            return new AppointmentResponse(existingAppointment);
        }
        catch (Exception e)
        {
            return new AppointmentResponse($"An error occurred while deleting the appointment: {e.Message}");
        }
    }

    public async Task<AppointmentResponse> GetByIdAsync(long id)
    {
        var existingAppointment = await _appointmentRepository.FindByIdAsync(id);

        if (existingAppointment == null)
            return new AppointmentResponse("Appointment not found.");

        return new AppointmentResponse(existingAppointment);
    }

    public async Task<IEnumerable<Appointment>> ListAsync()
    {
        return await _appointmentRepository.ListAsync();
    }

    public async Task<AppointmentResponse> SaveAsync(Appointment appointment)
    {
        try
        {
            // Verificar si ya existe un Appointment con los mismos valores
            var existingAppointment = await _appointmentRepository.FindByDateTechnicianAndClientAsync(
                appointment.DateReserve, appointment.TechnicianId, appointment.ClientId);

            if (existingAppointment != null)
            {
                return new AppointmentResponse("An Appointment with the same date, TechnicianId, and ClientId already exists.");
            }

            await _appointmentRepository.AddAsync(appointment);
            await _unitOfWork.CompleteAsync();

            return new AppointmentResponse(appointment);
        }
        catch (Exception e)
        {
            return new AppointmentResponse($"An error occurred while saving the appointment: {e.Message}");
        }
    }

    public async Task<AppointmentResponse> UpdateAsync(long id, Appointment appointment)
    {
        var existingAppointment = await _appointmentRepository.FindByIdAsync(id);

        if (existingAppointment == null)
            return new AppointmentResponse("Appointment not found.");

        existingAppointment.DateAttention = appointment.DateAttention;
        existingAppointment.DateReserve = appointment.DateReserve;
        existingAppointment.Hour = appointment.Hour;
        //existingAppointment.ApplianceId = appointment.ApplianceId;
        existingAppointment.Done = appointment.Done;

        try
        {
            _appointmentRepository.Update(existingAppointment);
            await _unitOfWork.CompleteAsync();
            return new AppointmentResponse(existingAppointment);
        }
        catch(Exception e)
        {
            return new AppointmentResponse($"An error occurred while updating the appointment: {e.Message}");
        }
    }
}