using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pokemon_Web_API.ActionFilters;

namespace Pokemon_Web_API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public AuthenticationController(IUserService userService, ILogger<AuthenticationController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        /// <summary>
        /// User registration
        /// </summary>
        /// <param name="userForRegistration"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(ModelValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            if (!(await _userService.RegisterUser(userForRegistration, ModelState)))
                return BadRequest(ModelState);
            return StatusCode(201);
        }
        /// <summary>
        /// Users authorization 
        /// </summary>
        /// <param name="userForAuthentication"></param>
        /// <returns>JWT Token</returns>
        [HttpPost("login")]
        [ServiceFilter(typeof(ModelValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto userForAuthentication)
        {
            if (!await _userService.ValidateUser(userForAuthentication))
            {
                _logger.LogWarning($"{nameof(Authenticate)}: Authentication failed. " +
                                   $"Wrong user name or password.");
                return Unauthorized();
            }

            return Ok(await _userService.CreateToken());
        }    
    }
}