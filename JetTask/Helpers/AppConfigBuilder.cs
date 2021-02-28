using JetTask.Entities.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JetTask.Helpers
{
    public static class AppConfigBuilder
    {
        public static AppConfig Build()
        {
            var confg = new AppConfig
            {
                Database = new Database
                {
                    Server = Startup.Configuration.GetSection("Database:Server").Value,
                    DatabaseName = Startup.Configuration.GetSection("Database:DatabaseName").Value,
                    IsTrustedConnection = bool.Parse(Startup.Configuration.GetSection("Database:IsTrustedConnection").Value),
                    Username = Startup.Configuration.GetSection("Database:Username").Value,
                    Password = Startup.Configuration.GetSection("Database:Password").Value,
                },
                Security = new Security
                {
                    JwtSecret = Startup.Configuration.GetSection("Security:JWTPrivateKey").Value,
                }
            };
            return confg;
        }
    }
}