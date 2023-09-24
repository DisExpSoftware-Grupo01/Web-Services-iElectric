using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using web_services_ielectric.SpareRequests.Domain.Services;
using Swashbuckle.AspNetCore.Annotations;
using web_services_ielectric.Shared.Extensions;
using web_services_ielectric.SpareRequests.Domain.Models;
using web_services_ielectric.SpareRequests.Resources;

namespace web_services_ielectric.SpareRequests.Controllers;

[ApiController]
[Produces("application/json")]
[Route("/api/v1/[controller]")]
public class SpareRequestsController : ControllerBase
{
    private readonly ISpareRequestService _spareRequestService;
    private readonly IMapper _mapper;

    public SpareRequestsController(ISpareRequestService spareRequestService, IMapper mapper)
    {
        _spareRequestService = spareRequestService;
        _mapper = mapper;
    }

    [SwaggerOperation(
    Summary = "Get all Spare Requests",
    Description = "Get of all Spare Requests",
    OperationId = "GetAllSpareRequests")]
    [SwaggerResponse(200, "All Spare Requests returned", typeof(IEnumerable<SpareRequestResource>))]

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SpareRequestResource>), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IEnumerable<SpareRequestResource>> GetAllAsync()
    {
        var spareRequest = await _spareRequestService.ListAsync();
        var resources = _mapper.Map<IEnumerable<SpareRequest>, IEnumerable<SpareRequestResource>>(spareRequest);
        return resources;
    }


    [SwaggerOperation(
    Summary = "Get Spare Request by Id",
    Description = "Get Spare Request by Id",
    OperationId = "GetSpareRequestById")]
    [SwaggerResponse(200, "Spare Request returned", typeof(SpareRequestResource))]

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SpareRequestResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> GetByIdAsync(long id)
    {
        var result = await _spareRequestService.GetByIdAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var spareRequestResource = _mapper.Map<SpareRequest, SpareRequestResource>(result.Resource);

        return Ok(spareRequestResource);
    }


    [SwaggerOperation(
    Summary = "Save Spare Request",
    Description = "Save  Spare Request",
    OperationId = "Save SpareRequest")]
    [SwaggerResponse(200, " Spare Request saved", typeof(SpareRequestResource))]

    [HttpPost]
    [ProducesResponseType(typeof(SpareRequestResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> PostAsync([FromBody] SaveSpareRequestResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var spareRequest = _mapper.Map<SaveSpareRequestResource, SpareRequest>(resource);
        var result = await _spareRequestService.SaveAsync(spareRequest);

        if (!result.Success)
            return BadRequest(result.Message);

        var spareRequestResource = _mapper.Map<SpareRequest, SpareRequestResource>(result.Resource);

        return Ok(spareRequestResource);
    }

    [SwaggerOperation(
    Summary = "Update Spare Request",
    Description = "Update Spare Request",
    OperationId = "UpdateSpareRequest")]
    [SwaggerResponse(200, "Spare Request updated", typeof(SpareRequestResource))]

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(SpareRequestResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> PutAsync(long id, [FromBody] SaveSpareRequestResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var spareRequest = _mapper.Map<SaveSpareRequestResource, SpareRequest>(resource);
        var result = await _spareRequestService.UpdateAsync(id, spareRequest);

        if (!result.Success)
            return BadRequest(result.Message);

        var spareRequestResource = _mapper.Map<SpareRequest, SpareRequestResource>(result.Resource);
        return Ok(spareRequestResource);
    }

    [SwaggerOperation(
    Summary = "Delete Spare Request",
    Description = "Delete Spare Request",
    OperationId = "DeleteSpareRequest")]
    [SwaggerResponse(200, "Spare Request deleted", typeof(SpareRequestResource))]

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(SpareRequestResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        var result = await _spareRequestService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var spareRequestResource = _mapper.Map<SpareRequest, SpareRequestResource>(result.Resource);

        return Ok(spareRequestResource);
    }
}