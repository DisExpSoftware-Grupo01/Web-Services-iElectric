using System.ComponentModel.DataAnnotations;

namespace web_services_ielectric.Security.Domain.Services.Communication;

public class RegisterRequest
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}