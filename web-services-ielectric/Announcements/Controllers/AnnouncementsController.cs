using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using web_services_ielectric.Announcements.Domain.Services;
using Swashbuckle.AspNetCore.Annotations;
using web_services_ielectric.Announcements.Domain.Models;
using web_services_ielectric.Announcements.Resources;
using web_services_ielectric.Shared.Extensions;

namespace web_services_ielectric.Announcements.Controllers;

[ApiController]
[Produces("application/json")]
[Route("/api/v1/[controller]")]
public class AnnouncementsController : ControllerBase
{
    private readonly IAnnouncementService _announcementService;
    private readonly IMapper _mapper;

    public AnnouncementsController(IAnnouncementService announcement, IMapper mapper)
    {
        _announcementService = announcement;
        _mapper = mapper;
    }

    [SwaggerOperation(
    Summary = "Get all Announcements",
    Description = "Get of all Announcements",
    OperationId = "GetAllAnnouncements")]
    [SwaggerResponse(200, "All Announcements returned", typeof(IEnumerable<AnnouncementResource>))]

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AnnouncementResource>), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IEnumerable<AnnouncementResource>> GetAllAsync()
    {
        var announcementes = await _announcementService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Announcement>, IEnumerable<AnnouncementResource>>(announcementes);
        return resources;
    }

    [SwaggerOperation(
    Summary = "Get Announcement by Id",
    Description = "Get Announcement by Id",
    OperationId = "GetAnnouncementById")]
    [SwaggerResponse(200, "Announcement returned", typeof(AnnouncementResource))]

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AnnouncementResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> GetByIdAsync(long id)
    {
        var result = await _announcementService.GetByIdAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var announcementResult = _mapper.Map<Announcement, AnnouncementResource>(result.Resource);

        return Ok(announcementResult);
    }

    [SwaggerOperation(
    Summary = "Save Announcement",
    Description = "Save Announcement",
    OperationId = "SaveAnnouncement")]
    [SwaggerResponse(200, "Announcement saved", typeof(AnnouncementResource))]

    [HttpPost]
    [ProducesResponseType(typeof(AnnouncementResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> PostAsync([FromBody] SaveAnnouncementResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var announcement = _mapper.Map<SaveAnnouncementResource, Announcement>(resource);
        var result = await _announcementService.SaveAsync(announcement);

        if (!result.Success)
            return BadRequest(result.Message);

        var announcementResource = _mapper.Map<Announcement, AnnouncementResource>(result.Resource);

        return Ok(announcementResource);
    }

    [SwaggerOperation(
    Summary = "Update Announcement",
    Description = "Update Announcement",
    OperationId = "UpdateAnnouncement")]
    [SwaggerResponse(200, "Announcement updated", typeof(AnnouncementResource))]

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AnnouncementResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> PutAsync(long id, [FromBody] SaveAnnouncementResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var announcement = _mapper.Map<SaveAnnouncementResource, Announcement>(resource);
        var result = await _announcementService.UpdateAsync(id, announcement);

        if (!result.Success)
            return BadRequest(result.Message);

        var announcementResource = _mapper.Map<Announcement, AnnouncementResource>(result.Resource);

        return Ok(announcementResource);
    }

    [SwaggerOperation(
    Summary = "Delete Announcement",
    Description = "Delete Announcement",
    OperationId = "DeleteAnnouncement")]
    [SwaggerResponse(200, "Announcement deleted", typeof(AnnouncementResource))]

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(AnnouncementResource), 200)]
    [ProducesResponseType(typeof(BadRequestResult), 404)]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        var result = await _announcementService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var announcementResource = _mapper.Map<Announcement, AnnouncementResource>(result.Resource);

        return Ok(announcementResource);
    }
}