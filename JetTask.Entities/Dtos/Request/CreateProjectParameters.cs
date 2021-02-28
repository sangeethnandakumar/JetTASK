using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JetTask.Entities.Dtos.Request
{
    public class CreateProjectParameters
    {
        public string Prefix { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ClientId { get; set; }
    }
}