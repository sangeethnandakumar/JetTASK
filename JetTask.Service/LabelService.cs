using System;
using JetTask.Entities;
using JetTask.Entities.Misc;

namespace JetTask.Service
{
    public interface ILabelService
    {
    }

    public class LabelService : ILabelService
    {
        private readonly AppConfig appConfig;

        public LabelService(AppConfig appConfig)
        {
            this.appConfig = appConfig;
        }
    }
}