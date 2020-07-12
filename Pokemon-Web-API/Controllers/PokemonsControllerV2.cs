using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Pokemon_Web_API.ActionFilters;

namespace Pokemon_Web_API.Controllers
{
    [Route("api/pokemons")]
    [ApiController]
    public class PokemonsControllerV2 : Controller
    {
        private readonly IPokemonService _pokemonService;
        private readonly ILogger _logger;
        public PokemonsControllerV2(IPokemonService pokemonService, ILogger<PokemonsController> logger)
        {
            _pokemonService = pokemonService;
            _logger = logger;
        }
        
        [HttpGet(Name = "GetPokemons")]
        [HttpHead]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetPokemons([FromQuery] PokemonParameters pokemonParameters)
        {
            var pokemons = await _pokemonService.FindAllPokemonsAsync(pokemonParameters);
            var pokemons1 = pokemons.Where(p =>
                p.Types.Any(t => t.Name.Contains("p")));
            if (pokemons1 == null) return NotFound();
            var links = _pokemonService.GenerateLinksOrShapePokemons(pokemons1,pokemonParameters,HttpContext);
            return links.HasLinks ? Ok(links.LinkedShapedObjects) : Ok(links.ShapedObjects);
        }
    }
}