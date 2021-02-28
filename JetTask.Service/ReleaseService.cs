using System;
using JetTask.Entities;
using JetTask.Entities.Misc;

namespace JetTask.Service
{
    public interface IReleaseService
    {
    }

    public class ReleaseService : IReleaseService
    {
        private readonly AppConfig appConfig;

        public ReleaseService(AppConfig appConfig)
        {
            this.appConfig = appConfig;
        }
    }
}