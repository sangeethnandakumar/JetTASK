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
        Response<List<string>> GetAllPermissionGroups();

        Response<List<string>> GetPermissionsOfUser(int userId);
        Response<List<string>> GetPermissionsFromPermissionGroup(string permissionGroupName);

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
                        ResponseStatus = ResponseStatus.SUCCESS
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
                    Message = "You require administrative rights to perform permission related operations"
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
                        ResponseStatus = ResponseStatus.SUCCESS
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
                    Message = "You require administrative rights to perform permission related operations"
                };
            }
        }

        public Response AddPermissionGroup(string permissionGroupName)
        {
            var isUserSuperAdmin = authorityService.GetLoggedInUser().IsSuperAdmin;
            if (isUserSuperAdmin)
            {
                var result = MicroPermissions.AddPermissionGroup(permissionGroupName);
                if (result)
                {
                    return new Response
                    {
                        IsSuccess = true,
                        ResponseStatus = ResponseStatus.SUCCESS,
                    };
                }
                else
                {
                    return new Response
                    {
                        IsSuccess = false,
                        ResponseStatus = ResponseStatus.ERROR,
                        Message = "Failed while creating new permission group",
                        Info = new List<string> { "Check if permission group already exists" }
                    };
                }
            }
            else
            {
                return new Response
                {
                    IsSuccess = false,
                    ResponseStatus = ResponseStatus.ERROR,
                    Message = "You require administrative rights to perform permission related operations"
                };
            }          
        }

        public Response AddPermissionToPermissionGroup(string permissionGroupName, string permissionName)
        {
            var isUserSuperAdmin = authorityService.GetLoggedInUser().IsSuperAdmin;
            if (isUserSuperAdmin)
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
                        ResponseStatus = ResponseStatus.ERROR,
                        Message = "Failed while adding permission to permission group",
                        Info = new List<string> { "Check if the permission group already exist", "Check if the permission already exists", "Check if the permission is already added to this group" }
                    };
                }
            }
            else
            {
                return new Response
                {
                    IsSuccess = false,
                    ResponseStatus = ResponseStatus.ERROR,
                    Message = "You require administrative rights to perform permission related operations"
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
            var isUserSuperAdmin = authorityService.GetLoggedInUser().IsSuperAdmin;
            if (isUserSuperAdmin)
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
                        ResponseStatus = ResponseStatus.ERROR,
                        Message = "Failed while unbinding permission group from user",
                        Info = new List<string> { "Check if permission group exist" }
                    };
                }
            }
            else
            {
                return new Response
                {
                    IsSuccess = false,
                    ResponseStatus = ResponseStatus.ERROR,
                    Message = "You require administrative rights to perform permission related operations"
                };
            }
        }

        public Response<List<string>> GetAllPermissionGroups()
        {
            var isUserSuperAdmin = authorityService.GetLoggedInUser().IsSuperAdmin;
            if (isUserSuperAdmin)
            {
                var result = MicroPermissions.GetAllPermissionGroups();
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
                        ResponseStatus = ResponseStatus.ERROR,
                        Message = "Unable to retrive the list of permission groups"
                    };
                }
            }
            else
            {
                return new Response<List<string>>
                {
                    IsSuccess = false,
                    ResponseStatus = ResponseStatus.ERROR,
                    Message = "You require administrative rights to perform permission related operations"
                };
            }
        }

        public Response<List<string>> GetPermissionsFromPermissionGroup(string permissionGroupName)
        {
            var isUserSuperAdmin = authorityService.GetLoggedInUser().IsSuperAdmin;
            if (isUserSuperAdmin)
            {
                var result = MicroPermissions.GetPermissionsFromPermissionGroup(permissionGroupName);
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
                        ResponseStatus = ResponseStatus.ERROR,
                        Message = "Unable to retrive permissions from permission group"
                    };
                }
            }
            else
            {
                return new Response<List<string>>
                {
                    IsSuccess = false,
                    ResponseStatus = ResponseStatus.ERROR,
                    Message = "You require administrative rights to perform permission related operations"
                };
            }
        }
    }
}