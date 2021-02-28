using JetTask.Data.Base;
using JetTask.Entities;
using System.Collections.Generic;
using System.Linq;

namespace JetTask.Data.Repos
{
    //CONTRACT
    public interface IProjectRepository : IRepository<Project>
    {
        public Project GetProjectByName(string projectName);

        public List<Project> GetProjectsByAuthor(int authorId);
    }

    //IMPLEMENTATION
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(JetTaskContext context) : base(context)
        {
        }

        public JetTaskContext Context { get { return (JetTaskContext)context; } }

        public Project GetProjectByName(string projectName)
        {
            return Context.Projects.Where(x => x.Name == projectName).FirstOrDefault();
        }

        public List<Project> GetProjectsByAuthor(int authorId)
        {
            return Context.Projects.Where(x => x.IsActive == true).ToList();
            //return Context.Projects.Where(x => x.CreatedBy == authorId && x.IsActive==true).ToList();
        }
    }
}