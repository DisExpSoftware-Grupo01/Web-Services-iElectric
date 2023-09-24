using web_services_ielectric.Administrators.Domain.Models;
using web_services_ielectric.Administrators.Domain.Repositories;
using web_services_ielectric.Administrators.Domain.Services;
using web_services_ielectric.Administrators.Domain.Services.Communication;
using web_services_ielectric.Shared.Domain.Repositories;

namespace web_services_ielectric.Administrators.Services;

public class AdministratorService : IAdministratorService
{
    private readonly IAdministratorRepository _administratorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AdministratorService(IAdministratorRepository administratorRepository, IUnitOfWork unitOfWork)
    {
        _administratorRepository = administratorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<AdministratorResponse> DeleteAsync(long id)
    {
        var existingAdministrator = await _administratorRepository.FindByIdAsync(id);

        if (existingAdministrator == null)
            return new AdministratorResponse("Administrator not found.");

        try
        {
            _administratorRepository.Remove(existingAdministrator);
            await _unitOfWork.CompleteAsync();

            return new AdministratorResponse(existingAdministrator);
        }
        catch (Exception e)
        {
            return new AdministratorResponse($"An error occurred while deleting the administrator: {e.Message}");
        };
    }

    public async Task<AdministratorResponse> GetByIdAsync(long id)
    {
        var existingAdministrator = await _administratorRepository.FindByIdAsync(id);

        if (existingAdministrator == null)
            return new AdministratorResponse("Administrator not found.");

        return new AdministratorResponse(existingAdministrator);
    }

    public async Task<AdministratorResponse> GetByUserIdAsync(long userId)
    {
        var existingAdministrator = await _administratorRepository.FindByIdAsync(userId);

        if (existingAdministrator == null)
            return new AdministratorResponse("Administrator not found.");

        return new AdministratorResponse(existingAdministrator);
    }

    public async Task<IEnumerable<Administrator>> ListAsync()
    {
        return await _administratorRepository.ListAsync();
    }

    public async Task<AdministratorResponse> SaveAsync(Administrator administrator)
    {
        try
        {
            await _administratorRepository.AddAsync(administrator);
            await _unitOfWork.CompleteAsync();

            return new AdministratorResponse(administrator);
        }
        catch (Exception e)
        {
            return new AdministratorResponse($"An error occurred while saving the administrator: {e.Message}");
        }
    }

    public async Task<AdministratorResponse> UpdateAsync(long id, Administrator administrator)
    {
        var existingAdministrator = await _administratorRepository.FindByIdAsync(id);

        if (existingAdministrator == null)
            return new AdministratorResponse("Administrator not found.");

        existingAdministrator.Names = administrator.Names;
        existingAdministrator.LastNames = administrator.LastNames;
        existingAdministrator.CellphoneNumber = administrator.CellphoneNumber;
        existingAdministrator.Address = administrator.Address;
        existingAdministrator.UserId = administrator.UserId;

        try
        {
            _administratorRepository.Update(existingAdministrator);
            await _unitOfWork.CompleteAsync();

            return new AdministratorResponse(existingAdministrator);
        }
        catch (Exception e)
        {
            return new AdministratorResponse($"An error occurred while updating the administrator: {e.Message}");
        }
    }
}