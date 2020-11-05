using System;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace Contracts
{
    public interface IUserService
    {
        Task<bool> RegisterUser(UserForRegistrationDto userForRegistration, ModelStateDictionary modelState);

        Task<bool> ValidateUser(UserForAuthenticationDto userForAuthentication, ModelStateDictionary modelState);

        public Task<MyToken> CreateToken();
        
        Task<bool> ChangePassword(ChangePasswordModel changePasswordModel, ModelStateDictionary modelState);
        Task<bool> ResetPassword(ResetPasswordModel resetPasswordModel, ModelStateDictionary modelState);

        Task<bool> ConfirmEmail(string userId, string code, ModelStateDictionary modelState);

       // public Task ResendEmailToken(string userId);

        public Task<EmailToken> CreateEmailToken1();

        public Task SendEmailToken1(string link);

        public Task<EmailToken> CreatePasswordResetToken(string email,  ModelStateDictionary modelState);

        public Task SendEmailResetPasswordToken(string link);



    }
    public class MyToken
    {
        public string Token { get; set; }
        public DateTime ExpiresIn { get; set; }
    }

    public class EmailToken
    {
        public string UserId { get; set; }
        public string Code { get; set; }
    }
}