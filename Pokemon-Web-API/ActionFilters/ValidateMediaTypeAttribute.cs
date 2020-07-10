using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;

namespace Pokemon_Web_API.ActionFilters
{
    public class ValidateMediaTypeAttribute : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var acceptPresentHeader = context.HttpContext.Request.Headers.ContainsKey("Accept");
            if (!acceptPresentHeader)
                context.Result = new BadRequestObjectResult
                    ("AcceptHeader is missing!");
            
            var mediaType = context.HttpContext.Request.Headers["Accept"].FirstOrDefault();

            if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue outMediaType)) 
                context.Result = new BadRequestObjectResult
                    ("AcceptHeader is missing!");
            
            context.HttpContext.Items.Add("AcceptHeaderMediaType", outMediaType);
            
            await next();
        }
    }
}