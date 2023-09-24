using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using web_services_ielectric.Security.Authorization.Attributes;
using web_services_ielectric.Security.Domain.Entities;
using web_services_ielectric.Security.Domain.Services;
using web_services_ielectric.Security.Domain.Services.Communication;
using web_services_ielectric.Security.Resources;

namespace web_services_ielectric.Security.Controllers;

[Authorize]
[ApiController]
[Route("/api/v1/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost("auth/sign-in")]
    public async Task<IActionResult> Authenticate(AuthenticateRequest request)
    {
        var response = await _userService.Authenticate(request);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("auth/sign-up")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var response = await _userService.RegisterAsync(request);

        if (response == null)
            return BadRequest(new { message = "User don't register" });

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.ListAsync();
        var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
        return Ok(resources);
    }
}