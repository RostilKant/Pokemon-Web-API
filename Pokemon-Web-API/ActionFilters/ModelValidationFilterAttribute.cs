using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Pokemon_Web_API.Controllers;

namespace Pokemon_Web_API.ActionFilters
{
    public class ModelValidationFilterAttribute : IAsyncActionFilter
    {
        private readonly ILogger _logger;

        public ModelValidationFilterAttribute(ILogger<PokemonsController> logger)
        {
            _logger = logger;
        }
        
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //var action = context.RouteData.Values["action"];
            //var controller = context.RouteData.Values["controller"];

            var param = context.ActionArguments
                .SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;
            if (param == null)
            {
                _logger.LogInformation("Object sent from client is null.");
                context.Result = new BadRequestObjectResult($"Object is null.");
                return;
            }
            
            if (!context.ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the object");
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
                return;
            }
            
            await next();
        }
    }
}