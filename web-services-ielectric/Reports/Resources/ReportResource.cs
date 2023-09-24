namespace web_services_ielectric.Reports.Resources;

public class ReportResource
{
    public long Id { get; set; }
    public string Observation { get; set; }
    public string Diagnosis { get; set; }
    public string RepairDescription { get; set; }
    public string Date { get; set; }
    public string ImagePath { get; set; }
    public long AppointmentId { get; set; }
    public long TechnicianId { get; set; }
}