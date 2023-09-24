namespace web_services_ielectric.Appliances.Domain.Models;

public class Appliance
{
    public long Id { get; set; }
    public long ClientId { get; set; }
    public long ApplianceModelId { get; set; }
    public string PurchaseDate { get; set; }
}