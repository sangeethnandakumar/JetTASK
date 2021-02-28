using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JetTask.Entities.Dtos.Request
{
    public class CreateClientParameters
    {
        public string Executive { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}