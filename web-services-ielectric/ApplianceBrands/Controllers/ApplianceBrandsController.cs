using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using web_services_ielectric.ApplianceBrands.Domain.Models;
using web_services_ielectric.ApplianceBrands.Domain.Services;
using web_services_ielectric.ApplianceBrands.Resources;
using web_services_ielectric.Shared.Extensions;

namespace web_services_ielectric.ApplianceBrands.Controllers;

[ApiController]
[Produces("application/json")]
[Route("/api/v1/[controller]")]
public class ApplianceBrandsController : ControllerBase
{
    private readonly IApplianceBrandService _applianceBrandService;
    private readonly IMapper _mapper;

    public ApplianceBrandsController(IApplianceBrandService applianceBrandService, IMapper mapper)
    {
        _applianceBrandService = applianceBrandService;
        _mapper = mapper;
    }
    [SwaggerOperation(
        Summary = "Get all ApplianceBrands",
        Description = "Get of all ApplianceBrands",
        OperationId = "GetAllApplianceBrands")]
    [SwaggerResponse(200, "All ApplianceBrands returned", typeof(IEnumerable<ApplianceBrandResource>))]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ApplianceBrandResource>), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IEnumerable<ApplianceBrandResource>> GetAllAsync()
    {
        var applianceBrands = await _applianceBrandService.ListAsync();
        var resources = _mapper.Map<IEnumerable<ApplianceBrand>, IEnumerable<ApplianceBrandResource>>(applianceBrands);
        return resources;
    }
    [SwaggerOperation(
        Summary = "Get ApplianceBrand by Id",
        Description = "Get ApplianceBrand by Id",
        OperationId = "GetApplianceBrandsById")]
    [SwaggerResponse(200, "ApplianceBrand returned", typeof(ApplianceBrandResource))]
    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(ApplianceBrandResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> GetByIdAsync(long id)
    {
        var result = await _applianceBrandService.GetByIdAsync(id);
        if (!result.Success)
            return BadRequest(result.Message);
        var applianceBrandResult = _mapper.Map<ApplianceBrand, ApplianceBrandResource>(result.Resource);

        return Ok(applianceBrandResult);
    }
    [SwaggerOperation(
        Summary = "Save ApplianceBrand",
        Description = "Save ApplianceBrand",
        OperationId = "SaveApplianceBrand")]
    [SwaggerResponse(200, "ApplianceBrand saved", typeof(ApplianceBrandResource))]
    [HttpPost]
    [ProducesResponseType(typeof(ApplianceBrandResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> PostAsync([FromBody] SaveApplianceBrandResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var applianceBrand = _mapper.Map<SaveApplianceBrandResource, ApplianceBrand>(resource);
        var result = await _applianceBrandService.SaveAsync(applianceBrand);
        if (!result.Success)
            return BadRequest(result.Message);

        var applianceBrandResource = _mapper.Map<ApplianceBrand, ApplianceBrandResource>(result.Resource);
        return Ok(applianceBrandResource); 
    }
    [SwaggerOperation(
        Summary = "Update ApplianceBrand",
        Description = "Update ApplianceBrand",
        OperationId = "UpdateApplianceBrand")]
    [SwaggerResponse(200, "ApplianceBrand updated", typeof(ApplianceBrandResource))]
    [HttpPut("{id:long}")]
    [ProducesResponseType(typeof(ApplianceBrandResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> PutAsync(long id, [FromBody] SaveApplianceBrandResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var applianceBrand = _mapper.Map<SaveApplianceBrandResource, ApplianceBrand>(resource);
        var result = await _applianceBrandService.UpdateAsync(id, applianceBrand);
        if (!result.Success)
            return BadRequest(result.Message);

        var applianceBrandResource = _mapper.Map<ApplianceBrand, ApplianceBrandResource>(result.Resource);
        return Ok(applianceBrandResource);
    }
    [SwaggerOperation(
        Summary = "Delete ApplianceBrand",
        Description = "Delete ApplianceBrand",
        OperationId = "DeleteApplianceBrand")]
    [SwaggerResponse(200, "ApplianceBrand deleted", typeof(ApplianceBrandResource))]
    [HttpDelete("{id:long}")]
    [ProducesResponseType(typeof(ApplianceBrandResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        var result = await _applianceBrandService.DeleteAsync(id);
        if (!result.Success)
            return BadRequest(result.Message);
        var applianceBrandResource = _mapper.Map<ApplianceBrand, ApplianceBrandResource>(result.Resource);
        return Ok(applianceBrandResource);
    }
}