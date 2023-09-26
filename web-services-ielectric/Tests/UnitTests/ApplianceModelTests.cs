using Xunit;
using Moq;
using web_services_ielectric.Reports.Domain.Models;
using web_services_ielectric.Reports.Domain.Repositories;
using web_services_ielectric.Reports.Services;
using web_services_ielectric.Shared.Domain.Repositories;

namespace web_services_ielectric.Tests.UnitTests;

public class ApplianceModelTests
{
    [Fact]
    public async Task DeleteAsync_ExistingReport_DeletesReport()
    {
        // Arrange
        var reportId = 1;
        var existingReport = new Report { Id = reportId };
        var reportRepositoryMock = new Mock<IReportRepository>();
        reportRepositoryMock.Setup(repo => repo.FindByIdAsync(reportId))
            .ReturnsAsync(existingReport);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new ReportService(reportRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await service.DeleteAsync(reportId);

        // Assert
        reportRepositoryMock.Verify(repo => repo.Remove(existingReport), Times.Once);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task DeleteAsync_NonExistingReport_ReturnsNotFoundResponse()
    {
        // Arrange
        var reportId = 1;
        Report existingReport = null; // Simulate non-existing report
        var reportRepositoryMock = new Mock<IReportRepository>();
        reportRepositoryMock.Setup(repo => repo.FindByIdAsync(reportId))
            .ReturnsAsync(existingReport);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new ReportService(reportRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await service.DeleteAsync(reportId);

        // Assert
        reportRepositoryMock.Verify(repo => repo.Remove(It.IsAny<Report>()), Times.Never);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Never);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetByIdAsync_ExistingReport_ReturnsReportResponse()
    {
        // Arrange
        var reportId = 1;
        var existingReport = new Report { Id = reportId };
        var reportRepositoryMock = new Mock<IReportRepository>();
        reportRepositoryMock.Setup(repo => repo.FindByIdAsync(reportId))
            .ReturnsAsync(existingReport);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new ReportService(reportRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await service.GetByIdAsync(reportId);

        // Assert
        reportRepositoryMock.Verify(repo => repo.FindByIdAsync(reportId), Times.Once);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Never);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingReport_ReturnsNotFoundResponse()
    {
        // Arrange
        var reportId = 1;
        Report existingReport = null; // Simulate non-existing report
        var reportRepositoryMock = new Mock<IReportRepository>();
        reportRepositoryMock.Setup(repo => repo.FindByIdAsync(reportId))
            .ReturnsAsync(existingReport);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new ReportService(reportRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await service.GetByIdAsync(reportId);

        // Assert
        reportRepositoryMock.Verify(repo => repo.FindByIdAsync(reportId), Times.Once);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Never);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task ListAsync_ReturnsListOfReports()
    {
        // Arrange
        var reports = new List<Report>
        {
            new Report { Id = 1 },
            new Report { Id = 2 },
            new Report { Id = 3 }
        };
        var reportRepositoryMock = new Mock<IReportRepository>();
        reportRepositoryMock.Setup(repo => repo.ListAsync())
            .ReturnsAsync(reports);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new ReportService(reportRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await service.ListAsync();

        // Assert
        reportRepositoryMock.Verify(repo => repo.ListAsync(), Times.Once);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Never);
        Assert.NotNull(result);
        Assert.Equal(reports, result);
    }

    [Fact]
    public async Task SaveAsync_ValidReport_ReturnsReportResponse()
    {
        // Arrange
        var newReport = new Report { Id = 1 };
        var reportRepositoryMock = new Mock<IReportRepository>();
        reportRepositoryMock.Setup(repo => repo.AddAsync(newReport))
            .Returns(Task.CompletedTask);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new ReportService(reportRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await service.SaveAsync(newReport);

        // Assert
        reportRepositoryMock.Verify(repo => repo.AddAsync(newReport), Times.Once);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task SaveAsync_InvalidReport_ReturnsErrorResponse()
    {
        // Arrange
        var invalidReport = new Report { Id = 1 }; // Simulate invalid report
        var reportRepositoryMock = new Mock<IReportRepository>();
        reportRepositoryMock.Setup(repo => repo.AddAsync(invalidReport))
            .ThrowsAsync(new Exception("Invalid report."));
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new ReportService(reportRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await service.SaveAsync(invalidReport);

        // Assert
        reportRepositoryMock.Verify(repo => repo.AddAsync(invalidReport), Times.Once);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Never);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateAsync_ExistingReport_UpdatesReport()
    {
        // Arrange
        var reportId = 1;
        var existingReport = new Report { Id = reportId };
        var updatedReport = new Report { Id = reportId };
        var reportRepositoryMock = new Mock<IReportRepository>();
        reportRepositoryMock.Setup(repo => repo.FindByIdAsync(reportId))
            .ReturnsAsync(existingReport);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new ReportService(reportRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await service.UpdateAsync(reportId, updatedReport);

        // Assert
        reportRepositoryMock.Verify(repo => repo.Update(existingReport), Times.Once);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
        Assert.NotNull(result);
    }

}