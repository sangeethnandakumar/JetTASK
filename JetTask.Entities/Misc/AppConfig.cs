using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetTask.Entities.Misc
{
    public class AppConfig
    {
        public Database Database { get; set; }
        public Security Security { get; set; }
    }

    public class Security
    {
        public string JwtSecret { get; set; }
    }

    public class Database
    {
        public string Server { get; set; }
        public string DatabaseName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsTrustedConnection { get; set; }
    }
}