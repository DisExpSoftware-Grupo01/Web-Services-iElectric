using web_services_ielectric.SpareRequests.Domain.Models;

namespace web_services_ielectric.SpareRequests.Domain.Repositories;

public interface ISpareRequestRepository
{
    Task<IEnumerable<SpareRequest>> ListAsync();
    Task AddAsync(SpareRequest spareRequest);
    Task<SpareRequest> FindByIdAsync(long id);
    void Update(SpareRequest spareRequest);
    void Remove(SpareRequest spareRequest);
}