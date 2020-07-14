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

        Task<string> CreateToken();
    }
}