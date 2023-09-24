namespace web_services_ielectric.ApplianceModels.Resources;

public class ApplianceModelResource
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Model { get; set; }
    public string ImgPath { get; set; }
    public long ApplianceBrandId { get; set; }
}