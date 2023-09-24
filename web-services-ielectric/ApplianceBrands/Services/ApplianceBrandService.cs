using web_services_ielectric.ApplianceBrands.Domain.Models;
using web_services_ielectric.ApplianceBrands.Domain.Repositories;
using web_services_ielectric.ApplianceBrands.Domain.Services;
using web_services_ielectric.ApplianceBrands.Domain.Services.Communication;
using web_services_ielectric.Shared.Domain.Repositories;

namespace web_services_ielectric.ApplianceBrands.Services;

public class ApplianceBrandService : IApplianceBrandService
{
    private readonly IApplianceBrandRepository _applianceBrandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ApplianceBrandService(IApplianceBrandRepository applianceBrandRepository, IUnitOfWork unitOfWork)
    {
        _applianceBrandRepository = applianceBrandRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<ApplianceBrand>> ListAsync()
    {
        return await _applianceBrandRepository.ListAsync();
    }

    public async Task<ApplianceBrandResponse> GetByIdAsync(long id)
    {
        var existingApplianceBrand = await _applianceBrandRepository.FindByIdAsync(id);
        return existingApplianceBrand == null
            ? new ApplianceBrandResponse("ApplianceBrand not found.")
            : new ApplianceBrandResponse(existingApplianceBrand);
    }

    public async Task<ApplianceBrandResponse> SaveAsync(ApplianceBrand applianceBrand)
    {
        try
        {
            await _applianceBrandRepository.AddAsync(applianceBrand);
            await _unitOfWork.CompleteAsync();
            return new ApplianceBrandResponse(applianceBrand);
        }
        catch (Exception e)
        {
            return new ApplianceBrandResponse($"An error occurred while saving applianceBrand: {e.Message}.");
        }
    }

    public async Task<ApplianceBrandResponse> UpdateAsync(long id, ApplianceBrand applianceBrand)
    {
        var existingApplianceBrand = await _applianceBrandRepository.FindByIdAsync(id);
        if (existingApplianceBrand==null)
        {
            return new ApplianceBrandResponse("ApplianceBrand not found.");
        }

        existingApplianceBrand.Name = applianceBrand.Name;
        existingApplianceBrand.ImgPath = applianceBrand.ImgPath;
        existingApplianceBrand.ApplianceModels = applianceBrand.ApplianceModels;
        try
        {
            _applianceBrandRepository.Update(existingApplianceBrand);
            await _unitOfWork.CompleteAsync();
            return new ApplianceBrandResponse(existingApplianceBrand);
        }
        catch (Exception e)
        {
            return new ApplianceBrandResponse($"An error occurred while updating the applianceBrand: {e.Message}.");
        }
    }

    public async Task<ApplianceBrandResponse> DeleteAsync(long id)
    {
        var existingApplianceBrand = await _applianceBrandRepository.FindByIdAsync(id);
        if (existingApplianceBrand == null)
            return new ApplianceBrandResponse("ApplianceBrand not found.");
        try
        {
            _applianceBrandRepository.Remove(existingApplianceBrand);
            await _unitOfWork.CompleteAsync();
            return new ApplianceBrandResponse(existingApplianceBrand);
        }
        catch (Exception e)
        {
            return new ApplianceBrandResponse($"An error occurred while deleting the applianceBrand: {e.Message}.");
        }
    }
}