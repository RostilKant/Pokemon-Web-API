using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Pokemon_Web_API.Controllers;
using Repository;

namespace Pokemon_Web_API.ActionFilters
{
    public class ValidatePokemonExistsAttribute : IAsyncActionFilter
    {
        private readonly ILogger _logger;
        private readonly IRepositoryManager _repository;
        public ValidatePokemonExistsAttribute(ILogger<PokemonsController> logger, IRepositoryManager repository)
        {
            _logger = logger;
            _repository = repository;
        }
        
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
            var id =  context.ActionArguments["pokemonId"] ?? context.ActionArguments["pokemonIds"];
            var pokemon = await _repository.Pokemon.GetPokemonAsync((int)id, trackChanges);
            
            if (pokemon == null)
            {
                _logger.LogInformation($"Pokemon with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("pokemon", pokemon);
                await next();
            }

        }
    }
}