using System.Linq;
using JetTask.Entities;
using JetTask.Data.Base;

namespace JetTask.Data.Repos
{
    //CONTRACT
    public interface ICommentRepository : IRepository<Comment>
    {
    }

    //IMPLEMENTATION
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(JetTaskContext context) : base(context)
        {
        }

        public JetTaskContext Context { get { return (JetTaskContext)context; } }
    }
}