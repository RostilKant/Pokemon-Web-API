using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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

        private User _user;

        public UserService(ILogger<UserService> logger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }


        public async Task<bool> RegisterUser(UserForRegistrationDto userForRegistration, ModelStateDictionary modelState)
        {
            var user = _mapper.Map<User>(userForRegistration);
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    modelState.TryAddModelError(error.Code, error.Description);
                }
                return false;
            }

            userForRegistration.Roles ??= new List<string> {"User"};
            
                await _userManager.AddToRolesAsync(user, userForRegistration.Roles);

            return true;
        }

        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuthentication)
        {
            _user = await _userManager.FindByNameAsync(userForAuthentication.UserName);

            return (_user != null && await _userManager
                .CheckPasswordAsync(_user, userForAuthentication.Password));
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

