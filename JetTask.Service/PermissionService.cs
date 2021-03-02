using ExpressGlobalExceptionHandler;
using ExpressMicroPermissions;
using ExpressMicroPermissions.Models;
using JetTask.Data;
using JetTask.Entities;
using JetTask.Entities.Dtos.Request;
using JetTask.Entities.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetTask.Service
{
    public interface IPermissionService
    {
        Response<List<Permission>> GetAllPermissions();

        Response<List<string>> GetPermissionsOfUser(int userId);

        Response BindPermissionToUser(int userId, string permissionName);

        Response UnBindPermissionFromUser(int userId, string permissionName);

        Response AddPermissionGroup(string permissionGroupName);

        Response AddPermissionToPermissionGroup(string permissionGroupName, string permissionName);

        Response BindPermissionGroupToUser(int userId, string permissionGroupName);

        Response UnBindPermissionGroupFromUser(int userId, string permissionGroupName);
    }

    public class PermissionService : IPermissionService
    {
        private readonly AppConfig appConfig;
        private readonly IAuthorityService authorityService;

        public PermissionService(AppConfig appConfig, IAuthorityService authorityService)
        {
            this.appConfig = appConfig;
            this.authorityService = authorityService;
        }

        public Response<List<Permission>> GetAllPermissions()
        {
            var result = MicroPermissions.GetAllPermissions();
            if (result != null)
            {
                return new Response<List<Permission>>
                {
                    IsSuccess = true,
                    ResponseStatus = ResponseStatus.SUCCESS,
                    Data = result
                };
            }
            else
            {
                return new Response<List<Permission>>
                {
                    IsSuccess = false,
                    ResponseStatus = ResponseStatus.WARNING
                };
            }
        }

        public Response<List<string>> GetPermissionsOfUser(int userId)
        {
            var result = MicroPermissions.GetPermissionsOfUser(userId);
            if (result != null)
            {
                return new Response<List<string>>
                {
                    IsSuccess = true,
                    ResponseStatus = ResponseStatus.SUCCESS,
                    Data = result
                };
            }
            else
            {
                return new Response<List<string>>
                {
                    IsSuccess = false,
                    ResponseStatus = ResponseStatus.WARNING
                };
            }
        }

        public Response BindPermissionToUser(int userId, string permissionName)
        {
            var isUserSuperAdmin = authorityService.GetLoggedInUser().IsSuperAdmin;
            if(isUserSuperAdmin)
            {
                var result = MicroPermissions.BindPermissionToUser(userId, permissionName);
                if (result)
                {
                    return new Response
                    {
                        IsSuccess = true,
                        ResponseStatus = ResponseStatus.SUCCESS,
                        Message = "Successfully binded permission to user"
                    };
                }
                else
                {
                    return new Response
                    {
                        IsSuccess = false,
                        ResponseStatus = ResponseStatus.ERROR,
                        Message = "Failed to bind permission to user"
                    };
                }
            }
            else
            {
                return new Response
                {
                    IsSuccess = false,
                    ResponseStatus = ResponseStatus.ERROR,
                    Message = "You requires administrative rights to perform permission related operations"
                };
            }
        }

        public Response UnBindPermissionFromUser(int userId, string permissionName)
        {
            var isUserSuperAdmin = authorityService.GetLoggedInUser().IsSuperAdmin;
            if (isUserSuperAdmin)
            {
                var result = MicroPermissions.UnBindPermissionFromUser(userId, permissionName);
                if (result)
                {
                    return new Response
                    {
                        IsSuccess = true,
                        ResponseStatus = ResponseStatus.SUCCESS,
                        Message = "Successfully unbinded permission from user"
                    };
                }
                else
                {
                    return new Response
                    {
                        IsSuccess = false,
                        ResponseStatus = ResponseStatus.ERROR,
                        Message = "Failed to unbind permission from user"
                    };
                }
            }
            else
            {
                return new Response
                {
                    IsSuccess = false,
                    ResponseStatus = ResponseStatus.ERROR,
                    Message = "You requires administrative rights to perform permission related operations"
                };
            }
        }

        public Response AddPermissionGroup(string permissionGroupName)
        {
            var result = MicroPermissions.AddPermissionGroup(permissionGroupName);
            if (result)
            {
                return new Response
                {
                    IsSuccess = true,
                    ResponseStatus = ResponseStatus.SUCCESS
                };
            }
            else
            {
                return new Response
                {
                    IsSuccess = false,
                    ResponseStatus = ResponseStatus.ERROR
                };
            }
        }

        public Response AddPermissionToPermissionGroup(string permissionGroupName, string permissionName)
        {
            var result = MicroPermissions.AddPermissionToPermissionGroup(permissionGroupName, permissionName);
            if (result)
            {
                return new Response
                {
                    IsSuccess = true,
                    ResponseStatus = ResponseStatus.SUCCESS
                };
            }
            else
            {
                return new Response
                {
                    IsSuccess = false,
                    ResponseStatus = ResponseStatus.ERROR
                };
            }
        }

        public Response BindPermissionGroupToUser(int userId, string permissionGroupName)
        {
            var result = MicroPermissions.BindPermissionGroupToUser(userId, permissionGroupName);
            if (result)
            {
                return new Response
                {
                    IsSuccess = true,
                    ResponseStatus = ResponseStatus.SUCCESS
                };
            }
            else
            {
                return new Response
                {
                    IsSuccess = false,
                    ResponseStatus = ResponseStatus.ERROR
                };
            }
        }

        public Response UnBindPermissionGroupFromUser(int userId, string permissionGroupName)
        {
            var result = MicroPermissions.UnBindPermissionGroupFromUser(userId, permissionGroupName);
            if (result)
            {
                return new Response
                {
                    IsSuccess = true,
                    ResponseStatus = ResponseStatus.SUCCESS
                };
            }
            else
            {
                return new Response
                {
                    IsSuccess = false,
                    ResponseStatus = ResponseStatus.ERROR
                };
            }
        }
    }
}