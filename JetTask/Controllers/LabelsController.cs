using Microsoft.AspNetCore.Http;
using ExpressGlobalExceptionHandler;
using Microsoft.AspNetCore.Mvc;
using System;
using Example.API.Validators;
using JetTask.Entities.Dtos;
using JetTask.Filters;

namespace JetTask
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelsController : ControllerBase
        {
            public LabelsController()
            {
            }

            [HttpGet]
            [Authorize]
            public IActionResult Get([FromQuery] int id)
            {
                if (LabelValidator.ValidateGet(id).IsSuccess)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(LabelValidator.ValidateGet(id));
                }
            }
        }
    }
