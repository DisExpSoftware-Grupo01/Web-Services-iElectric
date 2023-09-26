using Xunit;
using Moq;
using web_services_ielectric.Shared.Domain.Repositories;
using web_services_ielectric.Technicians.Domain.Models;
using web_services_ielectric.Technicians.Domain.Repositories;
using web_services_ielectric.Technicians.Services;

namespace web_services_ielectric.Tests.UnitTests;

public class TechniciansServiceTests
{
    [Fact]
    public async Task GetByIdAsync_ExistingTechnician_ReturnsTechnicianResponseWithTechnician()
    {
        // Arrange
        var technicianId = 1;
        var existingTechnician = new Technician { Id = technicianId, Names = "John", LastNames = "Doe" };
        var technicianRepositoryMock = new Mock<ITechnicianRepository>();
        technicianRepositoryMock.Setup(repo => repo.FindByIdAsync(technicianId))
            .ReturnsAsync(existingTechnician);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new TechnicianService(technicianRepositoryMock.Object, unitOfWorkMock.Object, null);

        // Act
        var result = await service.GetByIdAsync(technicianId);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingTechnician_ReturnsTechnicianResponseWithErrorMessage()
    {
        // Arrange
        var technicianId = 1;
        Technician existingTechnician = null; // Simulate non-existing technician
        var technicianRepositoryMock = new Mock<ITechnicianRepository>();
        technicianRepositoryMock.Setup(repo => repo.FindByIdAsync(technicianId))
            .ReturnsAsync(existingTechnician);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new TechnicianService(technicianRepositoryMock.Object, unitOfWorkMock.Object, null);

        // Act
        var result = await service.GetByIdAsync(technicianId);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task SaveAsync_ValidTechnician_ReturnsTechnicianResponseWithTechnician()
    {
        // Arrange
        var newTechnician = new Technician { Names = "Alice", LastNames = "Johnson" };
        var technicianRepositoryMock = new Mock<ITechnicianRepository>();
        technicianRepositoryMock.Setup(repo => repo.AddAsync(newTechnician));
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new TechnicianService(technicianRepositoryMock.Object, unitOfWorkMock.Object, null);

        // Act
        var result = await service.SaveAsync(newTechnician);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateAsync_ExistingTechnician_ReturnsTechnicianResponseWithUpdatedTechnician()
    {
        // Arrange
        var technicianId = 1;
        var existingTechnician = new Technician { Id = technicianId, Names = "John", LastNames = "Doe" };
        var updatedTechnician = new Technician { Id = technicianId, Names = "Jane", LastNames = "Smith" };
        var technicianRepositoryMock = new Mock<ITechnicianRepository>();
        technicianRepositoryMock.Setup(repo => repo.FindByIdAsync(technicianId))
            .ReturnsAsync(existingTechnician);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new TechnicianService(technicianRepositoryMock.Object, unitOfWorkMock.Object, null);

        // Act
        var result = await service.UpdateAsync(technicianId, updatedTechnician);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateAsync_NonExistingTechnician_ReturnsTechnicianResponseWithErrorMessage()
    {
        // Arrange
        var technicianId = 1;
        var updatedTechnician = new Technician { Id = technicianId, Names = "Jane", LastNames = "Smith" };
        Technician existingTechnician = null; // Simulate non-existing technician
        var technicianRepositoryMock = new Mock<ITechnicianRepository>();
        technicianRepositoryMock.Setup(repo => repo.FindByIdAsync(technicianId))
            .ReturnsAsync(existingTechnician);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new TechnicianService(technicianRepositoryMock.Object, unitOfWorkMock.Object, null);

        // Act
        var result = await service.UpdateAsync(technicianId, updatedTechnician);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task ListAsync_ReturnsListOfTechnicians()
    {
        // Arrange
        var technicians = new List<Technician>
        {
            new Technician { Id = 1, Names = "John", LastNames = "Doe" },
            new Technician { Id = 2, Names = "Jane", LastNames = "Smith" }
        };
        var technicianRepositoryMock = new Mock<ITechnicianRepository>();
        technicianRepositoryMock.Setup(repo => repo.ListAsync())
            .ReturnsAsync(technicians);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new TechnicianService(technicianRepositoryMock.Object, unitOfWorkMock.Object, null);

        // Act
        var result = await service.ListAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }
}