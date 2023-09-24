using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using web_services_ielectric.Administrators.Domain.Services;
using Swashbuckle.AspNetCore.Annotations;
using web_services_ielectric.Administrators.Domain.Models;
using web_services_ielectric.Administrators.Resources;
using web_services_ielectric.Shared.Extensions;

namespace web_services_ielectric.Administrators.Controllers;

[ApiController]
[Produces("application/json")]
[Route("/api/v1/[controller]")]
public class AdministratorsController : ControllerBase
{
    private readonly IAdministratorService _administratorService;
    private readonly IMapper _mapper;

    public AdministratorsController(IAdministratorService administratorService, IMapper mapper)
    {
        _administratorService = administratorService;
        _mapper = mapper;
    }

    [SwaggerOperation(
    Summary = "Get all Administrators",
    Description = "Get of all Administrators",
    OperationId = "GetAllAdministrators")]
    [SwaggerResponse(200, "All Administrators returned", typeof(IEnumerable<AdministratorResource>))]

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AdministratorResource>), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IEnumerable<AdministratorResource>> GetAllAsync()
    {
        var administrators = await _administratorService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Administrator>, IEnumerable<AdministratorResource>>(administrators);
        return resources;
    }

    [SwaggerOperation(
    Summary = "Get Administrators by Id",
    Description = "Get Administrators by Id",
    OperationId = "GetAdministratorsById")]
    [SwaggerResponse(200, "Administrators returned", typeof(AdministratorResource))]

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AdministratorResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> GetByIdAsync(long id)
    {
        var result = await _administratorService.GetByIdAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var administratorResult = _mapper.Map<Administrator, AdministratorResource>(result.Resource);

        return Ok(administratorResult);
    }

    [SwaggerOperation(
    Summary = "Get Administrators by User Id",
    Description = "Get Administrators by User Id",
    OperationId = "GetAdministratorsByUserId")]
    [SwaggerResponse(200, "Administrators returned", typeof(AdministratorResource))]

    [HttpGet("user/{userId}")]
    [ProducesResponseType(typeof(AdministratorResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> GetByUserIdAsync(long userId)
    {
        var result = await _administratorService.GetByUserIdAsync(userId);

        if (!result.Success)
            return BadRequest(result.Message);

        var administratorResult = _mapper.Map<Administrator, AdministratorResource>(result.Resource);

        return Ok(administratorResult);
    }

    [SwaggerOperation(
    Summary = "Save Administrator",
    Description = "Save Administrator",
    OperationId = "SaveAdministrator")]
    [SwaggerResponse(200, "Administrator saved", typeof(AdministratorResource))]

    [HttpPost]
    [ProducesResponseType(typeof(AdministratorResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> PostAsync([FromBody] SaveAdministratorResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var administrator = _mapper.Map<SaveAdministratorResource, Administrator>(resource);
        var result = await _administratorService.SaveAsync(administrator);

        if (!result.Success)
            return BadRequest(result.Message);

        var administratorResource = _mapper.Map<Administrator, AdministratorResource>(result.Resource);

        return Ok(administratorResource);
    }

    [SwaggerOperation(
    Summary = "Update Administrator",
    Description = "Update Administrator",
    OperationId = "UpdateAdministrator")]
    [SwaggerResponse(200, "Administrator updated", typeof(AdministratorResource))]

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AdministratorResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> PutAsync(long id, [FromBody] SaveAdministratorResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var administrator = _mapper.Map<SaveAdministratorResource, Administrator>(resource);
        var result = await _administratorService.UpdateAsync(id, administrator);

        if (!result.Success)
            return BadRequest(result.Message);

        var administratorResource = _mapper.Map<Administrator, AdministratorResource>(result.Resource);

        return Ok(administratorResource);
    }

    [SwaggerOperation(
    Summary = "Delete Administrator",
    Description = "Delete Administrator",
    OperationId = "DeleteAdministrator")]
    [SwaggerResponse(200, "Administrator deleted", typeof(AdministratorResource))]

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(AdministratorResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        var result = await _administratorService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var administratorResource = _mapper.Map<Administrator, AdministratorResource>(result.Resource);

        return Ok(administratorResource);
    }
}