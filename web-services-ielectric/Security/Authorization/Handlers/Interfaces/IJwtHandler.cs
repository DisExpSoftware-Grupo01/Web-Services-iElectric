using web_services_ielectric.Security.Domain.Entities;

namespace web_services_ielectric.Security.Authorization.Handlers.Interfaces;

public interface IJwtHandler
{
    public string GenerateToken(User user);
    public int? ValidateToken(string token);
}