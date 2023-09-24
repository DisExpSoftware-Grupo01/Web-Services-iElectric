using web_services_ielectric.Shared.Domain.Repositories;
using web_services_ielectric.SpareRequests.Domain.Models;
using web_services_ielectric.SpareRequests.Domain.Repositories;
using web_services_ielectric.SpareRequests.Domain.Services;
using web_services_ielectric.SpareRequests.Domain.Services.Communication;

namespace web_services_ielectric.SpareRequests.Services;

public class SpareRequestService : ISpareRequestService
{
    private readonly ISpareRequestRepository _spareRequestRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SpareRequestService(ISpareRequestRepository spareRequestRepository, IUnitOfWork unitOfWork)
    {
        _spareRequestRepository = spareRequestRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<SpareRequestResponse> DeleteAsync(long id)
    {
        var existingSpareRequest = await _spareRequestRepository.FindByIdAsync(id);

        if (existingSpareRequest == null)
            return new SpareRequestResponse("Spare Request not found.");

        try
        {
            _spareRequestRepository.Remove(existingSpareRequest);
            await _unitOfWork.CompleteAsync();

            return new SpareRequestResponse(existingSpareRequest);
        }
        catch (Exception e)
        {
            return new SpareRequestResponse($"An error occurred while deleting the spare request: {e.Message}");
        }
    }

    public async Task<SpareRequestResponse> GetByIdAsync(long id)
    {
        var existingSpareRequest = await _spareRequestRepository.FindByIdAsync(id);

        if (existingSpareRequest == null)
            return new SpareRequestResponse("Spare Request not found.");

        return new SpareRequestResponse(existingSpareRequest);
    }

    public async Task<IEnumerable<SpareRequest>> ListAsync()
    {
        return await _spareRequestRepository.ListAsync();
    }

    public async Task<SpareRequestResponse> SaveAsync(SpareRequest spareRequest)
    {
        try
        {
            await _spareRequestRepository.AddAsync(spareRequest);
            await _unitOfWork.CompleteAsync();

            return new SpareRequestResponse(spareRequest);
        }
        catch (Exception e)
        {
            return new SpareRequestResponse($"An error occurred while saving the spare request: {e.Message}");
        }
    }

    public async Task<SpareRequestResponse> UpdateAsync(long id, SpareRequest spareRequest)
    {
        var existingSpareRequest = await _spareRequestRepository.FindByIdAsync(id);

        if (existingSpareRequest == null)
            return new SpareRequestResponse("Spare Request not found.");

        existingSpareRequest.Description = spareRequest.Description;
        existingSpareRequest.Date = spareRequest.Description;
        existingSpareRequest.ImagePath = spareRequest.ImagePath;
        existingSpareRequest.TechnicianId = spareRequest.TechnicianId;

        try
        {
            _spareRequestRepository.Update(existingSpareRequest);
            await _unitOfWork.CompleteAsync();

            return new SpareRequestResponse(existingSpareRequest);
        }
        catch (Exception e)
        {
            return new SpareRequestResponse($"An error occurred while updating the spare request: {e.Message}");
        }
    }
    
}