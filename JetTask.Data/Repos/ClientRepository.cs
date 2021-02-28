using JetTask.Data.Base;
using JetTask.Entities;
using JetTask.Entities.Dtos.Request;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace JetTask.Data.Repos
{
    //CONTRACT
    public interface IClientRepository : IRepository<Client>
    {
        public Client GetClientByCompanyNameAndExecutive(CreateClientParameters data);

        public List<Client> GetAllClientsByAuthor(int authorId);
    }

    //IMPLEMENTATION
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(JetTaskContext context) : base(context)
        {
        }

        public JetTaskContext Context { get { return (JetTaskContext)context; } }

        public List<Client> GetAllClientsByAuthor(int authorId)
        {
            return Context.Clients.Where(x => x.CreatedById == authorId).ToList();
        }

        public Client GetClientByCompanyNameAndExecutive(CreateClientParameters data)
        {
            return Context.Clients.Where(x => x.Company == data.Company || x.Executive == data.Executive).FirstOrDefault();
        }
    }
}