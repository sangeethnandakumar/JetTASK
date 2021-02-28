using System.Linq;
using JetTask.Entities;
using JetTask.Data.Base;

namespace JetTask.Data.Repos
{
    //CONTRACT
    public interface ITicketRepository : IRepository<Ticket>
    {
    }

    //IMPLEMENTATION
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public TicketRepository(JetTaskContext context) : base(context)
        {
        }

        public JetTaskContext Context { get { return (JetTaskContext)context; } }
    }
}