using System.Linq;
using JetTask.Entities;
using JetTask.Data.Base;

namespace JetTask.Data.Repos
{
    //CONTRACT
    public interface IAttachmentRepository : IRepository<Attachment>
    {
    }

    //IMPLEMENTATION
    public class AttachmentRepository : Repository<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(JetTaskContext context) : base(context)
        {
        }

        public JetTaskContext Context { get { return (JetTaskContext)context; } }
    }
}