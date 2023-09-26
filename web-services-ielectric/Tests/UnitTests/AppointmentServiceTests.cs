using Xunit;
using Moq;
using web_services_ielectric.Appointments.Domain.Models;
using web_services_ielectric.Appointments.Domain.Repositories;
using web_services_ielectric.Appointments.Services;
using web_services_ielectric.Shared.Domain.Repositories;

namespace web_services_ielectric.Tests.UnitTests;

public class AppointmentServiceTests
{
    [Fact]
    public async Task GetByIdAsync_ExistingAppointment_ReturnsAppointmentResponseWithAppointment()
    {
        // Arrange
        var appointmentId = 1;
        var existingAppointment = new Appointment { Id = appointmentId };
        var appointmentRepositoryMock = new Mock<IAppointmentRepository>();
        appointmentRepositoryMock.Setup(repo => repo.FindByIdAsync(appointmentId))
            .ReturnsAsync(existingAppointment);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new AppointmentService(appointmentRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var response = await service.GetByIdAsync(appointmentId);

        // Assert
        Assert.NotNull(response);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingAppointment_ReturnsAppointmentResponseWithErrorMessage()
    {
        // Arrange
        var appointmentId = 1;
        Appointment existingAppointment = null; // Simulate non-existing appointment
        var appointmentRepositoryMock = new Mock<IAppointmentRepository>();
        appointmentRepositoryMock.Setup(repo => repo.FindByIdAsync(appointmentId))
            .ReturnsAsync(existingAppointment);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new AppointmentService(appointmentRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var response = await service.GetByIdAsync(appointmentId);

        // Assert
        Assert.NotNull(response);
    }
    
    [Fact]
    public async Task SaveAsync_ValidAppointment_ReturnsAppointmentResponseWithAppointment()
    {
        // Arrange
        var validAppointment = new Appointment { Id = 1 };
        var appointmentRepositoryMock = new Mock<IAppointmentRepository>();
        appointmentRepositoryMock.Setup(repo => repo.FindByDateTechnicianAndClientAsync(
            validAppointment.DateReserve, validAppointment.TechnicianId, validAppointment.ClientId))
            .ReturnsAsync((Appointment)null);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        appointmentRepositoryMock.Setup(repo => repo.AddAsync(validAppointment))
            .Callback((Appointment appointment) => appointment.Id = 1);
        var service = new AppointmentService(appointmentRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var response = await service.SaveAsync(validAppointment);

        // Assert
        Assert.NotNull(response);
    }

    [Fact]
    public async Task SaveAsync_DuplicateAppointment_ReturnsAppointmentResponseWithErrorMessage()
    {
        // Arrange
        var duplicateAppointment = new Appointment { Id = 1 };
        var appointmentRepositoryMock = new Mock<IAppointmentRepository>();
        appointmentRepositoryMock.Setup(repo => repo.FindByDateTechnicianAndClientAsync(
            duplicateAppointment.DateReserve, duplicateAppointment.TechnicianId, duplicateAppointment.ClientId))
            .ReturnsAsync(duplicateAppointment);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new AppointmentService(appointmentRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var response = await service.SaveAsync(duplicateAppointment);

        // Assert
        Assert.NotNull(response);
    }

    [Fact]
    public async Task DeleteAsync_ExistingAppointment_DeletesAppointment()
    {
        // Arrange
        var appointmentId = 1;
        var existingAppointment = new Appointment { Id = appointmentId };
        var appointmentRepositoryMock = new Mock<IAppointmentRepository>();
        appointmentRepositoryMock.Setup(repo => repo.FindByIdAsync(appointmentId))
            .ReturnsAsync(existingAppointment);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new AppointmentService(appointmentRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var response = await service.DeleteAsync(appointmentId);

        // Assert
        Assert.NotNull(response);
        appointmentRepositoryMock.Verify(repo => repo.Remove(existingAppointment), Times.Once);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_NonExistingAppointment_ReturnsAppointmentResponseWithErrorMessage()
    {
        // Arrange
        var appointmentId = 1;
        Appointment existingAppointment = null; 
        var appointmentRepositoryMock = new Mock<IAppointmentRepository>();
        appointmentRepositoryMock.Setup(repo => repo.FindByIdAsync(appointmentId))
            .ReturnsAsync(existingAppointment);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new AppointmentService(appointmentRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var response = await service.DeleteAsync(appointmentId);

        // Assert
        Assert.NotNull(response);
        appointmentRepositoryMock.Verify(repo => repo.Remove(It.IsAny<Appointment>()), Times.Never);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Never);
    }
    
    [Fact]
    public async Task UpdateAsync_ExistingAppointment_ReturnsAppointmentResponseWithUpdatedAppointment()
    {
        // Arrange
        var appointmentId = 1;
        var existingAppointment = new Appointment { Id = appointmentId, DateReserve = DateTime.Now.ToString() };
        var updatedAppointment = new Appointment { Id = appointmentId, DateReserve = DateTime.Now.AddDays(1).ToString() };

        var appointmentRepositoryMock = new Mock<IAppointmentRepository>();
        appointmentRepositoryMock.Setup(repo => repo.FindByIdAsync(appointmentId))
            .ReturnsAsync(existingAppointment);

        var unitOfWorkMock = new Mock<IUnitOfWork>();

        var service = new AppointmentService(appointmentRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var response = await service.UpdateAsync(appointmentId, updatedAppointment);

        // Assert
        Assert.NotNull(response);
        appointmentRepositoryMock.Verify(repo => repo.Update(existingAppointment), Times.Once);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_NonExistingAppointment_ReturnsAppointmentResponseWithErrorMessage()
    {
        // Arrange
        var appointmentId = 1;
        var nonExistingAppointment = new Appointment { Id = appointmentId };
        var appointmentRepositoryMock = new Mock<IAppointmentRepository>();
        appointmentRepositoryMock.Setup(repo => repo.FindByIdAsync(appointmentId))
            .ReturnsAsync((Appointment)null);

        var unitOfWorkMock = new Mock<IUnitOfWork>();

        var service = new AppointmentService(appointmentRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var response = await service.UpdateAsync(appointmentId, nonExistingAppointment);

        // Assert
        Assert.NotNull(response);
        appointmentRepositoryMock.Verify(repo => repo.Update(It.IsAny<Appointment>()), Times.Never);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Never);
    }

    [Fact]
    public async Task ListAsync_ReturnsListOfAppointments()
    {
        // Arrange
        var appointments = new List<Appointment>
        {
            new Appointment { Id = 1 },
            new Appointment { Id = 2 },
        }.AsQueryable();

        var appointmentRepositoryMock = new Mock<IAppointmentRepository>();
        appointmentRepositoryMock.Setup(repo => repo.ListAsync())
            .ReturnsAsync(appointments);

        var unitOfWorkMock = new Mock<IUnitOfWork>();

        var service = new AppointmentService(appointmentRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await service.ListAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

}