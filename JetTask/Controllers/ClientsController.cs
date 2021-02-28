using JetTask.Entities.Dtos.Request;
using JetTask.Filters;
using JetTask.Models;
using JetTask.Service;
using JetTask.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JetTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService clientService;

        public ClientsController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody] CreateClientParameters data)
        {
            if (ClientsValidator.ValidateCreate(data).IsSuccess)
            {
                var response = clientService.CreateClient(data);
                return (response.IsSuccess) ? Ok(response) : BadRequest(response);
            }
            else
            {
                return BadRequest(ClientsValidator.ValidateCreate(data));
            }
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody] CreateClientParameters data, [FromQuery] int clientId)
        {
            if (ClientsValidator.ValidateCreate(data).IsSuccess)
            {
                var response = clientService.UpdateClient(data, clientId);
                return (response.IsSuccess) ? Ok(response) : BadRequest(response);
            }
            else
            {
                return BadRequest(ClientsValidator.ValidateCreate(data));
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var response = clientService.GetMyClients();
            return (response.IsSuccess) ? Ok(response) : BadRequest(response);
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int clientId)
        {
            if (ClientsValidator.ValidateDelete(clientId).IsSuccess)
            {
                var response = clientService.DeleteClient(clientId);
                return (response.IsSuccess) ? Ok(response) : BadRequest(response);
            }
            else
            {
                return BadRequest(ClientsValidator.ValidateDelete(clientId));
            }
        }
    }
}