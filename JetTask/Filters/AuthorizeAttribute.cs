using ExpressGlobalExceptionHandler;
using JetTask.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JetTask.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (User)context.HttpContext.Items["User"];
            if (user == null)
            {
                context.Result = new JsonResult(new Response {
                    IsSuccess = false,
                    ResponseStatus = ResponseStatus.ERROR,
                    Message="Request terminated. Unauthorized access to protected resource.", 
                    Info=new List<string>() { 
                        "Verify the auth token sending through this request",
                        "Verify if your token is invalid or expired", 
                        "Request for a new token by logging in again" } 
                    })
                { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}