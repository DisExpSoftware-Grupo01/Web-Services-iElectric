using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using web_services_ielectric.ApplianceModels.Domain.Services;
using Swashbuckle.AspNetCore.Annotations;
using web_services_ielectric.ApplianceModels.Domain.Models;
using web_services_ielectric.ApplianceModels.Resources;
using web_services_ielectric.Shared.Extensions;

namespace web_services_ielectric.ApplianceModels.Controllers;

[ApiController]
[Produces("application/json")]
[Route("/api/v1/[controller]")]
public class ApplianceModelsController : ControllerBase
{
    private readonly IApplianceModelService _applianceModelService;
    private readonly IMapper _mapper;

    public ApplianceModelsController(IApplianceModelService applianceModelService, IMapper mapper)
    {
        _applianceModelService = applianceModelService;
        _mapper = mapper;
    }
    [SwaggerOperation(
        Summary = "Get all ApplianceModels",
        Description = "Get of all ApplianceModels",
        OperationId = "GetAllApplianceModels")]
    [SwaggerResponse(200, "All ApplianceModels returned", typeof(IEnumerable<ApplianceModelResource>))]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ApplianceModelResource>), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IEnumerable<ApplianceModelResource>> GetAllAsync()
    {
        var applianceModels = await _applianceModelService.ListAsync();
        var resources = _mapper.Map<IEnumerable<ApplianceModel>, IEnumerable<ApplianceModelResource>>(applianceModels);
        return resources;
    }

    [SwaggerOperation(
        Summary = "Get ApplianceModel by Id",
        Description = "Get ApplianceModel by Id",
        OperationId = "GetApplianceModelsById")]
    [SwaggerResponse(200, "ApplianceModel returned", typeof(ApplianceModelResource))]
    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(ApplianceModelResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> GetByIdAsync(long id)
    {
        var result = await _applianceModelService.GetByIdAsync(id);
        if (!result.Success)
            return BadRequest(result.Message);
        var applianceModelResult = _mapper.Map<ApplianceModel, ApplianceModelResource>(result.Resource);

        return Ok(applianceModelResult);
    }

    [SwaggerOperation(
        Summary = "Save ApplianceModel",
        Description = "Save ApplianceModel",
        OperationId = "SaveApplianceModel")]
    [SwaggerResponse(200, "ApplianceModel saved", typeof(ApplianceModelResource))]
    [HttpPost]
    [ProducesResponseType(typeof(ApplianceModelResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> PostAsync([FromBody] SaveApplianceModelResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var applianceModel = _mapper.Map<SaveApplianceModelResource, ApplianceModel>(resource);
        var result = await _applianceModelService.SaveAsync(applianceModel);
        if (!result.Success)
            return BadRequest(result.Message);

        var applianceModelResource = _mapper.Map<ApplianceModel, ApplianceModelResource>(result.Resource);
        return Ok(applianceModelResource); 
    }

    [SwaggerOperation(
        Summary = "Update ApplianceModel",
        Description = "Update ApplianceModel",
        OperationId = "UpdateApplianceModel")]
    [SwaggerResponse(200, "ApplianceModel updated", typeof(ApplianceModelResource))]
    [HttpPut("{id:long}")]
    [ProducesResponseType(typeof(ApplianceModelResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> PutAsync(long id, [FromBody] SaveApplianceModelResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var applianceModel = _mapper.Map<SaveApplianceModelResource, ApplianceModel>(resource);
        var result = await _applianceModelService.UpdateAsync(id, applianceModel);
        if (!result.Success)
            return BadRequest(result.Message);

        var applianceModelResource = _mapper.Map<ApplianceModel, ApplianceModelResource>(result.Resource);
        return Ok(applianceModelResource);
    }

    [SwaggerOperation(
        Summary = "Delete ApplianceModel",
        Description = "Delete ApplianceModel",
        OperationId = "DeleteApplianceModel")]
    [SwaggerResponse(200, "ApplianceModel deleted", typeof(ApplianceModelResource))]
    [HttpDelete("{id:long}")]
    [ProducesResponseType(typeof(ApplianceModelResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        var result = await _applianceModelService.DeleteAsync(id);
        if (!result.Success)
            return BadRequest(result.Message);
        var applianceModelResource = _mapper.Map<ApplianceModel, ApplianceModelResource>(result.Resource);
        return Ok(applianceModelResource);
    }
}