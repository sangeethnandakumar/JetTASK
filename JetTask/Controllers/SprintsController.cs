using Microsoft.AspNetCore.Http;
using ExpressGlobalExceptionHandler;
using Microsoft.AspNetCore.Mvc;
using System;
using Example.API.Validators;
using JetTask.Entities.Dtos;
using JetTask.Filters;
using JetTask.Service;
using JetTask.Entities.Dtos.Request;

namespace JetTask
{
    [Route("api/[controller]")]
    [ApiController]
    public class SprintsController : ControllerBase
    {
        private readonly ISprintService sprintService;

        public SprintsController(ISprintService sprintService)
        {
            this.sprintService = sprintService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var response = sprintService.GetAllSprints();
            return (response.IsSuccess) ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        [Authorize]
        [Route("ByProject")]
        public IActionResult GetSprintsFromProjects(int projectId)
        {
            var response = sprintService.GetSprintsByProjectId(projectId);
            return (response.IsSuccess) ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody] CreateSprintParameters data)
        {
            if (SprintValidator.ValidateCreateSprint(data).IsSuccess)
            {
                return Ok();
            }
            else
            {
                return BadRequest(SprintValidator.ValidateCreateSprint(data));
            }
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromQuery] int id)
        {
            if (SprintValidator.ValidateGet(id).IsSuccess)
            {
                return Ok();
            }
            else
            {
                return BadRequest(SprintValidator.ValidateGet(id));
            }
        }
    }
}