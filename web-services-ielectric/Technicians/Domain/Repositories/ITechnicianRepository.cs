using web_services_ielectric.Reports.Domain.Models;
using web_services_ielectric.Technicians.Domain.Models;

namespace web_services_ielectric.Technicians.Domain.Repositories;

public interface ITechnicianRepository
{
    Task<IEnumerable<Technician>> ListAsync();
    Task AddAsync(Technician technician);
    Task<Technician> FindByIdAsync(long id);
    Task<Technician> FindByUserIdAsync(long userId);
    Task<IEnumerable<Report>> FindByTechnicianId(int reportId);
    void Update(Technician technician);
    void Remove(Technician technician);
}