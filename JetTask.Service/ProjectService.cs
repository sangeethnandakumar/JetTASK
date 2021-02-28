using ExpressGlobalExceptionHandler;
using ExpressMicroPermissions;
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
    public interface IProjectService
    {
        public Response CreateProject(CreateProjectParameters data);

        public Response UpdateProject(CreateProjectParameters data, int projectId);

        public Response DeleteProject(int projectId);

        public Response<List<Project>> GetMyProjects();
    }

    public class ProjectService : IProjectService
    {
        private readonly AppConfig appConfig;
        private readonly IAuthorityService authorityService;

        public ProjectService(AppConfig appConfig, IAuthorityService authorityService)
        {
            this.appConfig = appConfig;
            this.authorityService = authorityService;
        }

        public Response CreateProject(CreateProjectParameters data)
        {
            var loggedInUser = authorityService.GetLoggedInUser();
            var hasPermissions = MicroPermissions.HasAllPermissions(loggedInUser.Id, "Add.Project");
            if (hasPermissions)
            {
                using (var unitOfWork = new UnitOfWork(new JetTaskContext(appConfig)))
                {
                    var existingProject = unitOfWork.Projects.GetProjectByName(data.Name);
                    if (existingProject == null)
                    {
                        var existingClient = unitOfWork.Clients.Get(data.ClientId);
                        if (existingClient != null)
                        {
                            unitOfWork.Projects.Add(new Project
                            {
                                Name = data.Name,
                                Description = data.Description,
                                IsActive = true,
                                CreatedOn = DateTime.UtcNow,
                                CreatedById = loggedInUser.Id,
                                Prefix = data.Prefix,
                                Client = existingClient
                            });
                            unitOfWork.Complete();
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
                                ResponseStatus = ResponseStatus.WARNING,
                                Info = new List<string> {
                                        "The client selected for this project is invalid"
                                    },
                                Message = "Operation aborted due to conflict"
                            };
                        }
                    }
                    else
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            ResponseStatus = ResponseStatus.WARNING,
                            Info = new List<string> {
                                    "A project with the same name already exist, Please choose a different projectname"
                                },
                            Message = "Operation aborted due to conflict"
                        };
                    }
                }
            }
            else
            {
                return new Response
                {
                    IsSuccess = false,
                    ResponseStatus = ResponseStatus.WARNING,
                    Info = new List<string> { "Failed to claim permissions - Add.Project" },
                    Message = "Operation aborted due to insufficient permissions"
                };
            }
        }

        public Response UpdateProject(CreateProjectParameters data, int projectId)
        {
            var loggedInUser = authorityService.GetLoggedInUser();
            var hasPermissions = MicroPermissions.HasAllPermissions(loggedInUser.Id, "Update.Project");
            if (hasPermissions)
            {
                using (var unitOfWork = new UnitOfWork(new JetTaskContext(appConfig)))
                {
                    var existingProject = unitOfWork.Projects.Get(projectId);
                    if (existingProject != null)
                    {
                        var existingClient = unitOfWork.Clients.Get(data.ClientId);
                        if (existingClient != null)
                        {
                            existingProject.Name = data.Name;
                            existingProject.Prefix = data.Prefix;
                            existingProject.Client = existingClient;
                            existingProject.Description = data.Description;
                            existingProject.UpdatedOn = DateTime.UtcNow;
                            existingProject.UpdatedById = loggedInUser.Id;
                            unitOfWork.Projects.Update(existingProject);
                            unitOfWork.Complete();
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
                                ResponseStatus = ResponseStatus.WARNING,
                                Info = new List<string> {
                                        "The client selected for this project is invalid"
                                    },
                                Message = "Operation aborted due to conflict"
                            };
                        }
                    }
                    else
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            ResponseStatus = ResponseStatus.WARNING,
                            Info = new List<string> {
                                    "Unable to determine the project to update, Please verify projectId"
                                },
                            Message = "Operation aborted due to conflict"
                        };
                    }
                }
            }
            else
            {
                return new Response
                {
                    IsSuccess = false,
                    ResponseStatus = ResponseStatus.WARNING,
                    Info = new List<string> { "Failed to claim permissions - Update.Project" },
                    Message = "Operation aborted due to insufficient permissions"
                };
            }
        }

        public Response DeleteProject(int projectId)
        {
            var loggedInUser = authorityService.GetLoggedInUser();
            var hasPermissions = MicroPermissions.HasAllPermissions(loggedInUser.Id, "Delete.Project");
            if (hasPermissions)
            {
                using (var unitOfWork = new UnitOfWork(new JetTaskContext(appConfig)))
                {
                    var existingProject = unitOfWork.Projects.Get(projectId);
                    if (existingProject != null)
                    {
                        if (existingProject.CreatedById == loggedInUser.Id)
                        {
                            unitOfWork.Projects.Remove(existingProject);
                            unitOfWork.Complete();
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
                                ResponseStatus = ResponseStatus.WARNING,
                                Info = new List<string> { "You can only delete projects created by you" },
                                Message = "Operation aborted due to insufficient permissions"
                            };
                        }
                    }
                    else
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            ResponseStatus = ResponseStatus.WARNING,
                            Info = new List<string> {
                                    "Unable to determine the project to delete, Please verify projectId"
                                },
                            Message = "Operation aborted due to conflict"
                        };
                    }
                }
            }
            else
            {
                return new Response
                {
                    IsSuccess = false,
                    ResponseStatus = ResponseStatus.WARNING,
                    Info = new List<string> { "Failed to claim permissions - Delete.Project" },
                    Message = "Operation aborted due to insufficient permissions"
                };
            }
        }

        public Response<List<Project>> GetMyProjects()
        {
            var loggedInUser = authorityService.GetLoggedInUser();
            var hasPermissions = MicroPermissions.HasAllPermissions(loggedInUser.Id, "View.Projects");
            if (hasPermissions)
            {
                using (var unitOfWork = new UnitOfWork(new JetTaskContext(appConfig)))
                {
                    var projects = unitOfWork.Projects.GetProjectsByAuthor(loggedInUser.Id);
                    return new Response<List<Project>>
                    {
                        IsSuccess = true,
                        ResponseStatus = ResponseStatus.SUCCESS,
                        Data = projects
                    };
                }
            }
            else
            {
                return new Response<List<Project>>
                {
                    IsSuccess = false,
                    ResponseStatus = ResponseStatus.WARNING,
                    Info = new List<string> { "Failed to claim permissions - View.Projects" },
                    Message = "Operation aborted due to insufficient permissions"
                };
            }
        }
    }
}