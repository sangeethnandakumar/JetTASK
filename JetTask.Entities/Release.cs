using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetTask.Entities
{
    public class Release
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string ReleaseNotes { get; set; }
        public Sprint Sprint { get; set; }
        public string GitHubLink { get; set; }
        public DateTime CreatedOn { get; set; }
        public User CreatedBy { get; set; }
    }
}