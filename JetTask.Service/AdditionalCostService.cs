using System;
using JetTask.Entities;
using JetTask.Entities.Misc;

namespace JetTask.Service
{
    public interface IAdditionalCostService
    {
    }

    public class AdditionalCostService : IAdditionalCostService
    {
        private readonly AppConfig appConfig;

        public AdditionalCostService(AppConfig appConfig)
        {
            this.appConfig = appConfig;
        }
    }
}