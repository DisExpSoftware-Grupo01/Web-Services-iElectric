using web_services_ielectric.Appointments.Domain.Models;
using web_services_ielectric.Reports.Domain.Models;
using web_services_ielectric.SpareRequests.Domain.Models;

namespace web_services_ielectric.Shared.Domain.Models;

public class Person
{
    //Properties
    public long Id { get; set; }
    public string Names { get; set; }
    public string LastNames { get; set; }
    public long CellphoneNumber { get; set; }
    public string Address { get; set; }
    public long UserId { get; set; }
    
    //Relationships - Relación de uno a muchos
    public IList<Appointment> Appointments { get; set; } = new List<Appointment>();
    public IList<Report> Reports { get; set; } = new List<Report>();
    public IList<SpareRequest> SpareRequests { get; set; } = new List<SpareRequest>();    
}