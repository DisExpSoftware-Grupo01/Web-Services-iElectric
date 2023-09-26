using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;
using web_services_ielectric.ApplianceModels.Domain.Models;
using web_services_ielectric.ApplianceModels.Persistence.Repositories;
using web_services_ielectric.Shared.Persistence.Contexts;

namespace web_services_ielectric.Tests.UnitTests;

public class ApplianceModelRepositoryTests
{
    [Fact]
    public async Task ListAsync_ReturnsListOfApplianceModels()
    {
        // Arrange
        var applianceModels = new List<ApplianceModel>
        {
            new ApplianceModel { Id = 1, Name = "Model A" },
            new ApplianceModel { Id = 2, Name = "Model B" },
        }.AsQueryable();

        var contextMock = new Mock<AppDbContext>();
        var applianceModelDbSetMock = new Mock<DbSet<ApplianceModel>>();

        // Configurar el comportamiento del DbSet simulado
        applianceModelDbSetMock.As<IQueryable<ApplianceModel>>().Setup(m => m.Provider).Returns(applianceModels.Provider);
        applianceModelDbSetMock.As<IQueryable<ApplianceModel>>().Setup(m => m.Expression).Returns(applianceModels.Expression);
        applianceModelDbSetMock.As<IQueryable<ApplianceModel>>().Setup(m => m.ElementType).Returns(applianceModels.ElementType);
        applianceModelDbSetMock.As<IQueryable<ApplianceModel>>().Setup(m => m.GetEnumerator()).Returns(applianceModels.GetEnumerator());

        contextMock.Setup(c => c.ApplianceModels).Returns(applianceModelDbSetMock.Object);

        var repository = new ApplianceModelRepository(contextMock.Object);

        // Act
        var result = await repository.ListAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task AddAsync_AddsApplianceModel()
    {
        // Arrange
        var applianceModel = new ApplianceModel { Id = 1, Name = "Model A" };
        var contextMock = new Mock<AppDbContext>();
        var repository = new ApplianceModelRepository(contextMock.Object);

        // Act
        await repository.AddAsync(applianceModel);

        // Assert
        contextMock.Verify(c => c.ApplianceModels.AddAsync(applianceModel, default), Times.Once);
    }

    [Fact]
    public async Task FindByIdAsync_ReturnsApplianceModel()
    {
        // Arrange
        var applianceModel = new ApplianceModel { Id = 1, Name = "Model A" };
        var contextMock = new Mock<AppDbContext>();
        contextMock.Setup(c => c.ApplianceModels.FindAsync(1)).ReturnsAsync(applianceModel);

        var repository = new ApplianceModelRepository(contextMock.Object);

        // Act
        var result = await repository.FindByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(applianceModel, result);
    }
}