using web_services_ielectric.Clients.Domain.Models;
using web_services_ielectric.Clients.Domain.Repositories;
using web_services_ielectric.Clients.Domain.Services.Communication;
using web_services_ielectric.Clients.Domain.Services.Communication.Communication;
using web_services_ielectric.Plans.Domain.Repositories;
using web_services_ielectric.Shared.Domain.Repositories;

namespace web_services_ielectric.Clients.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IPlanRepository _planRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ClientService(IClientRepository clientRepository, IPlanRepository planRepository, IUnitOfWork unitOfWork)
    {
        _clientRepository = clientRepository;
        _planRepository = planRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ClientResponse> DeleteAsync(long id)
    {
        var existingClient = await _clientRepository.FindByIdAsync(id);

        if (existingClient == null)
            return new ClientResponse("Client not found.");

        try
        {
            _clientRepository.Remove(existingClient);
            await _unitOfWork.CompleteAsync();

            return new ClientResponse(existingClient);
        }
        catch (Exception e)
        {
            return new ClientResponse($"An error occurred while deleting the client: {e.Message}");
        }
    }

    public async Task<ClientResponse> GetByIdAsync(long id)
    {
        var existingClient = await _clientRepository.FindByIdAsync(id);

        if (existingClient == null)
            return new ClientResponse("Client not found.");

        return new ClientResponse(existingClient);
    }

    public async Task<ClientResponse> GetByUserIdAsync(long userId)
    {
        var existingClient = await _clientRepository.FindByUserIdAsync(userId);

        if (existingClient == null)
            return new ClientResponse("Client not found.");

        return new ClientResponse(existingClient);
    } 

    public async Task<IEnumerable<Client>> ListAsync()
    {
        return await _clientRepository.ListAsync();
    }

    public async Task<ClientResponse> SaveAsync(Client client)
    {
        try
        {
            await _clientRepository.AddAsync(client);
            await _unitOfWork.CompleteAsync();

            return new ClientResponse(client);
        }
        catch (Exception e)
        {
            return new ClientResponse($"An error occurred while saving the client: {e.Message}");
        }
    }

    public async Task<ClientResponse> UpdateAsync(long id, Client client)
    {
        var existingClient = await _clientRepository.FindByIdAsync(id);

        if (existingClient == null)
            return new ClientResponse("Client not found.");

        existingClient.Names = client.Names;
        existingClient.LastNames = client.LastNames;
        existingClient.CellphoneNumber = client.CellphoneNumber;
        existingClient.Address = client.Address;
        existingClient.UserId = client.UserId;

        try
        {
            _clientRepository.Update(existingClient);
            await _unitOfWork.CompleteAsync();

            return new ClientResponse(existingClient);
        }
        catch (Exception e)
        {
            return new ClientResponse($"An error occurred while updating the client: {e.Message}");
        }
    }

    public async Task<ClientResponse> UpdateUserPlanAsync(long clientId, long planId)
    {
        var existingPlan = await _planRepository.FindById(planId);

        if (existingPlan == null && planId != 0)
            return new ClientResponse("Plan not found");

        var existingClient = await _clientRepository.FindByIdAsync(clientId);

        if (existingClient == null)
            return new ClientResponse("Client not found");

        existingClient.PlanId = planId;

        try
        {
            _clientRepository.Update(existingClient);
            await _unitOfWork.CompleteAsync();

            return new ClientResponse(existingClient);
        }
        catch (Exception ex)
        {
            return new ClientResponse($"An error ocurred while assigning User to Plan {ex.Message}");
        }
    }
}