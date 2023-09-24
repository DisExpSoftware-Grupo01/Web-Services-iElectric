using web_services_ielectric.Reports.Domain.Models;
using web_services_ielectric.Reports.Domain.Repositories;
using web_services_ielectric.Shared.Domain.Repositories;
using web_services_ielectric.Technicians.Domain.Models;
using web_services_ielectric.Technicians.Domain.Repositories;
using web_services_ielectric.Technicians.Domain.Services;
using web_services_ielectric.Technicians.Domain.Services.Communication;

namespace web_services_ielectric.Technicians.Services;

public class TechnicianService : ITechnicianService
{
    private readonly ITechnicianRepository _technicianRepository;
        private readonly IReportRepository _reportRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TechnicianService(ITechnicianRepository technicianRepository, IUnitOfWork unitOfWork, IReportRepository reportRepository)
        {
            _technicianRepository = technicianRepository;
            _reportRepository = reportRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TechnicianResponse> DeleteAsync(long id)
        {
            var existingTechnician = await _technicianRepository.FindByIdAsync(id);

            if (existingTechnician == null)
                return new TechnicianResponse("Client not found.");

            try
            {
                _technicianRepository.Remove(existingTechnician);
                await _unitOfWork.CompleteAsync();

                return new TechnicianResponse(existingTechnician);
            }
            catch (Exception e)
            {
                return new TechnicianResponse($"An error occurred while deleting the technician: {e.Message}");
            };
        }

        public async Task<TechnicianResponse> GetByIdAsync(long id)
        {
            var existingTechnician = await _technicianRepository.FindByIdAsync(id);

            if (existingTechnician == null)
                return new TechnicianResponse("Technician not found.");

            return new TechnicianResponse(existingTechnician);
        }

        public async Task<TechnicianResponse> GetByUserIdAsync(long userId)
        {
            var existingTechnician = await _technicianRepository.FindByUserIdAsync(userId);

            if (existingTechnician == null)
                return new TechnicianResponse("Technician not found.");

            return new TechnicianResponse(existingTechnician);
        }

        public async Task<IEnumerable<Technician>> ListAsync()
        {
            return await _technicianRepository.ListAsync();
        }

        public async Task<TechnicianResponse> SaveAsync(Technician technician)
        {
            try
            {
                await _technicianRepository.AddAsync(technician);
                await _unitOfWork.CompleteAsync();

                return new TechnicianResponse(technician);
            }
            catch (Exception e)
            {
                return new TechnicianResponse($"An error occurred while saving the technician: {e.Message}");
            }
        }

        public async Task<TechnicianResponse> UpdateAsync(long id, Technician technician)
        {
            var existingTechnician = await _technicianRepository.FindByIdAsync(id);

            if (existingTechnician == null)
                return new TechnicianResponse("Technician not found.");

            existingTechnician.Names = technician.Names;
            existingTechnician.LastNames = technician.LastNames;
            existingTechnician.CellphoneNumber = technician.CellphoneNumber;
            existingTechnician.Address = technician.Address;
            existingTechnician.UserId = technician.UserId;

            try
            {
                _technicianRepository.Update(existingTechnician);
                await _unitOfWork.CompleteAsync();

                return new TechnicianResponse(existingTechnician);
            }
            catch (Exception e)
            {
                return new TechnicianResponse($"An error occurred while updating the technician: {e.Message}");
            }
        }
        
        public async Task<IEnumerable<Report>> ListByTechnicianIdAsync(int technicianId)
        {
            return await _technicianRepository.FindByTechnicianId(technicianId);
        }
}