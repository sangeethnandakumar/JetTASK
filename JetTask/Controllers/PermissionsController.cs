using JetTask.Filters;
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
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService permissionService;

        public PermissionsController(IPermissionService permissionService)
        {
            this.permissionService = permissionService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var response = permissionService.GetAllPermissions();
            return (response.IsSuccess) ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            var response = permissionService.GetPermissionsOfUser(id);
            return (response.IsSuccess) ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        [Route("BindPermission")]
        [Authorize]
        public IActionResult BindPermission([FromForm]int userId, [FromForm] string permissionName)
        {
            var response = permissionService.BindPermissionToUser(userId, permissionName);
            return (response.IsSuccess) ? Ok(response) : BadRequest(response);
        }

        [HttpDelete]
        [Route("UnBindPermission")]
        [Authorize]
        public IActionResult UnBindPermission(int userId, string permissionName)
        {
            var response = permissionService.UnBindPermissionFromUser(userId, permissionName);
            return (response.IsSuccess) ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        [Route("BindPermissionGroup")]
        [Authorize]
        public IActionResult BindPermissionGroup(int userId, string permissionName)
        {
            var response = permissionService.BindPermissionGroupToUser(userId, permissionName);
            return (response.IsSuccess) ? Ok(response) : BadRequest(response);
        }

        [HttpDelete]
        [Route("UnBindPermissionGroup")]
        [Authorize]
        public IActionResult UnBindPermissionGroup(int userId, string permissionName)
        {
            var response = permissionService.UnBindPermissionFromUser(userId, permissionName);
            return (response.IsSuccess) ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        [Route("AddGroup")]
        [Authorize]
        public IActionResult AddGroup(string permissionGroupName)
        {
            var response = permissionService.AddPermissionGroup(permissionGroupName);
            return (response.IsSuccess) ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        [Route("AddPermissionToGroup")]
        [Authorize]
        public IActionResult AddPermissionToGroup(string permissionGroupName, string permissionName)
        {
            var response = permissionService.AddPermissionToPermissionGroup(permissionGroupName, permissionName);
            return (response.IsSuccess) ? Ok(response) : BadRequest(response);
        }
    }
}