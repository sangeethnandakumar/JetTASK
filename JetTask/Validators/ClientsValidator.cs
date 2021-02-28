using ExpressGlobalExceptionHandler;
using JetTask.Entities.Dtos.Request;
using JetTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JetTask.Validators
{
    public static class ClientsValidator
    {
        private static List<string> Errors = new List<string>();

        public static Response ValidateCreate(CreateClientParameters data)
        {
            Errors.Clear();
            try
            {
                if (data.Executive == null || data.Executive.Length < 2 || data.Executive.Length > 25)
                {
                    Errors.Add("Client executive cannot be empty or outside between 2-25 charactors");
                }
                if (data.Company == null || data.Company.Length < 2 || data.Company.Length > 25)
                {
                    Errors.Add("Client company cannot be empty or outside between 2-25 charactors");
                }
                if (data.Address == null || data.Address.Length < 2 || data.Address.Length > 150)
                {
                    Errors.Add("Client address cannot be empty or outside between 2-150 charactors");
                }
                if (data.Phone == null || !Regex.IsMatch(data.Phone, @"^[+][0-9]{2} [0-9]{3}-[0-9]{3}-[0-9]{4}$"))
                {
                    Errors.Add("Client contact number need to be in the format '+XX XXX-XXX-XXXX' (Requres country code as shown)");
                }
                if (data.Email == null || !Regex.IsMatch(data.Email, @"^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+.)+[a-z]{2,5}$"))
                {
                    Errors.Add("Client email need to be in the format 'example@mail.com'");
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

        public static Response ValidateDelete(int clientId)
        {
            Errors.Clear();
            try
            {
                if (clientId <= 0)
                {
                    Errors.Add("You need to specify a valid client id to delete it");
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