using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using web_services_ielectric.Reports.Domain.Models;
using web_services_ielectric.Reports.Domain.Services;
using web_services_ielectric.Reports.Resources;
using web_services_ielectric.Shared.Extensions;

namespace web_services_ielectric.Reports.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/v1/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;
    private readonly IMapper _mapper;

    public ReportsController(IReportService reportService, IMapper mapper)
    {
        _reportService = reportService;
        _mapper = mapper;
    }

    [SwaggerOperation(
    Summary = "Get all Reports",
    Description = "Get of all Reports",
    OperationId = "GetAlReports")]
    [SwaggerResponse(200, "All Reports returned", typeof(IEnumerable<ReportResource>))]


    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ReportResource>), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IEnumerable<ReportResource>> GetAllAsync()
    {
        var reports = await _reportService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Report>, IEnumerable<ReportResource>>(reports);
        return resources;
    }


    [SwaggerOperation(
    Summary = "Get Report by Id",
    Description = "Get Report by Id",
    OperationId = "GetReportById")]
    [SwaggerResponse(200, "Report returned", typeof(ReportResource))]

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ReportResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> GetByIdAsync(long id)
    {
        var result = await _reportService.GetByIdAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var reportResource = _mapper.Map<Report, ReportResource>(result.Resource);

        return Ok(reportResource);
    }

    [SwaggerOperation(
    Summary = "Save Report",
    Description = "Save Report",
    OperationId = "SaveReport")]
    [SwaggerResponse(200, "Report saved", typeof(ReportResource))]

    [HttpPost]
    [ProducesResponseType(typeof(ReportResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> PostAsync([FromBody] SaveReportResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var report = _mapper.Map<SaveReportResource, Report>(resource);
        var result = await _reportService.SaveAsync(report);

        if (!result.Success)
            return BadRequest(result.Message);

        var reportResource = _mapper.Map<Report, ReportResource>(result.Resource);

        return Ok(reportResource);
    }

    [SwaggerOperation(
    Summary = "Update Report",
    Description = "Update Report",
    OperationId = "UpdateReport")]
    [SwaggerResponse(200, "Report updated", typeof(ReportResource))]

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ReportResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> PutAsync(long id, [FromBody] SaveReportResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var report = _mapper.Map<SaveReportResource, Report>(resource);
        var result = await _reportService.UpdateAsync(id, report);

        if (!result.Success)
            return BadRequest(result.Message);

        var reportResource = _mapper.Map<Report, ReportResource>(result.Resource);
        return Ok(reportResource);
    }

    [SwaggerOperation(
    Summary = "Delete Report",
    Description = "Delete Report",
    OperationId = "DeleteReport")]
    [SwaggerResponse(200, "Report deleted", typeof(ReportResource))]

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ReportResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        var result = await _reportService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var reportResource = _mapper.Map<Report, ReportResource>(result.Resource);

        return Ok(reportResource);
    }
}