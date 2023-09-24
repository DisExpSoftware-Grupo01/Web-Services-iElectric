namespace web_services_ielectric.SpareRequests.Resources;

public class SpareRequestResource
{
    public long Id { get; set; }
    public string Description { get; set; }
    public string Date { get; set; }
    public string ImagePath { get; set; }
    public long TechnicianId { get; set; }
    public long AppointmentId { get; set; }
}