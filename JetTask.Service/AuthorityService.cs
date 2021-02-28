using ExpressEncription;
using ExpressGlobalExceptionHandler;
using JetTask.Entities;
using JetTask.Entities.Dtos;
using JetTask.Entities.Dtos.Response;
using JetTask.Entities.Misc;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JetTask.Service
{
    //CONTACTS
    public interface IAuthorityService
    {
        public Response<LoginResponse> Login(string username, string password);

        public User GetLoggedInUser();
    }

    //IMPLEMENTATION
    public class AuthorityService : IAuthorityService
    {
        private readonly AppConfig appConfig;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthorityService(AppConfig appConfig, IHttpContextAccessor httpContextAccessor)
        {
            this.appConfig = appConfig;
            this.httpContextAccessor = httpContextAccessor;
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appConfig.Security.JwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public Response<LoginResponse> Login(string username, string password)
        {
            if (username != null && password != null)
            {
                var userService = new UserService(appConfig);
                var user = userService.GetUserByUsername(username);
                if (user != null)
                {
                    if (BlowFishHashing.Validate(password, user.Password))
                    {
                        return new Response<LoginResponse>
                        {
                            IsSuccess = true,
                            ResponseStatus = ResponseStatus.SUCCESS,
                            Data = new LoginResponse
                            {
                                Id = user.Id,
                                Avatar = user.Avatar,
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                Token = GenerateJwtToken(user)
                            }
                        };
                    }
                    else
                    {
                        return new Response<LoginResponse>
                        {
                            IsSuccess = false,
                            ResponseStatus = ResponseStatus.ERROR,
                            Message = "Incorrect username or password provided"
                        };
                    }
                }
                else
                {
                    return new Response<LoginResponse>
                    {
                        IsSuccess = false,
                        ResponseStatus = ResponseStatus.ERROR,
                        Message = $"Unable to find user with username '{username}'"
                    };
                }
            }
            else
            {
                return new Response<LoginResponse>
                {
                    IsSuccess = false,
                    ResponseStatus = ResponseStatus.ERROR,
                    Message = $"Invalid username or password provided"
                };
            }
        }

        public User GetLoggedInUser()
        {
            try
            {
                var user = (User)httpContextAccessor.HttpContext.Items["User"];
                if (user != null)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}