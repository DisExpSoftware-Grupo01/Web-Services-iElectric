using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using web_services_ielectric.Clients.Resources;
using web_services_ielectric.Reports.Domain.Models;
using web_services_ielectric.Reports.Resources;
using web_services_ielectric.Shared.Extensions;
using web_services_ielectric.Technicians.Domain.Models;
using web_services_ielectric.Technicians.Domain.Services;
using web_services_ielectric.Technicians.Resources;

namespace web_services_ielectric.Technicians.Controllers;

[ApiController]
[Produces("application/json")]
[Route("/api/v1/[controller]")]
public class TechnicianController : ControllerBase
{
    private readonly ITechnicianService _technicianService;
    private readonly IMapper _mapper;

    public TechnicianController(ITechnicianService technicianService, IMapper mapper)
    {
        _technicianService = technicianService;
        _mapper = mapper;
    }

    [SwaggerOperation(
        Summary = "Get all Technicians",
        Description = "Get of all Technicians",
        OperationId = "GetAllTechnicians")]
    [SwaggerResponse(200, "All Technicians returned", typeof(IEnumerable<TechnicianResource>))]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TechnicianResource>), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IEnumerable<TechnicianResource>> GetAllAsync()
    {
        var technicians = await _technicianService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Technician>, IEnumerable<TechnicianResource>>(technicians);
        return resources;
    }

    [SwaggerOperation(
        Summary = "Get Technicians by Id",
        Description = "Get Technicians by Id",
        OperationId = "GetTechniciansById")]
    [SwaggerResponse(200, "Technicians returned", typeof(TechnicianResource))]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TechnicianResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> GetByIdAsyn(long id)
    {
        var result = await _technicianService.GetByIdAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var technicianResult = _mapper.Map<Technician, TechnicianResource>(result.Resource);

        return Ok(technicianResult);
    }

    [SwaggerOperation(
        Summary = "Get Technician by User Id",
        Description = "Get Technician by User Id",
        OperationId = "GetTechnicianByUserId")]
    [SwaggerResponse(200, "Technician returned", typeof(TechnicianResource))]
    [HttpGet("user/{userId}")]
    [ProducesResponseType(typeof(TechnicianResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> GetByUserIdAsyn(long userId)
    {
        var result = await _technicianService.GetByUserIdAsync(userId);

        if (!result.Success)
            return BadRequest(result.Message);

        var technicianResult = _mapper.Map<Technician, TechnicianResource>(result.Resource);

        return Ok(technicianResult);
    }

    [SwaggerOperation(
        Summary = "Save Technician",
        Description = "Save Technician",
        OperationId = "SaveTechnician")]
    [SwaggerResponse(200, "Technician saved", typeof(TechnicianResource))]
    [HttpPost]
    [ProducesResponseType(typeof(TechnicianResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> PostAsync([FromBody] SaveTechnicianResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var technician = _mapper.Map<SaveTechnicianResource, Technician>(resource);
        var result = await _technicianService.SaveAsync(technician);

        if (!result.Success)
            return BadRequest(result.Message);

        var technicianResource = _mapper.Map<Technician, TechnicianResource>(result.Resource);

        return Ok(technicianResource);
    }

    [SwaggerOperation(
        Summary = "Update Technician",
        Description = "Update Technician",
        OperationId = "UpdateTechnician")]
    [SwaggerResponse(200, "Technician updated", typeof(TechnicianResource))]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(TechnicianResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> PutAsync(long id, [FromBody] SaveTechnicianResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var technician = _mapper.Map<SaveTechnicianResource, Technician>(resource);
        var result = await _technicianService.UpdateAsync(id, technician);

        if (!result.Success)
            return BadRequest(result.Message);

        var technicianResource = _mapper.Map<Technician, TechnicianResource>(result.Resource);

        return Ok(technicianResource);
    }

    [SwaggerOperation(
        Summary = "Delete Technician",
        Description = "Delete Technician",
        OperationId = "DeleteTechnician")]
    [SwaggerResponse(200, "Technician deleted", typeof(TechnicianResource))]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(TechnicianResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        var result = await _technicianService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var technicianResource = _mapper.Map<Technician, TechnicianResource>(result.Resource);

        return Ok(technicianResource);
    }

    [SwaggerOperation(
        Summary = "Get Reports Technician",
        Description = "Get Reports Technician",
        OperationId = "GetReportsTechnician",
        Tags = new[] { "Reports" })]
    [HttpGet("{technicianId}/reports/")]
    [ProducesResponseType(typeof(ClientResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IEnumerable<ReportResource>> GetAllByTechnicianIdAsync(int technicianId)
    {
        var reports = await _technicianService.ListByTechnicianIdAsync(technicianId);
        var resources = _mapper.Map<IEnumerable<Report>, IEnumerable<ReportResource>>(reports);
        return resources;
    }
}