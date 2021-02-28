using ExpressGlobalExceptionHandler;
using JetTask.Entities.Dtos.Request;
using JetTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JetTask.Validators
{
    public static class ProjectsValidator
    {
        private static List<string> Errors = new List<string>();

        public static Response ValidateCreate(CreateProjectParameters data)
        {
            Errors.Clear();
            try
            {
                if (data.Prefix == null || data.Prefix.Length < 2 || data.Prefix.Length > 6)
                {
                    Errors.Add("Project prefix cannot be empty or outside between 2-6 charactors");
                }
                if (data.Name == null || data.Name.Length < 5 || data.Prefix.Length > 25)
                {
                    Errors.Add("Project name cannot be empty or outside between 5-25 charactors");
                }
                if (data.Description == null || data.Description.Length < 5 || data.Description.Length > 250)
                {
                    Errors.Add("Project description cannot be empty or outside between 5-250 charactors");
                }
                if (data.ClientId == 0)
                {
                    Errors.Add("Please verify the client provided to bind the project");
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

        public static Response ValidateDelete(int id)
        {
            Errors.Clear();
            try
            {
                if (id <= 0)
                {
                    Errors.Add("You need to specify a valid project id to delete it");
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