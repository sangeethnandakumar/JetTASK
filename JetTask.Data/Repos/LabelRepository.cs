using System.Linq;
using JetTask.Entities;
using JetTask.Data.Base;

namespace JetTask.Data.Repos
{
    //CONTRACT
    public interface ILabelRepository : IRepository<Label>
    {
    }

    //IMPLEMENTATION
    public class LabelRepository : Repository<Label>, ILabelRepository
    {
        public LabelRepository(JetTaskContext context) : base(context)
        {
        }

        public JetTaskContext Context { get { return (JetTaskContext)context; } }
    }
}