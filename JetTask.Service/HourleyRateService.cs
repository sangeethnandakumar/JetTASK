using System;
using JetTask.Entities;
using JetTask.Entities.Misc;

namespace JetTask.Service
{
    public interface IHourleyRateService
    {
    }

    public class HourleyRateService : IHourleyRateService
    {
        private readonly AppConfig appConfig;

        public HourleyRateService(AppConfig appConfig)
        {
            this.appConfig = appConfig;
        }
    }
}