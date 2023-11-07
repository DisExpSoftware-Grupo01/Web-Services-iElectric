using Moq;
using web_services_ielectric.ApplianceBrands.Domain.Repositories;
using web_services_ielectric.ApplianceModels.Domain.Models;
using web_services_ielectric.ApplianceModels.Domain.Repositories;
using web_services_ielectric.ApplianceModels.Services;
using web_services_ielectric.Appliances.Domain.Models;
using web_services_ielectric.Appliances.Domain.Repositories;
using web_services_ielectric.Appliances.Services;
using web_services_ielectric.Appointments.Domain.Models;
using web_services_ielectric.Appointments.Domain.Repositories;
using web_services_ielectric.Appointments.Services;
using web_services_ielectric.Clients.Domain.Models;
using web_services_ielectric.Clients.Domain.Repositories;
using web_services_ielectric.Clients.Domain.Services.Communication;
using web_services_ielectric.Shared.Domain.Repositories;
using Xunit;

namespace web_services_ielectric.Tests.IntegrationTests;

public class IntegrationTest
{
    [Fact]
    public async Task TestIntegration()
    {
        var applianceRepositoryMock = new Mock<IApplianceRepository>();
        var applianceModelRepositoryMock = new Mock<IApplianceModelRepository>();
        var clientRepositoryMock = new Mock<IClientRepository>();
        var applianceBrandRepositoryMock = new Mock<IApplianceBrandRepository>();
        var appointmentRepositoryMock = new Mock<IAppointmentRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var clientServiceMock = new Mock<IClientService>();
        var clientService = clientServiceMock.Object;
        
        var applianceService = new ApplianceService(applianceRepositoryMock.Object, applianceModelRepositoryMock.Object, clientRepositoryMock.Object, unitOfWorkMock.Object);
        var applianceModelService = new ApplianceModelService(applianceModelRepositoryMock.Object, applianceBrandRepositoryMock.Object, unitOfWorkMock.Object);
        var appointmentService = new AppointmentService(appointmentRepositoryMock.Object, unitOfWorkMock.Object);
        
        var client = new Client
        {
            Id = 1,
            Names = "John",
            LastNames = "Doe",
            CellphoneNumber = 1234567890,
            Address = "123 Main St, City",
            UserId = 1, 
            PlanId = 1,
        };

        var applianceModel = new ApplianceModel
        {
            Id = 1,
            Name = "Ejemplo Modelo",
            Model = "Modelo 123",
            ImgPath = "ruta/imagen.png",
            ApplianceBrandId = 1, 
        };

        var appliance = new Appliance
        {
            Id = 1,
            ClientId = client.Id, 
            ApplianceModelId = applianceModel.Id, 
            PurchaseDate = "2023-11-06",
        };

        
        clientRepositoryMock.Setup(repo => repo.FindByIdAsync(client.Id)).ReturnsAsync(client);
        applianceModelRepositoryMock.Setup(repo => repo.FindByIdAsync(applianceModel.Id)).ReturnsAsync(applianceModel);

        var clientResponse = await clientService.SaveAsync(client);
        var applianceModelResponse = await applianceModelService.SaveAsync(applianceModel);

        Assert.True(clientResponse.Success);
        Assert.True(applianceModelResponse.Success);
        
        appliance.ClientId = client.Id;
        var appointment = new Appointment
        {
            ClientId = client.Id,
            TechnicianId = 1, 
        };
        
        applianceRepositoryMock.Setup(repo => repo.FindByIdAsync(appliance.Id)).ReturnsAsync(appliance);

        var applianceResponse = await applianceService.SaveAsync(appliance);
        var appointmentResponse = await appointmentService.SaveAsync(appointment);

        Assert.True(applianceResponse.Success);
        Assert.True(appointmentResponse.Success);
        
    }
}