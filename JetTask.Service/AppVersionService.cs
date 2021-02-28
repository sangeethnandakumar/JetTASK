using System;
using JetTask.Entities;
using JetTask.Entities.Misc;

namespace JetTask.Service
{
    public interface IAppVersionService
    {
    }

    public class AppVersionService : IAppVersionService
    {
        private readonly AppConfig appConfig;

        public AppVersionService(AppConfig appConfig)
        {
            this.appConfig = appConfig;
        }
    }
}