using System;
using System.Collections.Generic;
using ExpressGlobalExceptionHandler;
using ExpressMicroPermissions;
using JetTask.Data;
using JetTask.Entities;
using JetTask.Entities.Misc;

namespace JetTask.Service
{
    public interface ISprintService
    {
        Response<List<Sprint>> GetSprintsByProjectId(int projectId);

        Response<List<Sprint>> GetAllSprints();
    }

    public class SprintService : ISprintService
    {
        private readonly AppConfig appConfig;
        private readonly IAuthorityService authorityService;

        public SprintService(AppConfig appConfig, IAuthorityService authorityService)
        {
            this.appConfig = appConfig;
            this.authorityService = authorityService;
        }

        public Response<List<Sprint>> GetSprintsByProjectId(int projectId)
        {
            var loggedInUser = authorityService.GetLoggedInUser();
            var hasPermissions = MicroPermissions.HasAllPermissions(loggedInUser.Id, "View.Sprints");
            if (hasPermissions)
            {
                using (var unitOfWork = new UnitOfWork(new JetTaskContext(appConfig)))
                {
                    var project = unitOfWork.Projects.Get(projectId);
                    if (project != null)
                    {
                        if (project.CreatedById == loggedInUser.Id)
                        {
                            var sprints = unitOfWork.Sprints.GetSprintsByProject(project);
                            return new Response<List<Sprint>>
                            {
                                IsSuccess = true,
                                ResponseStatus = ResponseStatus.SUCCESS,
                                Data = sprints
                            };
                        }
                        else
                        {
                            return new Response<List<Sprint>>
                            {
                                IsSuccess = false,
                                ResponseStatus = ResponseStatus.WARNING,
                                Message = "Access blocked to the project. You can only sprints from projects you created."
                            };
                        }
                    }
                    else
                    {
                        return new Response<List<Sprint>>
                        {
                            IsSuccess = false,
                            ResponseStatus = ResponseStatus.WARNING,
                            Message = "Unable to locate specified project. Verify the projectId."
                        };
                    }
                }
            }
            else
            {
                return new Response<List<Sprint>>
                {
                    IsSuccess = false,
                    ResponseStatus = ResponseStatus.WARNING,
                    Info = new List<string> { "Failed to claim permissions - View.Sprints" },
                    Message = "Operation aborted due to insufficient permissions"
                };
            }
        }

        public Response<List<Sprint>> GetAllSprints()
        {
            var loggedInUser = authorityService.GetLoggedInUser();
            var hasPermissions = MicroPermissions.HasAllPermissions(loggedInUser.Id, "View.Sprints");
            if (hasPermissions)
            {
                using (var unitOfWork = new UnitOfWork(new JetTaskContext(appConfig)))
                {
                    var projects = unitOfWork.Projects.GetProjectsByAuthor(loggedInUser.Id);
                    if (projects != null)
                    {
                        var sprints = unitOfWork.Sprints.GetSprintsByProjects(projects);
                        return new Response<List<Sprint>>
                        {
                            IsSuccess = true,
                            ResponseStatus = ResponseStatus.SUCCESS,
                            Data = sprints
                        };
                    }
                    else
                    {
                        return new Response<List<Sprint>>
                        {
                            IsSuccess = false,
                            ResponseStatus = ResponseStatus.WARNING,
                            Message = "Unable to locate any sprints from your projects."
                        };
                    }
                }
            }
            else
            {
                return new Response<List<Sprint>>
                {
                    IsSuccess = false,
                    ResponseStatus = ResponseStatus.WARNING,
                    Info = new List<string> { "Failed to claim permissions - View.Sprints" },
                    Message = "Operation aborted due to insufficient permissions"
                };
            }
        }
    }
}