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
    public class CommentsController : ControllerBase
        {
            public CommentsController()
            {
            }

            [HttpGet]
            [Authorize]
            public IActionResult Get([FromQuery] int id)
            {
                if (CommentValidator.ValidateGet(id).IsSuccess)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(CommentValidator.ValidateGet(id));
                }
            }
        }
    }
