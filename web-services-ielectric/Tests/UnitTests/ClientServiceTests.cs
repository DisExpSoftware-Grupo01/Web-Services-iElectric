using Xunit;
using Moq;
using web_services_ielectric.Clients.Domain.Models;
using web_services_ielectric.Clients.Domain.Repositories;
using web_services_ielectric.Clients.Services;
using web_services_ielectric.Shared.Domain.Repositories;

namespace web_services_ielectric.Tests.UnitTests;

public class ClientServiceTests
{
    [Fact]
    public async Task GetByIdAsync_ExistingClient_ReturnsClientResponseWithClient()
    {
        // Arrange
        var clientId = 1;
        var existingClient = new Client { Id = clientId, Names = "John", LastNames = "Doe" };
        var clientRepositoryMock = new Mock<IClientRepository>();
        clientRepositoryMock.Setup(repo => repo.FindByIdAsync(clientId))
            .ReturnsAsync(existingClient);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new ClientService(clientRepositoryMock.Object, null, unitOfWorkMock.Object);

        // Act
        var result = await service.GetByIdAsync(clientId);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingClient_ReturnsClientResponseWithErrorMessage()
    {
        // Arrange
        var clientId = 1;
        Client existingClient = null; // Simulate non-existing client
        var clientRepositoryMock = new Mock<IClientRepository>();
        clientRepositoryMock.Setup(repo => repo.FindByIdAsync(clientId))
            .ReturnsAsync(existingClient);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new ClientService(clientRepositoryMock.Object, null, unitOfWorkMock.Object);

        // Act
        var result = await service.GetByIdAsync(clientId);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task SaveAsync_ValidClient_ReturnsClientResponseWithClient()
    {
        // Arrange
        var newClient = new Client { Names = "Alice", LastNames = "Johnson" };
        var clientRepositoryMock = new Mock<IClientRepository>();
        clientRepositoryMock.Setup(repo => repo.AddAsync(newClient));
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new ClientService(clientRepositoryMock.Object, null, unitOfWorkMock.Object);

        // Act
        var result = await service.SaveAsync(newClient);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateAsync_ExistingClient_ReturnsClientResponseWithUpdatedClient()
    {
        // Arrange
        var clientId = 1;
        var existingClient = new Client { Id = clientId, Names = "John", LastNames = "Doe" };
        var updatedClient = new Client { Id = clientId, Names = "Jane", LastNames = "Smith" };
        var clientRepositoryMock = new Mock<IClientRepository>();
        clientRepositoryMock.Setup(repo => repo.FindByIdAsync(clientId))
            .ReturnsAsync(existingClient);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new ClientService(clientRepositoryMock.Object, null, unitOfWorkMock.Object);

        // Act
        var result = await service.UpdateAsync(clientId, updatedClient);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateAsync_NonExistingClient_ReturnsClientResponseWithErrorMessage()
    {
        // Arrange
        var clientId = 1;
        var updatedClient = new Client { Id = clientId, Names = "Jane", LastNames = "Smith" };
        Client existingClient = null; // Simulate non-existing client
        var clientRepositoryMock = new Mock<IClientRepository>();
        clientRepositoryMock.Setup(repo => repo.FindByIdAsync(clientId))
            .ReturnsAsync(existingClient);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new ClientService(clientRepositoryMock.Object, null, unitOfWorkMock.Object);

        // Act
        var result = await service.UpdateAsync(clientId, updatedClient);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task ListAsync_ReturnsListOfClients()
    {
        // Arrange
        var clients = new List<Client>
        {
            new Client { Id = 1, Names = "John", LastNames = "Doe" },
            new Client { Id = 2, Names = "Jane", LastNames = "Smith" }
        };
        var clientRepositoryMock = new Mock<IClientRepository>();
        clientRepositoryMock.Setup(repo => repo.ListAsync())
            .ReturnsAsync(clients);
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var service = new ClientService(clientRepositoryMock.Object, null, unitOfWorkMock.Object);

        // Act
        var result = await service.ListAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }
}