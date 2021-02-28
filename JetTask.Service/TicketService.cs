using System;
using JetTask.Entities;
using JetTask.Entities.Misc;

namespace JetTask.Service
{
    public interface ITicketService
    {
    }

    public class TicketService : ITicketService
    {
        private readonly AppConfig appConfig;

        public TicketService(AppConfig appConfig)
        {
            this.appConfig = appConfig;
        }
    }
}