using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JetTask.Entities.Dtos.Request
{
    public class CreateSprintParameters
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime SprintStart { get; set; }
        public DateTime SprintEnd { get; set; }
        public int ProjectId { get; set; }
    }
}