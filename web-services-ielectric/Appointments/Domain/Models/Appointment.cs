using web_services_ielectric.Clients.Domain.Models;
using web_services_ielectric.Reports.Domain.Models;
using web_services_ielectric.SpareRequests.Domain.Models;
using web_services_ielectric.Technicians.Domain.Models;

namespace web_services_ielectric.Appointments.Domain.Models;

public class Appointment
{
    public long Id { get; set; }
    public string DateReserve { get; set; }
    public string DateAttention { get; set; }
    public string Hour { get; set; }
    public bool Done { get; set; }


    //Relationships - de muchos a uno
    public long ClientId { get; set; }
    public long TechnicianId { get; set; }
    //public long ApplianceId { get; set; }
    public Client Client { get; set; }
    public Technician Technician { get; set; } 
        
        
    //Relationships - Relación de uno a muchos
    public IList<Report> Reports { get; set; } = new List<Report>();
    public IList<SpareRequest> SpareRequests { get; set; } = new List<SpareRequest>();
}