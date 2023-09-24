using web_services_ielectric.ApplianceBrands.Domain.Repositories;
using web_services_ielectric.ApplianceModels.Domain.Models;
using web_services_ielectric.ApplianceModels.Domain.Repositories;
using web_services_ielectric.ApplianceModels.Domain.Services;
using web_services_ielectric.ApplianceModels.Domain.Services.Communication;
using web_services_ielectric.Shared.Domain.Repositories;

namespace web_services_ielectric.ApplianceModels.Services;

public class ApplianceModelService : IApplianceModelService
{
    private readonly IApplianceModelRepository _applianceModelRepository;
    private readonly IApplianceBrandRepository _applianceBrandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ApplianceModelService(IApplianceModelRepository applianceModelRepository,IApplianceBrandRepository applianceBrandRepository, IUnitOfWork unitOfWork)
    {
        _applianceModelRepository = applianceModelRepository;
        _applianceBrandRepository = applianceBrandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ApplianceModel>> ListAsync()
    {
        return await _applianceModelRepository.ListAsync();
    }

    public async Task<IEnumerable<ApplianceModel>> ListByApplianceBrandIdAsync(long applianceBrandId)
    {
        return await _applianceModelRepository.FindByApplianceBrandId(applianceBrandId);
    }

    public async Task<ApplianceModelResponse> GetByIdAsync(long id)
    {
        var existingApplianceModel = await _applianceModelRepository.FindByIdAsync(id);
        return existingApplianceModel == null ? new ApplianceModelResponse("ApplianceModel not found.") : new ApplianceModelResponse(existingApplianceModel);
    }

    public async Task<ApplianceModelResponse> SaveAsync(ApplianceModel applianceModel)
    {
        var existingBrand = _applianceBrandRepository.FindByIdAsync(applianceModel.ApplianceBrandId);
        if (existingBrand == null)
        {
            return new ApplianceModelResponse("Invalid Brand");
        }
        
        try
        {
            await _applianceModelRepository.AddAsync(applianceModel);
            await _unitOfWork.CompleteAsync();
            return new ApplianceModelResponse(applianceModel);
        }
        catch (Exception e)
        {
            return new ApplianceModelResponse($"An error occurred while saving applianceModel: {e.Message}.");
        }
    }

    public async Task<ApplianceModelResponse> UpdateAsync(long id, ApplianceModel applianceModel)
    {
        var existingApplianceModel = await _applianceModelRepository.FindByIdAsync(id);
        if (existingApplianceModel == null)
            return new ApplianceModelResponse("ApplianceModel not found.");
        var existingBrand = await _applianceBrandRepository.FindByIdAsync(applianceModel.ApplianceBrandId);
        if (existingBrand == null)
        {
            return new ApplianceModelResponse("Invalid Brand");
        }
        existingApplianceModel.Name = applianceModel.Name;
        existingApplianceModel.Model = applianceModel.Model;
        existingApplianceModel.ImgPath = applianceModel.ImgPath;
        existingApplianceModel.ApplianceBrandId = applianceModel.ApplianceBrandId;
        try
        {
            _applianceModelRepository.Update(existingApplianceModel);
            await _unitOfWork.CompleteAsync();
            return new ApplianceModelResponse(existingApplianceModel);
        }
        catch (Exception e)
        {
            return new ApplianceModelResponse($"An error occurred while updating the applianceModel:{e.Message}");
        }
    }

    public async Task<ApplianceModelResponse> DeleteAsync(long id)
    {
        var existingApplianceModel = await _applianceModelRepository.FindByIdAsync(id);
        if (existingApplianceModel == null)
            return new ApplianceModelResponse("ApplianceModel not found.");
        try
        {
            _applianceModelRepository.Remove(existingApplianceModel);
            await _unitOfWork.CompleteAsync();
            return new ApplianceModelResponse(existingApplianceModel);
        }
        catch(Exception e)
        {
            return new ApplianceModelResponse($"An error occurred while deleting the applianceModel: {e.Message}.");
        }
    }
}