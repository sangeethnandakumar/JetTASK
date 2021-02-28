using System;
using JetTask.Entities;
using JetTask.Entities.Misc;

namespace JetTask.Service
{
    public interface IInvoiceService
    {
    }

    public class InvoiceService : IInvoiceService
    {
        private readonly AppConfig appConfig;

        public InvoiceService(AppConfig appConfig)
        {
            this.appConfig = appConfig;
        }
    }
}