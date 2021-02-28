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
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService projectService;

        public ProjectsController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody] CreateProjectParameters data)
        {
            if (ProjectsValidator.ValidateCreate(data).IsSuccess)
            {
                var response = projectService.CreateProject(data);
                return (response.IsSuccess) ? Ok(response) : BadRequest(response);
            }
            else
            {
                return BadRequest(ProjectsValidator.ValidateCreate(data));
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var response = projectService.GetMyProjects();
            return (response.IsSuccess) ? Ok(response) : BadRequest(response);
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int projectId)
        {
            if (ProjectsValidator.ValidateDelete(projectId).IsSuccess)
            {
                var response = projectService.DeleteProject(projectId);
                return (response.IsSuccess) ? Ok(response) : BadRequest(response);
            }
            else
            {
                return BadRequest(ProjectsValidator.ValidateDelete(projectId));
            }
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody] CreateProjectParameters data, [FromQuery] int projectId)
        {
            if (ProjectsValidator.ValidateCreate(data).IsSuccess)
            {
                var response = projectService.UpdateProject(data, projectId);
                return (response.IsSuccess) ? Ok(response) : BadRequest(response);
            }
            else
            {
                return BadRequest(ProjectsValidator.ValidateCreate(data));
            }
        }
    }
}