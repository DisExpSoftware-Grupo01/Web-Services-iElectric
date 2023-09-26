using Xunit;
using Moq;
using web_services_ielectric.ApplianceModels.Domain.Models;
using web_services_ielectric.ApplianceModels.Domain.Repositories;
using web_services_ielectric.Appliances.Domain.Models;
using web_services_ielectric.Appliances.Domain.Repositories;
using web_services_ielectric.Appliances.Services;
using web_services_ielectric.Clients.Domain.Models;
using web_services_ielectric.Clients.Domain.Repositories;
using web_services_ielectric.Shared.Domain.Repositories;

namespace web_services_ielectric.Tests.UnitTests;

public class ApplianceTests
{
    [Fact]
    public async Task DeleteAsync_ExistingAppliance_DeletesAppliance()
    {
        // Arrange
        var applianceId = 1;
        var existingAppliance = new Appliance { Id = applianceId };
        var applianceRepositoryMock = new Mock<IApplianceRepository>();
        applianceRepositoryMock.Setup(repo => repo.FindByIdAsync(applianceId))
            .ReturnsAsync(existingAppliance);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new ApplianceService(
            applianceRepositoryMock.Object, 
            Mock.Of<IApplianceModelRepository>(),
            Mock.Of<IClientRepository>(),
            unitOfWorkMock.Object);

        // Act
        var result = await service.DeleteAsync(applianceId);

        // Assert
        applianceRepositoryMock.Verify(repo => repo.Remove(existingAppliance), Times.Once);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task DeleteAsync_NonExistingAppliance_ReturnsNotFoundResponse()
    {
        // Arrange
        var applianceId = 1;
        Appliance existingAppliance = null; // Simulate non-existing appliance
        var applianceRepositoryMock = new Mock<IApplianceRepository>();
        applianceRepositoryMock.Setup(repo => repo.FindByIdAsync(applianceId))
            .ReturnsAsync(existingAppliance);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new ApplianceService(
            applianceRepositoryMock.Object, 
            Mock.Of<IApplianceModelRepository>(),
            Mock.Of<IClientRepository>(),
            unitOfWorkMock.Object);

        // Act
        var result = await service.DeleteAsync(applianceId);

        // Assert
        applianceRepositoryMock.Verify(repo => repo.Remove(It.IsAny<Appliance>()), Times.Never);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Never);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetByIdAsync_ExistingAppliance_ReturnsApplianceResponseWithAppliance()
    {
        // Arrange
        var applianceId = 1;
        var existingAppliance = new Appliance { Id = applianceId };
        var applianceRepositoryMock = new Mock<IApplianceRepository>();
        applianceRepositoryMock.Setup(repo => repo.FindByIdAsync(applianceId))
            .ReturnsAsync(existingAppliance);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new ApplianceService(
            applianceRepositoryMock.Object, 
            Mock.Of<IApplianceModelRepository>(),
            Mock.Of<IClientRepository>(),
            unitOfWorkMock.Object);

        // Act
        var result = await service.GetByIdAsync(applianceId);

        // Assert
        Assert.NotNull(result);
    }
    
    [Fact]
        public async Task GetByIdAsync_NonExistingAppliance_ReturnsNotFoundResponse()
        {
            // Arrange
            var applianceId = 1;
            Appliance existingAppliance = null; // Simulate non-existing appliance
            var applianceRepositoryMock = new Mock<IApplianceRepository>();
            applianceRepositoryMock.Setup(repo => repo.FindByIdAsync(applianceId))
                .ReturnsAsync(existingAppliance);
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var service = new ApplianceService(
                applianceRepositoryMock.Object, 
                Mock.Of<IApplianceModelRepository>(),
                Mock.Of<IClientRepository>(),
                unitOfWorkMock.Object);

            // Act
            var result = await service.GetByIdAsync(applianceId);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ListByClientIdAsync_ReturnsListOfAppliances()
        {
            // Arrange
            var clientId = 1;
            var appliances = new List<Appliance>
            {
                new Appliance { Id = 1, ClientId = clientId },
                new Appliance { Id = 2, ClientId = clientId },
            }.AsQueryable();

            var applianceRepositoryMock = new Mock<IApplianceRepository>();
            applianceRepositoryMock.Setup(repo => repo.FindByClientIdAsync(clientId))
                .ReturnsAsync(appliances);
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var service = new ApplianceService(
                applianceRepositoryMock.Object, 
                Mock.Of<IApplianceModelRepository>(),
                Mock.Of<IClientRepository>(),
                unitOfWorkMock.Object);

            // Act
            var result = await service.ListByClientIdAsync(clientId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task SaveAsync_ValidAppliance_SavesAppliance()
        {
            // Arrange
            var appliance = new Appliance { Id = 1, ClientId = 1, ApplianceModelId = 1 };
            var clientRepositoryMock = new Mock<IClientRepository>();
            clientRepositoryMock.Setup(repo => repo.FindByIdAsync(appliance.ClientId))
                .ReturnsAsync(new Client { Id = appliance.ClientId });
            var applianceModelRepositoryMock = new Mock<IApplianceModelRepository>();
            applianceModelRepositoryMock.Setup(repo => repo.FindByIdAsync(appliance.ApplianceModelId))
                .ReturnsAsync(new ApplianceModel { Id = appliance.ApplianceModelId });
            var applianceRepositoryMock = new Mock<IApplianceRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var service = new ApplianceService(
                applianceRepositoryMock.Object, 
                applianceModelRepositoryMock.Object,
                clientRepositoryMock.Object,
                unitOfWorkMock.Object);

            // Act
            var result = await service.SaveAsync(appliance);

            // Assert
            applianceRepositoryMock.Verify(repo => repo.AddAsync(appliance), Times.Once);
            unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
            Assert.NotNull(result);
        }
}