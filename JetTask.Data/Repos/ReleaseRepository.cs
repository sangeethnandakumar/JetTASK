using System.Linq;
using JetTask.Entities;
using JetTask.Data.Base;

namespace JetTask.Data.Repos
{
    //CONTRACT
    public interface IReleaseRepository : IRepository<Release>
    {
    }

    //IMPLEMENTATION
    public class ReleaseRepository : Repository<Release>, IReleaseRepository
    {
        public ReleaseRepository(JetTaskContext context) : base(context)
        {
        }

        public JetTaskContext Context { get { return (JetTaskContext)context; } }
    }
}