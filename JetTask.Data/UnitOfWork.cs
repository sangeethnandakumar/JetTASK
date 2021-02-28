using JetTask.Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetTask.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository Users { get; private set; }
        public IProjectRepository Projects { get; private set; }
        public IClientRepository Clients { get; private set; }
        public ISprintRepository Sprints { get; private set; }

        private readonly JetTaskContext context;

        public UnitOfWork(JetTaskContext context)
        {
            this.context = context;
            Users = new UserRepository(context);
            Projects = new ProjectRepository(context);
            Clients = new ClientRepository(context);
            Sprints = new SprintRepository(context);
        }

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}