using web_services_ielectric.Reports.Domain.Models;
using web_services_ielectric.Technicians.Domain.Models;
using web_services_ielectric.Technicians.Domain.Services.Communication;

namespace web_services_ielectric.Technicians.Domain.Services;

public interface ITechnicianService
{
    Task<IEnumerable<Technician>> ListAsync();
    Task<TechnicianResponse> GetByIdAsync(long id);
    Task<TechnicianResponse> GetByUserIdAsync(long id);
    Task<TechnicianResponse> SaveAsync(Technician technician);
    Task<TechnicianResponse> UpdateAsync(long id, Technician technician);
    Task<TechnicianResponse> DeleteAsync(long id);
    Task<IEnumerable<Report>> ListByTechnicianIdAsync(int technicianId);
}