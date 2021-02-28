using System.Linq;
using JetTask.Entities;
using JetTask.Data.Base;

namespace JetTask.Data.Repos
{
    //CONTRACT
    public interface IAdditionalCostRepository : IRepository<AdditionalCost>
    {
    }

    //IMPLEMENTATION
    public class AdditionalCostRepository : Repository<AdditionalCost>, IAdditionalCostRepository
    {
        public AdditionalCostRepository(JetTaskContext context) : base(context)
        {
        }

        public JetTaskContext Context { get { return (JetTaskContext)context; } }
    }
}