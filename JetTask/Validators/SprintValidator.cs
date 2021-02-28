using System;
using ExpressGlobalExceptionHandler;
using System.Collections.Generic;
using System.Linq;
using JetTask.Entities.Dtos.Request;

namespace Example.API.Validators
{
    public static class SprintValidator
    {
        private static List<string> Errors = new List<string>();

        public static Response ValidateGet(int id)
        {
            Errors.Clear();
            try
            {
                if (id == 0)
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

        public static Response ValidateCreateSprint(CreateSprintParameters data)
        {
            Errors.Clear();
            try
            {
                if (data.Name == null || data.Name.Length < 2 || data.Name.Length > 15)
                {
                    Errors.Add("Sprint name cannot be empty or outside between 2-15 charactors");
                }
                if (data.Description == null || data.Description.Length < 5 || data.Description.Length > 50)
                {
                    Errors.Add("Sprint name cannot be empty or outside between 5-50 charactors");
                }
                if (data.ProjectId <= 0)
                {
                    Errors.Add("Invalid project Id, A sprint should be assocated with a valid project id");
                }
                if (data.SprintStart < DateTime.UtcNow)
                {
                    Errors.Add("Sprint start date should be after current UTC time");
                }
                if (data.SprintEnd < DateTime.UtcNow && data.SprintEnd < data.SprintStart)
                {
                    Errors.Add("Sprint end date should be after sprint start date and current UTC time");
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