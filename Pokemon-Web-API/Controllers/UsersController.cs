using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pokemon_Web_API.ActionFilters;

namespace Pokemon_Web_API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        /// <summary>
        /// User registration
        /// </summary>
        /// <param name="userForRegistration"></param>
        /// <returns></returns>
        [HttpPost("registration")]
        [ServiceFilter(typeof(ModelValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            
            if (!(await _userService.RegisterUser(userForRegistration, ModelState)))
                return BadRequest(ModelState);

            var token = await _userService.CreateEmailToken1();

            var link = Url.Action("ConfirmEmail",
                "Users", token, protocol: HttpContext.Request.Scheme);

            await _userService.SendEmailToken1(link);

            return Content("For finishing registration, check your email and click the link");
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
            if (await _userService.ValidateUser(userForAuthentication, ModelState)) return Ok(await _userService.CreateToken());
            _logger.LogWarning($"{nameof(Authenticate)}: Authentication failed. " +
                               $"Wrong user name or password.");
            return BadRequest(ModelState);
        }

        [HttpPost("change-pass")]
        [ServiceFilter(typeof(ModelValidationFilterAttribute))]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel changePasswordModel)
        {
            if (!(await _userService.ChangePassword(changePasswordModel, ModelState)))
                return BadRequest(ModelState);
            return StatusCode(201);
        }

        [HttpPost("forgot-pass")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel forgotPasswordModel)
        {
            var token = await _userService.CreatePasswordResetToken(forgotPasswordModel.Email);
            var link = Url.Action("ConfirmEmail",
                "Users", token, protocol: HttpContext.Request.Scheme);
            await _userService.SendEmailResetPasswordToken(link);

            return Ok();
        }
        
        [HttpPost("reset-pass")]
        [ServiceFilter(typeof(ModelValidationFilterAttribute))]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel resetPasswordModel)
        {
            if (!(await _userService.ResetPassword(resetPasswordModel, ModelState)))
                return BadRequest(ModelState);
            
            return StatusCode(201);
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string UserId, [FromQuery] string Code)
        {
            if (await _userService.ConfirmEmail(UserId, Code, ModelState)) return Ok();
            
            // await _userService.ResendEmailToken(userId);
            return BadRequest(ModelState);
        }
        
    }
}