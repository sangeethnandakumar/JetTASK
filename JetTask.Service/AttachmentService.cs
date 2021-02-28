using System;
using JetTask.Entities;
using JetTask.Entities.Misc;

namespace JetTask.Service
{
    public interface IAttachmentService
    {
    }

    public class AttachmentService : IAttachmentService
    {
        private readonly AppConfig appConfig;

        public AttachmentService(AppConfig appConfig)
        {
            this.appConfig = appConfig;
        }
    }
}