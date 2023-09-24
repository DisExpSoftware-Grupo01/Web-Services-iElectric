using web_services_ielectric.Appointments.Domain.Models;
using web_services_ielectric.Technicians.Domain.Models;

namespace web_services_ielectric.SpareRequests.Domain.Models;

public class SpareRequest
{
    public long Id { get; set; }
    public string Description { get; set; }
    public string Date { get; set; }
    public string ImagePath { get; set; }

    //Relationships -- Relación de muchos a uno
    public long TechnicianId { get; set; }
    public long AppointmentId { get; set; }
    public Technician Technician { get; set; }
    public Appointment Appointment { get; set; }
}