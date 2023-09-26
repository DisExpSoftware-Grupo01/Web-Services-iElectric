using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using web_services_ielectric.ApplianceBrands.Domain.Models;
using web_services_ielectric.ApplianceBrands.Domain.Repositories;
using web_services_ielectric.ApplianceBrands.Services;
using web_services_ielectric.ApplianceModels.Domain.Models;
using web_services_ielectric.Appointments.Domain.Models;
using web_services_ielectric.Appointments.Domain.Repositories;
using web_services_ielectric.Appointments.Services;
using web_services_ielectric.Shared.Domain.Repositories;
using Xunit;
namespace web_services_ielectric.Tests.UnitTests;

public class ApplianceModelAppointmentTest
{
    [Fact]
    public async Task SaveAsync_ValidBrand_ShouldReturnSuccessResponse()
    {
        var mockBrandRepository = new Mock<IApplianceBrandRepository>();
        mockBrandRepository.Setup(repo => repo.AddAsync(It.IsAny<ApplianceBrand>()))
            .Returns(Task.CompletedTask);

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(uow => uow.CompleteAsync())
            .Returns(Task.CompletedTask); 

        var applianceBrandService = new ApplianceBrandService(
            mockBrandRepository.Object,
            mockUnitOfWork.Object
        );

        var brandToSave = new ApplianceBrand
        {
            Name = "Marca Ejemplo",
            ImgPath = "/images/marca-ejemplo.png",
            ApplianceModels = new List<ApplianceModel>
            {
                new ApplianceModel
                {
                    Name = "Modelo 1",
                    Model = "12345",
                    ImgPath = "/images/modelo-1.png",
                    ApplianceBrandId = 1
                },
                new ApplianceModel
                {
                    Name = "Modelo 2",
                    Model = "67890",
                    ImgPath = "/images/modelo-2.png",
                    ApplianceBrandId = 1
                }
            }
        };
        
        
        
        var response = await applianceBrandService.SaveAsync(brandToSave);
        
        Assert.True(response.Success);
        Assert.Null(response.Message);
    }
    
    [Fact]
    public async Task SaveAsync_DuplicateAppointment_ReturnsErrorResponse()
    {
        var dateReserve = "2023-09-25";
        long technicianId = 1;
        long clientId = 2;

        var mockAppointmentRepository = new Mock<IAppointmentRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        var existingAppointment = new Appointment
        {
            DateReserve = dateReserve,
            TechnicianId = technicianId,
            ClientId = clientId
        };

        mockAppointmentRepository.Setup(r =>
                r.FindByDateTechnicianAndClientAsync(dateReserve, technicianId, clientId))
            .ReturnsAsync(existingAppointment);

        var appointmentService = new AppointmentService(mockAppointmentRepository.Object, mockUnitOfWork.Object);

        // Act
        var appointmentToSave = new Appointment
        {
            DateReserve = dateReserve,
            TechnicianId = technicianId,
            ClientId = clientId
        };

        var result = await appointmentService.SaveAsync(appointmentToSave);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("An Appointment with the same date, TechnicianId, and ClientId already exists.", result.Message);
    }
}