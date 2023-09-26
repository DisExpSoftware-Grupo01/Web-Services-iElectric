using Xunit;
using Moq;
using web_services_ielectric.Appointments.Domain.Models;
using web_services_ielectric.Shared.Domain.Repositories;
using web_services_ielectric.SpareRequests.Domain.Models;
using web_services_ielectric.SpareRequests.Domain.Repositories;
using web_services_ielectric.SpareRequests.Services;
using web_services_ielectric.Technicians.Domain.Models;

namespace web_services_ielectric.Tests.UnitTests;

public class SpareRequestServiceTest
{
    [Fact]
    public async Task SaveAsync_SpareRequestsWithSameDetailsButDifferentDates_ReturnsSuccess() {
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var repositoryMock = new Mock<ISpareRequestRepository>();

        var existingSpareRequests = new List<SpareRequest>
        {
            new SpareRequest
            {
                TechnicianId = 1,
                Technician = new Technician { Id = 1 },
                AppointmentId = 1,
                Appointment = new Appointment { Id = 1 },
                Date = "2023-09-25"
            }
        };

        repositoryMock.Setup(r => r.ListAsync()).ReturnsAsync(existingSpareRequests);
        var spareRequestService = new SpareRequestService(repositoryMock.Object, unitOfWorkMock.Object);
        var newSpareRequest = new SpareRequest
        {
            TechnicianId = 1,
            Technician = new Technician { Id = 1 },
            AppointmentId = 2,
            Appointment = new Appointment { Id = 1 },
            Date = "2023-09-26"
        };

        var result = await spareRequestService.SaveAsync(newSpareRequest);

        unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        Assert.True(result.Success);
        Assert.Null(result.Message);
    }
    
    [Fact]
    public async Task DeleteAsync_ExistingSpareRequest_DeletesSpareRequest()
    {
        // Arrange
        var spareRequestId = 1;
        var existingSpareRequest = new SpareRequest { Id = spareRequestId };
        var spareRequestRepositoryMock = new Mock<ISpareRequestRepository>();
        spareRequestRepositoryMock.Setup(repo => repo.FindByIdAsync(spareRequestId))
            .ReturnsAsync(existingSpareRequest);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new SpareRequestService(spareRequestRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await service.DeleteAsync(spareRequestId);

        // Assert
        spareRequestRepositoryMock.Verify(repo => repo.Remove(existingSpareRequest), Times.Once);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task DeleteAsync_NonExistingSpareRequest_ReturnsNotFoundResponse()
    {
        // Arrange
        var spareRequestId = 1;
        SpareRequest existingSpareRequest = null; // Simulate non-existing spare request
        var spareRequestRepositoryMock = new Mock<ISpareRequestRepository>();
        spareRequestRepositoryMock.Setup(repo => repo.FindByIdAsync(spareRequestId))
            .ReturnsAsync(existingSpareRequest);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new SpareRequestService(spareRequestRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await service.DeleteAsync(spareRequestId);

        // Assert
        spareRequestRepositoryMock.Verify(repo => repo.Remove(It.IsAny<SpareRequest>()), Times.Never);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Never);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task ListAsync_ReturnsListOfSpareRequests()
    {
        // Arrange
        var spareRequests = new List<SpareRequest>
        {
            new SpareRequest { Id = 1, Description = "Request A" },
            new SpareRequest { Id = 2, Description = "Request B" },
        }.AsQueryable();

        var spareRequestRepositoryMock = new Mock<ISpareRequestRepository>();
        spareRequestRepositoryMock.Setup(repo => repo.ListAsync())
            .ReturnsAsync(spareRequests);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new SpareRequestService(spareRequestRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await service.ListAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task SaveAsync_AddsSpareRequest()
    {
        // Arrange
        var spareRequest = new SpareRequest { Id = 1, Description = "Request A" };
        var spareRequestRepositoryMock = new Mock<ISpareRequestRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new SpareRequestService(spareRequestRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        await service.SaveAsync(spareRequest);

        // Assert
        spareRequestRepositoryMock.Verify(repo => repo.AddAsync(spareRequest), Times.Once);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ExistingSpareRequest_UpdatesSpareRequest()
    {
        // Arrange
        var spareRequestId = 1;
        var existingSpareRequest = new SpareRequest { Id = spareRequestId };
        var updatedSpareRequest = new SpareRequest { Id = spareRequestId, Description = "Updated Request" };
        var spareRequestRepositoryMock = new Mock<ISpareRequestRepository>();
        spareRequestRepositoryMock.Setup(repo => repo.FindByIdAsync(spareRequestId))
            .ReturnsAsync(existingSpareRequest);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new SpareRequestService(spareRequestRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await service.UpdateAsync(spareRequestId, updatedSpareRequest);

        // Assert
        spareRequestRepositoryMock.Verify(repo => repo.Update(existingSpareRequest), Times.Once);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateAsync_NonExistingSpareRequest_ReturnsNotFoundResponse()
    {
        // Arrange
        var spareRequestId = 1;
        SpareRequest existingSpareRequest = null; // Simulate non-existing spare request
        var updatedSpareRequest = new SpareRequest { Id = spareRequestId, Description = "Updated Request" };
        var spareRequestRepositoryMock = new Mock<ISpareRequestRepository>();
        spareRequestRepositoryMock.Setup(repo => repo.FindByIdAsync(spareRequestId))
            .ReturnsAsync(existingSpareRequest);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new SpareRequestService(spareRequestRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await service.UpdateAsync(spareRequestId, updatedSpareRequest);

        // Assert
        spareRequestRepositoryMock.Verify(repo => repo.Update(It.IsAny<SpareRequest>()), Times.Never);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Never);
        Assert.NotNull(result);
    }
}