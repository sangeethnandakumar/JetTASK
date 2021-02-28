using JetTask.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetTask.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IProjectRepository Projects { get; }
        IClientRepository Clients { get; }
        ISprintRepository Sprints { get; }

        int Complete();
    }
}