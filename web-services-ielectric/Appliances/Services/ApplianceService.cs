using web_services_ielectric.ApplianceModels.Domain.Repositories;
using web_services_ielectric.Appliances.Domain.Models;
using web_services_ielectric.Appliances.Domain.Repositories;
using web_services_ielectric.Appliances.Domain.Services;
using web_services_ielectric.Appliances.Domain.Services.Communication;
using web_services_ielectric.Clients.Domain.Repositories;
using web_services_ielectric.Shared.Domain.Repositories;

namespace web_services_ielectric.Appliances.Services;

public class ApplianceService : IApplianceService
{
    private readonly IApplianceRepository _applianceRepository;
    private readonly IApplianceModelRepository _applianceModelRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ApplianceService(IApplianceRepository applianceRepository,
        IApplianceModelRepository applianceModelRepository,
        IClientRepository clientRepository,
        IUnitOfWork unitOfWork)
    {
        _applianceRepository = applianceRepository;
        _applianceModelRepository = applianceModelRepository;
        _clientRepository = clientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ApplianceResponse> DeleteAsync(long id)
    {
        var existingAppliance = await _applianceRepository.FindByIdAsync(id);

        if (existingAppliance == null)
            return new ApplianceResponse("Appliance not found");

        try
        {
            _applianceRepository.Remove(existingAppliance);
            await _unitOfWork.CompleteAsync();

            return new ApplianceResponse(existingAppliance);
        }
        catch (Exception e)
        {
            return new ApplianceResponse($"An error ocurred while deleting the appliance: {e.Message}");
        }
    }

    public async Task<ApplianceResponse> GetByIdAsync(long id)
    {
        var existingAppliance = await _applianceRepository.FindByIdAsync(id);

        if (existingAppliance == null)
            return new ApplianceResponse("Appliance not found");

        return new ApplianceResponse(existingAppliance);
    }

    public async Task<IEnumerable<Appliance>> ListByClientIdAsync(long clientId)
    {
        return await _applianceRepository.FindByClientIdAsync(clientId);
    }

    public async Task<ApplianceResponse> SaveAsync(Appliance appliance)
    {
        var existingClient = await _clientRepository.FindByIdAsync(appliance.ClientId);

        if (existingClient == null)
            return new ApplianceResponse("Client not found");

        var existingApplianceModel = await _applianceModelRepository.FindByIdAsync(appliance.ApplianceModelId);

        if (existingApplianceModel == null) 
            return new ApplianceResponse("Appliance Model not found");

        try
        {
            await _applianceRepository.AddAsync(appliance);
            await _unitOfWork.CompleteAsync();

            return new ApplianceResponse(appliance);
        }
        catch (Exception e)
        {
            return new ApplianceResponse($"An error occurred while saving the appliance: {e.Message}");
        }
    }

    public async Task<ApplianceResponse> UpdateAsync(long id, Appliance appliance)
    {
        var existingAppliance = await _applianceRepository.FindByIdAsync(id);

        if (existingAppliance == null)
            return new ApplianceResponse("Appliance not found");

        var existingClient = await _clientRepository.FindByIdAsync(appliance.ClientId);

        if (existingClient == null)
            return new ApplianceResponse("Client not found");

        var existingApplianceModel = await _applianceModelRepository.FindByIdAsync(appliance.ApplianceModelId);

        if (existingApplianceModel == null)
            return new ApplianceResponse("Appliance Model not found");

        existingAppliance.ClientId = appliance.ClientId;
        existingAppliance.ApplianceModelId = appliance.ApplianceModelId;
        existingAppliance.PurchaseDate = appliance.PurchaseDate;

        try
        {
            _applianceRepository.Update(existingAppliance);
            await _unitOfWork.CompleteAsync();

            return new ApplianceResponse(existingAppliance);
        }
        catch (Exception e)
        {
            return new ApplianceResponse($"An error occurred while updating the appliance: {e.Message}");
        }
    }
}