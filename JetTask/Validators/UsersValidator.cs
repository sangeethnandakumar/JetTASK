using ExpressGlobalExceptionHandler;
using JetTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JetTask.Validators
{
    public static class UsersValidator
    {
        private static List<string> Errors = new List<string>();

        public static Response ValidateLogin(LoginParameters data)
        {
            Errors.Clear();
            try
            {
                if (data.Username == null || data.Username.Length < 1)
                {
                    Errors.Add("Username should have more than 1 charactor");
                }
                if (data.Password == null || data.Password.Length < 1)
                {
                    Errors.Add("Password should have more than 1 charactor");
                }
                return Errors.Count > 0 ? Response.ErrorResponse(Errors) : new Response();
            }
            catch (Exception)
            {
                return Response.ErrorResponse(new List<string> {
                    "Unable to validate the request"
                });
            }
        }
    }
}