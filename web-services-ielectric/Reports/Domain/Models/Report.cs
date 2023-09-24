using web_services_ielectric.Appointments.Domain.Models;
using web_services_ielectric.Technicians.Domain.Models;

namespace web_services_ielectric.Reports.Domain.Models;

public class Report
{
    //Properties
    public long Id { get; set; }
    public string Observation { get; set; }
    public string Diagnosis { get; set; }
    public string RepairDescription { get; set; }
    public string Date { get; set; }
    public string ImagePath { get; set; }


    //Relationships -- Relación de muchos a uno
    public long AppointmentId { get; set; }
    public long TechnicianId { get; set; }

    public Appointment Appointment { get; set; }
    public Technician Technician { get; set; } 
}