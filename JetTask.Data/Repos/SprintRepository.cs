using System.Linq;
using JetTask.Entities;
using JetTask.Data.Base;
using System.Collections.Generic;

namespace JetTask.Data.Repos
{
    //CONTRACT
    public interface ISprintRepository : IRepository<Sprint>
    {
        List<Sprint> GetSprintsByProject(Project project);

        List<Sprint> GetSprintsByProjects(List<Project> projects);
    }

    //IMPLEMENTATION
    public class SprintRepository : Repository<Sprint>, ISprintRepository
    {
        public SprintRepository(JetTaskContext context) : base(context)
        {
        }

        public JetTaskContext Context { get { return (JetTaskContext)context; } }

        public List<Sprint> GetSprintsByProject(Project project)
        {
            return Context.Sprints.Where(x => x.Project == project).ToList();
        }

        public List<Sprint> GetSprintsByProjects(List<Project> projects)
        {
            var sprints = new List<Sprint>();
            foreach (var project in projects)
            {
                sprints.AddRange(GetSprintsByProject(project));
            }
            return sprints;
        }
    }
}