using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        private User _user;

        public UserService(ILogger<UserService> logger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration, IEmailService emailService)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _emailService = emailService;
        }


        public async Task<bool> RegisterUser(UserForRegistrationDto userForRegistration, ModelStateDictionary modelState)
        {
            _user = _mapper.Map<User>(userForRegistration);
            var result = await _userManager.CreateAsync(_user, userForRegistration.Password);

            if (userForRegistration.Password != userForRegistration.ConfirmPassword)
            {
                modelState.TryAddModelError("error", "Password mismatch");
                return false;
            }

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    modelState.TryAddModelError(error.Code, error.Description);
                }
                return false;
            }

            // await CreateEmailToken(user);
            
            userForRegistration.Roles ??= new List<string> {"User"};
            
                await _userManager.AddToRolesAsync(_user, userForRegistration.Roles);

            return true;
        }

        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuthentication, ModelStateDictionary modelState)
        {
            _user = await _userManager.FindByEmailAsync(userForAuthentication.Email);

            if (_user == null)    
            {
                modelState.TryAddModelError("wrongEmailOrPassword", "Wrong email or password");
                return false;
            }
            
            if (!(await _userManager.IsEmailConfirmedAsync(_user)))
            {
                modelState.TryAddModelError("notConfirmedEmail", "Email wasn't confirmed!");
                return false;
            }

            return (_user != null && await _userManager
                .CheckPasswordAsync(_user, userForAuthentication.Password));
        }
        
        public async Task<bool> ChangePassword(ChangePasswordModel changePasswordModel, ModelStateDictionary modelState)
        {
            _user = await _userManager.FindByEmailAsync(changePasswordModel.Email);
            if (_user != null)
            {
                var result = await _userManager
                    .ChangePasswordAsync(_user, 
                    changePasswordModel.OldPassword, 
                    changePasswordModel.NewPassword);
                
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        modelState.TryAddModelError(error.Code, error.Description);
                    }
                    return false;
                }
                
            }
            else
            {
                modelState.TryAddModelError("userNotFound", "User with such email doesn't exists");
                return false;
            }

            return true;
        }
        
        public async Task<bool> ResetPassword(ResetPasswordModel resetPasswordModel, ModelStateDictionary modelState)
        {
            _user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (_user != null)
            {
                var result = await _userManager
                    .ResetPasswordAsync(_user, 
                        resetPasswordModel.Code,
                        resetPasswordModel.NewPassword);
                
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        modelState.TryAddModelError(error.Code, error.Description);
                    }
                    return false;
                }
                
            }

            return true;
        }
        
        

        public async Task<bool> ConfirmEmail(string userId, string code, ModelStateDictionary modelState)
        {
            _user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ConfirmEmailAsync(_user, code);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    modelState.TryAddModelError(error.Code, error.Description);
                }

                return false;
            }

            return true;

        }

        public async Task ResendEmailToken(string userId)
        {
            _user = await _userManager.FindByIdAsync(userId);
            
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(_user);
            var callbackUrl = $"https://localhost:5001/api/users/ConfirmEmail?userId={_user.Id}&code={code}";

            await _emailService.SendEmailAsync(_user.Email, "Confirm your password for Pokemon Web API", 
                $"If u want to reset your password click link: <a href='{callbackUrl}'>{callbackUrl}</a> ");

        }


        public async Task<MyToken> CreateToken()
        {
            var credentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(credentials, claims);
            
            return new MyToken
            {
                Token = new JwtSecurityTokenHandler()
                    .WriteToken(tokenOptions),
                ExpiresIn = tokenOptions.ValidTo.ToLocalTime()
            };

        }

        /*public async Task CreateEmailToken(User user)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = $"https://localhost:5001/api/users/ConfirmEmail?userId={user.Id}&code={code}";

            await _emailService.SendEmailAsync(user.Email, "Confirm your password for Pokemon Web API", 
                $"Submit your email by clicking this link: <a href='{callbackUrl}'>{callbackUrl}</a> ");
        }*/
        
        public async Task<EmailToken> CreateEmailToken1()
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(_user);
            return new EmailToken {UserId = _user.Id, Code = code};
        }
        
        public async Task SendEmailToken1(string link)
        {
            // var callbackUrl = $"https://localhost:5001/api/users/ConfirmEmail?userId={user.Id}&code={code}";

            await _emailService.SendEmailAsync(_user.Email, "Confirm your password for Pokemon Web API", 
                $"Submit your email by clicking this link: <a href='{link}'>{link}</a> ");
            
        }

        public async Task<EmailToken> CreatePasswordResetToken(string email)
        {
            _user = await _userManager.FindByEmailAsync(email);
            
            var code = await _userManager.GeneratePasswordResetTokenAsync(_user);
            return new EmailToken {UserId = _user.Id, Code = code};
        }
        
        public async Task SendEmailResetPasswordToken(string link)
        {
            var link1 = string.Concat("https://localhost:4200/admin/reset-pass", link.Remove(0, 45));
            await _emailService.SendEmailAsync(_user.Email, "Confirm your password for Pokemon Web API", 
                $"Submit your email by clicking this link: <a href='{link1}'>{link1}</a> ");
            
        }

        private static SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET")!);
            var secret = new SymmetricSecurityKey(key);
            IdentityModelEventSource.ShowPII = true; 

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user.UserName)
            };
            var roles = await _userManager.GetRolesAsync(_user);

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials credentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            
            return new JwtSecurityToken(
                issuer: jwtSettings.GetSection("validIssuer").Value,
                audience: jwtSettings.GetSection("validAudience").Value,
                claims: claims,
                expires:
                    DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("expires").Value)),
                signingCredentials: credentials
                );
        }
        
    }
    
}

