using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using web_services_ielectric.Appliances.Domain.Models;
using web_services_ielectric.Appliances.Domain.Services;
using web_services_ielectric.Appliances.Resources;
using web_services_ielectric.Shared.Extensions;

namespace web_services_ielectric.Appliances.Controller;

[ApiController]
[Produces("application/json")]
[Route("/api/v1/[controller]")]
public class AppliancesController : ControllerBase
{
    private readonly IApplianceService _applianceService;
    private readonly IMapper _mapper;

    public AppliancesController(IApplianceService applianceService, IMapper mapper)
    {
        _applianceService = applianceService;
        _mapper = mapper;
    }

    [SwaggerOperation(
    Summary = "Get Appliance by Id",
    Description = "Get Appliance by Id",
    OperationId = "GetApplianceById")]
    [SwaggerResponse(200, "Appliance returned", typeof(ApplianceResource))]

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApplianceResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> GetByIdAsync(long id)
    {
        var result = await _applianceService.GetByIdAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var applianceResource = _mapper.Map<Appliance, ApplianceResource>(result.Resource);

        return Ok(applianceResource);
    }

    [SwaggerOperation(
    Summary = "Get all Appliance by Client Id",
    Description = "Get of Appliance by Client Id",
    OperationId = "GetAllapplianceByClientId")]
    [SwaggerResponse(200, "All Appliance of client returned", typeof(IEnumerable<ApplianceResource>))]

    [HttpGet("client/{clientId}")]
    [ProducesResponseType(typeof(IEnumerable<ApplianceResource>), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IEnumerable<ApplianceResource>> GetAllByClientId(long clientId)
    {
        var appliances = await _applianceService.ListByClientIdAsync(clientId);
        var resources = _mapper.Map<IEnumerable<Appliance>, IEnumerable<ApplianceResource>>(appliances);
        return resources;
    }

    [SwaggerOperation(
    Summary = "Save Appliance",
    Description = "Save Appliance",
    OperationId = "SaveAppliance")]
    [SwaggerResponse(200, "Appliance saved", typeof(ApplianceResource))]

    [HttpPost]
    [ProducesResponseType(typeof(ApplianceResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> PostAsync([FromBody] SaveApplianceResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var appliance = _mapper.Map<SaveApplianceResource, Appliance>(resource);
        var result = await _applianceService.SaveAsync(appliance);

        if (!result.Success)
            return BadRequest(result.Message);

        var applianceResource= _mapper.Map<Appliance, ApplianceResource>(result.Resource);

        return Ok(applianceResource);
    }

    [SwaggerOperation(
    Summary = "Update Appliance",
    Description = "Update Appliance",
    OperationId = "UpdateAppliance")]
    [SwaggerResponse(200, "Appliance updated", typeof(ApplianceResource))]

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApplianceResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> PutAsync(long id, [FromBody] SaveApplianceResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var appliance = _mapper.Map<SaveApplianceResource, Appliance>(resource);
        var result = await _applianceService.UpdateAsync(id, appliance);

        if (!result.Success)
            return BadRequest(result.Message);

        var applianceResource = _mapper.Map<Appliance, ApplianceResource>(result.Resource);

        return Ok(applianceResource);
    }

    [SwaggerOperation(
    Summary = "Delete Appliance",
    Description = "Delete Appliance",
    OperationId = "DeleteAppliance")]
    [SwaggerResponse(200, "Appliance deleted", typeof(ApplianceResource))]

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApplianceResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        var result = await _applianceService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var applianceResource = _mapper.Map<Appliance, ApplianceResource>(result.Resource);

        return Ok(applianceResource);
    }
}