using web_services_ielectric.Security.Domain.Entities;
using web_services_ielectric.Security.Domain.Services.Communication;

namespace web_services_ielectric.Security.Domain.Services;

public interface IUserService
{
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest request);
    Task<IEnumerable<User>> ListAsync();
    Task<User> GetByIdAsync(int id);
    Task<RegisterResponse> RegisterAsync(RegisterRequest request);
    Task UpdateAsync(int id, UpdateRequest request);
    Task DeleteAsync(int id);
}