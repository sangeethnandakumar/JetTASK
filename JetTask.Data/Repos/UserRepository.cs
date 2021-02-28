using JetTask.Data.Base;
using JetTask.Entities;
using System.Linq;

namespace JetTask.Data.Repos
{
    //CONTRACT
    public interface IUserRepository : IRepository<User>
    {
        public User GetUserByUsername(string username);
    }

    //IMPLEMENTATION
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(JetTaskContext context) : base(context)
        {
        }

        public JetTaskContext Context { get { return (JetTaskContext)context; } }

        public User GetUserByUsername(string username)
        {
            return Context.Users.Where(x => x.Username == username).FirstOrDefault();
        }
    }
}