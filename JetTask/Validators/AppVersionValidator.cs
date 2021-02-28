using System;
using ExpressGlobalExceptionHandler;
using System.Collections.Generic;
using System.Linq;

namespace Example.API.Validators
{
    public static class AppVersionValidator
    {
        private static List<string> Errors = new List<string>();

        public static Response ValidateGet(int id)
        {
            Errors.Clear();
            try
            {
                if (id==0)
                {
                    Errors.Add("Custom bad request message");
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
