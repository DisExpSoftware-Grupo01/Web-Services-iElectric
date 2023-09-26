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
}