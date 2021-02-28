using JetTask.Data;
using JetTask.Entities;
using JetTask.Entities.Misc;
using JetTask.Service.Helpers;
using System;

namespace JetTask.Service

{
    public interface IUserService
    {
        public User GetUserByUsername(string username);

        public User GetUserById(int id);
    }

    public class UserService : IUserService
    {
        private readonly AppConfig appConfig;

        public UserService(AppConfig appConfig)
        {
            this.appConfig = appConfig;
        }

        public User GetUserByUsername(string username)
        {
            if (username != null)
            {
                using (var unitOfWork = new UnitOfWork(new JetTaskContext(appConfig)))
                {
                    var user = unitOfWork.Users.GetUserByUsername(username);
                    if (user != null)
                    {
                        return user;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                return null;
            }
        }

        public User GetUserById(int id)
        {
            if (id != 0)
            {
                using (var unitOfWork = new UnitOfWork(new JetTaskContext(appConfig)))
                {
                    var user = unitOfWork.Users.Get(id);
                    if (user != null)
                    {
                        return user;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                return null;
            }
        }
    }
}