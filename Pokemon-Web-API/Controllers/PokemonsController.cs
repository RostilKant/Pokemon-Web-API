using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Entities.RequestFeatures;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pokemon_Web_API.ActionFilters;
using Pokemon_Web_API.ModelBinders;


namespace Pokemon_Web_API.Controllers
{
    [Route("api/pokemons")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    //[ResponseCache(CacheProfileName = "Duration120")]
    public class PokemonsController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;
        private readonly ILogger _logger;
        public PokemonsController(IPokemonService pokemonService, ILogger<PokemonsController> logger)
        {
            _pokemonService = pokemonService;
            _logger = logger;
        }
        /// <summary>
        /// Get the list of all pokemons from https://pokeapi.co
        /// </summary>
        /// <returns>List of pokemons from https://pokeapi.co</returns>
        [Route("poke-api")]
        [HttpGet]
        public IActionResult GetPokeApimons() => Ok(_pokemonService.GetAllFromPokeApi());

        /// <summary>
        ///  Get the pokemons with pokeId from https://pokeapi.co
        /// </summary>
        /// <param name="pokeId"></param>
        /// <returns>Single pokemon from https://pokeapi.co</returns>
        [HttpGet("poke-api/{pokeId}")]
        public IActionResult GetPokeApimon(string pokeId) => Ok(_pokemonService.GetByIdFromPokeApi(pokeId));
        
        [Authorize]
        [HttpGet(Name = "GetPokemons")]
        //[HttpHead]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetPokemons([FromQuery] PokemonParameters pokemonParameters)
        {
            var pokemons = await _pokemonService.FindAllPokemonsAsync(pokemonParameters, Response);
            if (pokemons == null) return NotFound();
            //var links = _pokemonService.GenerateLinksOrShapePokemons(pokemons,pokemonParameters,HttpContext);
            //return links.HasLinks ? Ok(links.LinkedShapedObjects) : Ok(links.ShapedObjects);
            return Ok(pokemons);
        }
        
        [Authorize]
        [HttpGet("{pokemonId}", Name = "GetPokemonById")]
        [ServiceFilter(typeof(ValidatePokemonExistsAttribute))]
        //[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any)]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> GetPokemonById(int pokemonId) => 
            Ok(await _pokemonService.FindPokemonByIdAsync(pokemonId));
        
        [Authorize]
        [HttpGet("collection/", Name = "PokemonsCollection")]
        public async Task<IActionResult> GetPokemonByIds([ModelBinder(BinderType =
        typeof(ArrayModelBinder))]IEnumerable<int> pokemonIds)
        {
            var pokemons = await _pokemonService.FindPokemonsByIdsAsync(pokemonIds);
            if (pokemons == null) return NotFound(); 
            if (pokemonIds == null) 
            {
                 _logger.LogError("Parameter ids is null");
                 return BadRequest("Parameter ids is null"); 
            }
            return Ok(pokemons);
        }
        
        [Authorize]
        [HttpPost(Name = "CreatePokemon")]
        [ServiceFilter(typeof(ModelValidationFilterAttribute))]
        public async Task<IActionResult> CreatePokemon([FromBody]PokemonForCreationDto pokemon)
        {
            var pokemonDto = await _pokemonService.PostPokemonAsync(pokemon);
            //return CreatedAtRoute("PokemonById", new {id = pokemonDto.Id}, pokemonDto);
            return Created($"api/pokemons/{pokemonDto.Id}", pokemonDto);
        }

        [Authorize]
        [HttpPost("collection")]
        [ServiceFilter(typeof(ModelValidationFilterAttribute))]
        public async Task<IActionResult> CreatePokemonCollection([FromBody] IEnumerable<PokemonForCreationDto> pokemonForCreation)
        {
            var pokemons = await _pokemonService.PostPokemonCollectionAsync(pokemonForCreation);
            var ids = string.Join(",", pokemons.Select(p => p.Id));
            return Created($"api/pokemons/collection/{ids}", pokemons);
        }

        [Authorize]
        [HttpDelete("{pokemonId}", Name = "DeletePokemon")]
        [ServiceFilter(typeof(ValidatePokemonExistsAttribute))]
        public async Task<IActionResult> DeletePokemon(int pokemonId)
        {
            await _pokemonService.DeletePokemonAsync(pokemonId);
            return NoContent();
        }

        [Authorize]
        [HttpPut("{pokemonId}", Name = "UpdatePokemon")]
        [ServiceFilter(typeof(ValidatePokemonExistsAttribute))]
        [ServiceFilter(typeof(ModelValidationFilterAttribute))]
        public async Task<IActionResult> UpdatePokemon(int pokemonId, [FromBody] PokemonForUpdateDto pokemonForUpdate)
        {
            await _pokemonService.UpdatePokemonAsync(pokemonId, pokemonForUpdate);
            return NoContent();
        }
        
        [Authorize]
        [HttpPatch("{pokemonId}", Name = "PartiallyUpdatePokemon")]
        [ServiceFilter(typeof(ModelValidationFilterAttribute))]
        public async Task<IActionResult> PartiallyUpdatePokemon(int pokemonId, [FromBody] JsonPatchDocument<PokemonForUpdateDto> patchDtoDocument)
        {
            var pokemon = await _pokemonService.PartiallyUpdatePokemonAsync(pokemonId, patchDtoDocument);
            if (pokemon == null) return NotFound();
            patchDtoDocument.ApplyTo(pokemon, ModelState);
            await _pokemonService.SaveAndMapAsync(pokemonId, pokemon);
            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetPokemonsOptions()
        {
            Response.Headers.Add("Allow","GET, OPTIONS, POST");
            return Ok();
        }
        
    }
}