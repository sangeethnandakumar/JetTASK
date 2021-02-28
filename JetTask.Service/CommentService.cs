using System;
using JetTask.Entities;
using JetTask.Entities.Misc;

namespace JetTask.Service
{
    public interface ICommentService
    {
    }

    public class CommentService : ICommentService
    {
        private readonly AppConfig appConfig;

        public CommentService(AppConfig appConfig)
        {
            this.appConfig = appConfig;
        }
    }
}