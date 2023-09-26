using Xunit;
using Moq;
using web_services_ielectric.Announcements.Domain.Models;
using web_services_ielectric.Announcements.Domain.Repositories;
using web_services_ielectric.Announcements.Services;
using web_services_ielectric.Shared.Domain.Repositories;

namespace web_services_ielectric.Tests.UnitTests;

public class AnnouncementsServiceTests
{
    [Fact]
    public async Task DeleteAsync_ExistingAnnouncement_DeletesAnnouncement()
    {
        // Arrange
        var announcementId = 1;
        var existingAnnouncement = new Announcement { Id = announcementId };
        var announcementRepositoryMock = new Mock<IAnnouncementRepository>();
        announcementRepositoryMock.Setup(repo => repo.FindByIdAsync(announcementId))
            .ReturnsAsync(existingAnnouncement);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new AnnouncementService(announcementRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await service.DeleteAsync(announcementId);

        // Assert
        Assert.NotNull(result);
        announcementRepositoryMock.Verify(repo => repo.Remove(existingAnnouncement), Times.Once);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_NonExistingAnnouncement_ReturnsNotFoundResponse()
    {
        // Arrange
        var announcementId = 1;
        Announcement existingAnnouncement = null; // Simulate non-existing announcement
        var announcementRepositoryMock = new Mock<IAnnouncementRepository>();
        announcementRepositoryMock.Setup(repo => repo.FindByIdAsync(announcementId))
            .ReturnsAsync(existingAnnouncement);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new AnnouncementService(announcementRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await service.DeleteAsync(announcementId);

        // Assert
        Assert.NotNull(result);
        announcementRepositoryMock.Verify(repo => repo.Remove(It.IsAny<Announcement>()), Times.Never);
        unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Never);
    }
    
}