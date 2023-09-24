using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using web_services_ielectric.Plans.Domain.Models;
using web_services_ielectric.Plans.Domain.Services;
using web_services_ielectric.Plans.Resources;
using web_services_ielectric.Shared.Extensions;

namespace web_services_ielectric.Plans.Controllers;

[ApiController]
[Produces("application/json")]
[Route("/api/v1/[controller]")]
public class PlansController : ControllerBase
{
    private readonly IPlanService _planService;
    private readonly IMapper _mapper;

    public PlansController(IPlanService planService, IMapper mapper)
    {
        _planService = planService;
        _mapper = mapper;
    }

    [SwaggerOperation(
    Summary = "Get all Plans",
    Description = "Get of all Plans",
    OperationId = "GetAllPlans")]
    [SwaggerResponse(200, "All Plans returned", typeof(IEnumerable<PlanResource>))]

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PlanResource>), 200)]
    public async Task<IEnumerable<PlanResource>> GetAllAsync()
    {
        var plans = await _planService.ListAsync();
        var resources = _mapper
            .Map<IEnumerable<Plan>, IEnumerable<PlanResource>>(plans);
        return resources;
    }

    [SwaggerOperation(
    Summary = "Get Plan by Id",
    Description = "Get Plan by Id",
    OperationId = "GetPlanById")]
    [SwaggerResponse(200, "Plan returned", typeof(PlanResource))]

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PlanResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> GetAsync(int id)
    {
        var result = await _planService.GetByIdAsync(id);
        if (!result.Success)
            return BadRequest(result.Message);
        var planResource = _mapper.Map<Plan, PlanResource>(result.Resource);
        return Ok(planResource);
    }

    [SwaggerOperation(
    Summary = "Save Plan",
    Description = "Save Plan",
    OperationId = "SavePlan")]
    [SwaggerResponse(200, "Plan saved", typeof(PlanResource))]

    [HttpPost]
    [ProducesResponseType(typeof(PlanResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> PostAsync([FromBody] SavePlanResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var plan = _mapper.Map<SavePlanResource, Plan>(resource);
        var result = await _planService.SaveAsync(plan);

        if (!result.Success)
            return BadRequest(result.Message);

        var planResource = _mapper.Map<Plan, PlanResource>(result.Resource);
        return Ok(planResource);

    }

    [SwaggerOperation(
    Summary = "Update Plan",
    Description = "Update Plan",
    OperationId = "UpdatePlan")]
    [SwaggerResponse(200, "Plan updated", typeof(PlanResource))]

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(PlanResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SavePlanResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var plan = _mapper.Map<SavePlanResource, Plan>(resource);
        var result = await _planService.UpdateAsync(id, plan);

        if (!result.Success)
            return BadRequest(result.Message);

        var planResource = _mapper.Map<Plan, PlanResource>(result.Resource);
        return Ok(planResource);

    }

    [SwaggerOperation(
    Summary = "Delete Plan",
    Description = "Delete Plan",
    OperationId = "DeletePlan")]
    [SwaggerResponse(200, "Plan deleted", typeof(PlanResource))]


    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(PlanResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _planService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var planResource = _mapper.Map<Plan, PlanResource>(result.Resource);

        return Ok(planResource);
    }
}