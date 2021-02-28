using System.Linq;
using JetTask.Entities;
using JetTask.Data.Base;

namespace JetTask.Data.Repos
{
    //CONTRACT
    public interface IHourleyRateRepository : IRepository<HourleyRate>
    {
    }

    //IMPLEMENTATION
    public class HourleyRateRepository : Repository<HourleyRate>, IHourleyRateRepository
    {
        public HourleyRateRepository(JetTaskContext context) : base(context)
        {
        }

        public JetTaskContext Context { get { return (JetTaskContext)context; } }
    }
}