using System;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace Contracts
{
    public interface IUserService
    {
        Task<bool> RegisterUser(UserForRegistrationDto userForRegistration, ModelStateDictionary modelStateDictionary);

        Task<bool> ValidateUser(UserForAuthenticationDto userForAuthentication);

        Task<MyToken> CreateToken();
    }
    public class MyToken
    {
        public string Token { get; set; }
        public TimeSpan ExpiresIn { get; set; }
    }
}