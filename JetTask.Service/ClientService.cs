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
    public interface IClientService
    {
        public Response<Client> GetClientById(int clientId);

        public Response<Client> CreateClient(CreateClientParameters data);

        public Response<Client> UpdateClient(CreateClientParameters data, int clientId);

        public Response<List<Client>> GetMyClients();

        public Response DeleteClient(int clientId);
    }

    public class ClientService : IClientService
    {
        private readonly AppConfig appConfig;
        private readonly IAuthorityService authorityService;

        public ClientService(AppConfig appConfig, IAuthorityService authorityService)
        {
            this.appConfig = appConfig;
            this.authorityService = authorityService;
        }

        public Response<Client> CreateClient(CreateClientParameters data)
        {
            var loggedInUser = authorityService.GetLoggedInUser();
            var hasPermissions = MicroPermissions.HasAllPermissions(loggedInUser.Id, "Add.Client");
            if (hasPermissions)
            {
                using (var unitOfWork = new UnitOfWork(new JetTaskContext(appConfig)))
                {
                    var existingClient = unitOfWork.Clients.GetClientByCompanyNameAndExecutive(data);
                    if (existingClient == null)
                    {
                        unitOfWork.Clients.Add(new Client
                        {
                            Executive = data.Executive,
                            Company = data.Company,
                            Address = data.Address,
                            IsActive = true,
                            CreatedOn = DateTime.UtcNow,
                            CreatedById = loggedInUser.Id,
                            Email = data.Email,
                            Phone = data.Phone
                        });
                        unitOfWork.Complete();
                        return new Response<Client>
                        {
                            IsSuccess = true,
                            ResponseStatus = ResponseStatus.SUCCESS,
                            Message = "Successfully created new client",
                            Data = unitOfWork.Clients.GetClientByCompanyNameAndExecutive(data)
                        };
                    }
                    else
                    {
                        return new Response<Client>
                        {
                            IsSuccess = false,
                            ResponseStatus = ResponseStatus.ERROR,
                            Info = new List<string> { "A client with same company or executive already exist" },
                            Message = "Operation aborted due to conflict"
                        };
                    }
                }
            }
            else
            {
                return new Response<Client>
                {
                    IsSuccess = false,
                    ResponseStatus = ResponseStatus.WARNING,
                    Info = new List<string> {
                            "Failed to claim permissions - Add.Client"
                            },
                    Message = "Operation aborted due to insufficient permissions"
                };
            }
        }

        public Response<Client> UpdateClient(CreateClientParameters data, int clientId)
        {
            var loggedInUser = authorityService.GetLoggedInUser();
            var hasPermissions = MicroPermissions.HasAllPermissions(loggedInUser.Id, "Update.Client");
            if (hasPermissions)
            {
                using (var unitOfWork = new UnitOfWork(new JetTaskContext(appConfig)))
                {
                    var existingClient = unitOfWork.Clients.GetClientByCompanyNameAndExecutive(data);
                    if (existingClient != null)
                    {
                        existingClient.UpdatedOn = DateTime.UtcNow;
                        existingClient.UpdatedById = loggedInUser.Id;
                        existingClient.Executive = data.Executive;
                        existingClient.Company = data.Company;
                        existingClient.Email = data.Email;
                        existingClient.Phone = data.Phone;
                        existingClient.Address = data.Address;
                        unitOfWork.Clients.Update(existingClient);
                        unitOfWork.Complete();
                        return new Response<Client>
                        {
                            IsSuccess = true,
                            ResponseStatus = ResponseStatus.SUCCESS,
                            Message = "Successfully updated the client",
                            Data = unitOfWork.Clients.GetClientByCompanyNameAndExecutive(data)
                        };
                    }
                    else
                    {
                        return new Response<Client>
                        {
                            IsSuccess = false,
                            ResponseStatus = ResponseStatus.ERROR,
                            Info = new List<string> { "Unable to locate the client. Please verify the clientId." },
                            Message = "Operation aborted due to conflict"
                        };
                    }
                }
            }
            else
            {
                return new Response<Client>
                {
                    IsSuccess = false,
                    ResponseStatus = ResponseStatus.WARNING,
                    Info = new List<string> {
                            "Failed to claim permissions - Add.Client"
                            },
                    Message = "Operation aborted due to insufficient permissions"
                };
            }
        }

        public Response<Client> GetClientById(int clientId)
        {
            var loggedInUser = authorityService.GetLoggedInUser();
            var hasPermissions = MicroPermissions.HasAllPermissions(loggedInUser.Id, "View.Client");
            if (hasPermissions)
            {
                using (var unitOfWork = new UnitOfWork(new JetTaskContext(appConfig)))
                {
                    var existingClient = unitOfWork.Clients.Get(clientId);
                    if (existingClient != null)
                    {
                        return new Response<Client>
                        {
                            IsSuccess = true,
                            ResponseStatus = ResponseStatus.SUCCESS,
                            Data = existingClient
                        };
                    }
                    else
                    {
                        return new Response<Client>
                        {
                            IsSuccess = false,
                            ResponseStatus = ResponseStatus.ERROR,
                            Message = $"No client exist with same with id - {clientId}"
                        };
                    }
                }
            }
            else
            {
                return new Response<Client>
                {
                    IsSuccess = false,
                    ResponseStatus = ResponseStatus.WARNING,
                    Info = new List<string> {
                            "The current account lacks permission to create a new project",
                            "Failed to claim permissions - View.Client"
                            },
                    Message = "Operation aborted due to insufficient permissions"
                };
            }
        }

        public Response<List<Client>> GetMyClients()
        {
            var loggedInUser = authorityService.GetLoggedInUser();
            var hasPermissions = MicroPermissions.HasAllPermissions(loggedInUser.Id, "View.Clients");
            if (hasPermissions)
            {
                using (var unitOfWork = new UnitOfWork(new JetTaskContext(appConfig)))
                {
                    var allClients = unitOfWork.Clients.GetAllClientsByAuthor(loggedInUser.Id);
                    if (allClients != null)
                    {
                        return new Response<List<Client>>
                        {
                            IsSuccess = true,
                            ResponseStatus = ResponseStatus.SUCCESS,
                            Data = allClients
                        };
                    }
                    else
                    {
                        return new Response<List<Client>>
                        {
                            IsSuccess = false,
                            ResponseStatus = ResponseStatus.ERROR,
                            Message = $"Failed while fecthing all clients"
                        };
                    }
                }
            }
            else
            {
                return new Response<List<Client>>
                {
                    IsSuccess = false,
                    ResponseStatus = ResponseStatus.WARNING,
                    Info = new List<string> {
                            "Failed to claim permissions - View.Client"
                            },
                    Message = "Operation aborted due to insufficient permissions"
                };
            }
        }

        public Response DeleteClient(int clientId)
        {
            var loggedInUser = authorityService.GetLoggedInUser();
            var hasPermissions = MicroPermissions.HasAllPermissions(loggedInUser.Id, "Delete.Client");
            if (hasPermissions)
            {
                using (var unitOfWork = new UnitOfWork(new JetTaskContext(appConfig)))
                {
                    var client = unitOfWork.Clients.Get(clientId);
                    if (client != null)
                    {
                        unitOfWork.Clients.Remove(client);
                        unitOfWork.Complete();
                        return new Response
                        {
                            IsSuccess = true,
                            ResponseStatus = ResponseStatus.SUCCESS,
                            Message = $"Successfully deleted client"
                        };
                    }
                    else
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            ResponseStatus = ResponseStatus.ERROR,
                            Message = $"Unable to find the client requested to delete. Verify the clientId."
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
                    Info = new List<string> {
                            "Failed to claim permissions - Delete.Client"
                            },
                    Message = "Operation aborted due to insufficient permissions"
                };
            }
        }
    }
}