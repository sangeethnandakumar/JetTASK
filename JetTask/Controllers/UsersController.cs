using JetTask.Models;
using JetTask.Service;
using JetTask.Validators;
using Microsoft.AspNetCore.Mvc;

namespace JetTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthorityService authService;

        public UsersController(IAuthorityService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        [Route("LogIn")]
        public IActionResult LogIn([FromBody] LoginParameters data)
        {
            if (UsersValidator.ValidateLogin(data).IsSuccess)
            {
                var response = authService.Login(data.Username, data.Password);
                return (response.IsSuccess) ? Ok(response) : BadRequest(response);
            }
            else
            {
                return BadRequest(UsersValidator.ValidateLogin(data));
            }
        }
    }
}