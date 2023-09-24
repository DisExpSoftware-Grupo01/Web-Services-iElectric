namespace web_services_ielectric.Appliances.Resources;

public class SaveApplianceResource
{
    public long ClientId { get; set; }
    public long ApplianceModelId { get; set; }
    public string PurchaseDate { get; set; }
}