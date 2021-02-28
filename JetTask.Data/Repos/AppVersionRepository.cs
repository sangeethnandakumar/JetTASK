using System.Linq;
using JetTask.Entities;
using JetTask.Data.Base;

namespace JetTask.Data.Repos
{
    //CONTRACT
    public interface IAppVersionRepository : IRepository<AppVersion>
    {
    }

    //IMPLEMENTATION
    public class AppVersionRepository : Repository<AppVersion>, IAppVersionRepository
    {
        public AppVersionRepository(JetTaskContext context) : base(context)
        {
        }

        public JetTaskContext Context { get { return (JetTaskContext)context; } }
    }
}