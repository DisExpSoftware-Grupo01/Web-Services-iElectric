using Xunit;
using Moq;
using web_services_ielectric.ApplianceBrands.Domain.Models;
using web_services_ielectric.ApplianceBrands.Domain.Repositories;
using web_services_ielectric.ApplianceBrands.Services;
using web_services_ielectric.Shared.Domain.Repositories;

namespace web_services_ielectric.Tests.UnitTests;

public class ApplianceBrandsTests
{
    [Fact]
    public async Task ListAsync_ReturnsListOfApplianceBrands()
    {
        // Arrange
        var applianceBrands = new List<ApplianceBrand>
        {
            new ApplianceBrand { Id = 1, Name = "Brand A" },
            new ApplianceBrand { Id = 2, Name = "Brand B" },
        }.AsQueryable();

        var applianceBrandRepositoryMock = new Mock<IApplianceBrandRepository>();
        applianceBrandRepositoryMock.Setup(repo => repo.ListAsync())
            .ReturnsAsync(applianceBrands);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new ApplianceBrandService(applianceBrandRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await service.ListAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_ExistingApplianceBrand_ReturnsApplianceBrandResponse()
    {
        // Arrange
        var applianceBrandId = 1;
        var existingApplianceBrand = new ApplianceBrand { Id = applianceBrandId };
        var applianceBrandRepositoryMock = new Mock<IApplianceBrandRepository>();
        applianceBrandRepositoryMock.Setup(repo => repo.FindByIdAsync(applianceBrandId))
            .ReturnsAsync(existingApplianceBrand);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new ApplianceBrandService(applianceBrandRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await service.GetByIdAsync(applianceBrandId);

        // Assert
        applianceBrandRepositoryMock.Verify(repo => repo.FindByIdAsync(applianceBrandId), Times.Once);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingApplianceBrand_ReturnsNotFoundResponse()
    {
        // Arrange
        var applianceBrandId = 1;
        ApplianceBrand existingApplianceBrand = null; // Simulate non-existing appliance brand
        var applianceBrandRepositoryMock = new Mock<IApplianceBrandRepository>();
        applianceBrandRepositoryMock.Setup(repo => repo.FindByIdAsync(applianceBrandId))
            .ReturnsAsync(existingApplianceBrand);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new ApplianceBrandService(applianceBrandRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await service.GetByIdAsync(applianceBrandId);

        // Assert
        applianceBrandRepositoryMock.Verify(repo => repo.FindByIdAsync(applianceBrandId), Times.Once);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task SaveAsync_AddsApplianceBrand()
    {
        // Arrange
        var applianceBrand = new ApplianceBrand { Id = 1, Name = "Brand A" };
        var applianceBrandRepositoryMock = new Mock<IApplianceBrandRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new ApplianceBrandService(applianceBrandRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        await service.SaveAsync(applianceBrand);

        // Assert
        applianceBrandRepositoryMock.Verify(repo => repo.AddAsync(applianceBrand), Times.Once);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ExistingApplianceBrand_UpdatesApplianceBrand()
    {
        // Arrange
        var applianceBrandId = 1;
        var existingApplianceBrand = new ApplianceBrand { Id = applianceBrandId };
        var updatedApplianceBrand = new ApplianceBrand { Id = applianceBrandId, Name = "Updated Brand" };
        var applianceBrandRepositoryMock = new Mock<IApplianceBrandRepository>();
        applianceBrandRepositoryMock.Setup(repo => repo.FindByIdAsync(applianceBrandId))
            .ReturnsAsync(existingApplianceBrand);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new ApplianceBrandService(applianceBrandRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await service.UpdateAsync(applianceBrandId, updatedApplianceBrand);

        // Assert
        applianceBrandRepositoryMock.Verify(repo => repo.Update(existingApplianceBrand), Times.Once);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateAsync_NonExistingApplianceBrand_ReturnsNotFoundResponse()
    {
        // Arrange
        var applianceBrandId = 1;
        ApplianceBrand existingApplianceBrand = null; // Simulate non-existing appliance brand
        var updatedApplianceBrand = new ApplianceBrand { Id = applianceBrandId, Name = "Updated Brand" };
        var applianceBrandRepositoryMock = new Mock<IApplianceBrandRepository>();
        applianceBrandRepositoryMock.Setup(repo => repo.FindByIdAsync(applianceBrandId))
            .ReturnsAsync(existingApplianceBrand);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new ApplianceBrandService(applianceBrandRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await service.UpdateAsync(applianceBrandId, updatedApplianceBrand);

        // Assert
        applianceBrandRepositoryMock.Verify(repo => repo.Update(It.IsAny<ApplianceBrand>()), Times.Never);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Never);
        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task DeleteAsync_ExistingApplianceBrand_DeletesApplianceBrand()
    {
        // Arrange
        var applianceBrandId = 1;
        var existingApplianceBrand = new ApplianceBrand { Id = applianceBrandId };
        var applianceBrandRepositoryMock = new Mock<IApplianceBrandRepository>();
        applianceBrandRepositoryMock.Setup(repo => repo.FindByIdAsync(applianceBrandId))
            .ReturnsAsync(existingApplianceBrand);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new ApplianceBrandService(applianceBrandRepositoryMock.Object, unitOfWorkMock.Object);
    
        // Act
        var result = await service.DeleteAsync(applianceBrandId);
    
        // Assert
        applianceBrandRepositoryMock.Verify(repo => repo.Remove(existingApplianceBrand), Times.Once);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task DeleteAsync_NonExistingApplianceBrand_ReturnsNotFoundResponse()
    {
        // Arrange
        var applianceBrandId = 1;
        ApplianceBrand existingApplianceBrand = null; // Simulate non-existing appliance brand
        var applianceBrandRepositoryMock = new Mock<IApplianceBrandRepository>();
        applianceBrandRepositoryMock.Setup(repo => repo.FindByIdAsync(applianceBrandId))
            .ReturnsAsync(existingApplianceBrand);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new ApplianceBrandService(applianceBrandRepositoryMock.Object, unitOfWorkMock.Object);
    
        // Act
        var result = await service.DeleteAsync(applianceBrandId);
    
        // Assert
        applianceBrandRepositoryMock.Verify(repo => repo.Remove(It.IsAny<ApplianceBrand>()), Times.Never);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Never);
        Assert.NotNull(result);
    }

}