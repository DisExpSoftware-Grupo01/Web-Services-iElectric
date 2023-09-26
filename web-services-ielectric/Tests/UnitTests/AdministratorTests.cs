using Moq;
using web_services_ielectric.Administrators.Domain.Models;
using web_services_ielectric.Administrators.Domain.Repositories;
using web_services_ielectric.Administrators.Services;
using web_services_ielectric.Shared.Domain.Repositories;
using Xunit;
namespace web_services_ielectric.Tests.UnitTests;

public class AdministratorTests
{
    [Fact]
    public async Task DeleteAsync_ExistingAdministrator_DeletesAdministrator()
    {
        // Arrange
        var administratorId = 1;
        var existingAdministrator = new Administrator { Id = administratorId };
        var administratorRepositoryMock = new Mock<IAdministratorRepository>();
        administratorRepositoryMock.Setup(repo => repo.FindByIdAsync(administratorId))
            .ReturnsAsync(existingAdministrator);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new AdministratorService(administratorRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await service.DeleteAsync(administratorId);

        // Assert
        administratorRepositoryMock.Verify(repo => repo.Remove(existingAdministrator), Times.Once);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task DeleteAsync_NonExistingAdministrator_ReturnsNotFoundResponse()
    {
        // Arrange
        var administratorId = 1;
        Administrator existingAdministrator = null; // Simulate non-existing administrator
        var administratorRepositoryMock = new Mock<IAdministratorRepository>();
        administratorRepositoryMock.Setup(repo => repo.FindByIdAsync(administratorId))
            .ReturnsAsync(existingAdministrator);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new AdministratorService(administratorRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await service.DeleteAsync(administratorId);

        // Assert
        administratorRepositoryMock.Verify(repo => repo.Remove(It.IsAny<Administrator>()), Times.Never);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Never);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetByIdAsync_ExistingAdministrator_ReturnsAdministrator()
    {
        // Arrange
        var administratorId = 1;
        var existingAdministrator = new Administrator { Id = administratorId };
        var administratorRepositoryMock = new Mock<IAdministratorRepository>();
        administratorRepositoryMock.Setup(repo => repo.FindByIdAsync(administratorId))
            .ReturnsAsync(existingAdministrator);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new AdministratorService(administratorRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await service.GetByIdAsync(administratorId);

        // Assert
        administratorRepositoryMock.Verify(repo => repo.FindByIdAsync(administratorId), Times.Once);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingAdministrator_ReturnsNotFoundResponse()
    {
        // Arrange
        var administratorId = 1;
        Administrator existingAdministrator = null; // Simulate non-existing administrator
        var administratorRepositoryMock = new Mock<IAdministratorRepository>();
        administratorRepositoryMock.Setup(repo => repo.FindByIdAsync(administratorId))
            .ReturnsAsync(existingAdministrator);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new AdministratorService(administratorRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await service.GetByIdAsync(administratorId);

        // Assert
        administratorRepositoryMock.Verify(repo => repo.FindByIdAsync(administratorId), Times.Once);
        Assert.NotNull(result);
    } 
    
    [Fact]
    public async Task ListAsync_ReturnsListOfAdministrators()
    {
        // Arrange
        var administrators = new List<Administrator>
        {
            new Administrator { Id = 1, Names = "Admin A" },
            new Administrator { Id = 2, Names = "Admin B" },
        }.AsQueryable();
    
        var administratorRepositoryMock = new Mock<IAdministratorRepository>();
        administratorRepositoryMock.Setup(repo => repo.ListAsync())
            .ReturnsAsync(administrators);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new AdministratorService(administratorRepositoryMock.Object, unitOfWorkMock.Object);
    
        // Act
        var result = await service.ListAsync();
    
        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }
    
    [Fact]
    public async Task SaveAsync_AddsAdministrator()
    {
        // Arrange
        var administrator = new Administrator { Id = 1, Names = "Admin A" };
        var administratorRepositoryMock = new Mock<IAdministratorRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new AdministratorService(administratorRepositoryMock.Object, unitOfWorkMock.Object);
    
        // Act
        await service.SaveAsync(administrator);
    
        // Assert
        administratorRepositoryMock.Verify(repo => repo.AddAsync(administrator), Times.Once);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
    }
    
    [Fact]
    public async Task UpdateAsync_ExistingAdministrator_UpdatesAdministrator()
    {
        // Arrange
        var administratorId = 1;
        var existingAdministrator = new Administrator { Id = administratorId };
        var updatedAdministrator = new Administrator { Id = administratorId, Names = "Updated Admin" };
        var administratorRepositoryMock = new Mock<IAdministratorRepository>();
        administratorRepositoryMock.Setup(repo => repo.FindByIdAsync(administratorId))
            .ReturnsAsync(existingAdministrator);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new AdministratorService(administratorRepositoryMock.Object, unitOfWorkMock.Object);
    
        // Act
        var result = await service.UpdateAsync(administratorId, updatedAdministrator);
    
        // Assert
        administratorRepositoryMock.Verify(repo => repo.Update(existingAdministrator), Times.Once);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task UpdateAsync_NonExistingAdministrator_ReturnsNotFoundResponse()
    {
        var administratorId = 1;
        Administrator existingAdministrator = null;
        var updatedAdministrator = new Administrator { Id = administratorId, Names = "Updated Admin" };
        var administratorRepositoryMock = new Mock<IAdministratorRepository>();
        administratorRepositoryMock.Setup(repo => repo.FindByIdAsync(administratorId))
            .ReturnsAsync(existingAdministrator);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new AdministratorService(administratorRepositoryMock.Object, unitOfWorkMock.Object);
        
        var result = await service.UpdateAsync(administratorId, updatedAdministrator);
    
        administratorRepositoryMock.Verify(repo => repo.Update(It.IsAny<Administrator>()), Times.Never);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Never);
        Assert.NotNull(result);
    }


}