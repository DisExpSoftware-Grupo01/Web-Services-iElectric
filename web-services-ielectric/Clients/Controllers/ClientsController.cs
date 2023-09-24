using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using web_services_ielectric.Clients.Domain.Services.Communication;
using Swashbuckle.AspNetCore.Annotations;
using web_services_ielectric.Clients.Domain.Models;
using web_services_ielectric.Clients.Resources;
using web_services_ielectric.Shared.Extensions;

namespace web_services_ielectric.Clients.Controllers;

[ApiController]
    [Produces("application/json")]
    [Route("/api/v1/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public ClientsController(IClientService clientService, IMapper mapper)
        {
            _clientService = clientService;
            _mapper = mapper;
        }

        [SwaggerOperation(
        Summary = "Get all Clients",
        Description = "Get of all Clients",
        OperationId = "GetAllClients")]
        [SwaggerResponse(200, "All Clients returned", typeof(IEnumerable<ClientResource>))]

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClientResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<ClientResource>> GetAllAsync()
        {
            var clients = await _clientService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Client>, IEnumerable<ClientResource>>(clients);
            return resources;
        }

        [SwaggerOperation(
        Summary = "Get Client by Id",
        Description = "Get Client by Id",
        OperationId = "GetClientById")]
        [SwaggerResponse(200, "Client returned", typeof(ClientResource))]

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClientResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _clientService.GetByIdAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var clientResult = _mapper.Map<Client, ClientResource>(result.Resource);

            return Ok(clientResult);
        }

        [SwaggerOperation(
        Summary = "Get Client by User Id",
        Description = "Get Client by User Id",
        OperationId = "GetClientByUserId")]
        [SwaggerResponse(200, "Client returned", typeof(ClientResource))]

        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(ClientResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetByUserIdAsync(long userId)
        {
            var result = await _clientService.GetByUserIdAsync(userId);

            if (!result.Success)
                return BadRequest(result.Message);

            var clientResult = _mapper.Map<Client, ClientResource>(result.Resource);

            return Ok(clientResult);
        }

        [SwaggerOperation(
        Summary = "Save Client",
        Description = "Save Client",
        OperationId = "SaveClient")]
        [SwaggerResponse(200, "Client saved", typeof(ClientResource))]

        [HttpPost]
        [ProducesResponseType(typeof(ClientResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveClientResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var client = _mapper.Map<SaveClientResource, Client>(resource);
            var result = await _clientService.SaveAsync(client);

            if (!result.Success)
                return BadRequest(result.Message);

            var clientResource = _mapper.Map<Client, ClientResource>(result.Resource);

            return Ok(clientResource);
        }

        [SwaggerOperation(
        Summary = "Update Client",
        Description = "Update Client",
        OperationId = "UpdateClient")]
        [SwaggerResponse(200, "Client updated", typeof(ClientResource))]

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ClientResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(long id, [FromBody] SaveClientResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var client = _mapper.Map<SaveClientResource, Client>(resource);
            var result = await _clientService.UpdateAsync(id, client);

            if (!result.Success)
                return BadRequest(result.Message);

            var clientResource = _mapper.Map<Client, ClientResource>(result.Resource);
            return Ok(clientResource);
        }

        [SwaggerOperation(
        Summary = "Delete Client",
        Description = "Delete Client",
        OperationId = "DeleteClient")]
        [SwaggerResponse(200, "Client deleted", typeof(ClientResource))]

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ClientResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _clientService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var clientResource = _mapper.Map<Client, ClientResource>(result.Resource);

            return Ok(clientResource);
        } 

        [SwaggerOperation(
            Summary = "Update Plan Client",
            Description = "Update Plan Client",
            OperationId = "UpdatePlanClient")]

        [HttpPut("{clientId}/plans/{planId}")]
        [ProducesResponseType(typeof(ClientResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> UpdateUserPlan(long clientId, long planId)
        {
            var result = await _clientService.UpdateUserPlanAsync(clientId, planId);

            if (!result.Success)
                return BadRequest(result.Message);

            var ClientResource = _mapper.Map<Client, ClientResource>(result.Resource);

            return Ok(ClientResource);
        }
    }