using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.GETAllFromPokeApi;
using Entities.Models;
using HttpServices;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pokemon_Web_API.ActionFilters;
using Pokemon_Web_API.ModelBinders;
using Pokemon = Entities.GetPokemonsFromPokeApi.Pokemon;


namespace Pokemon_Web_API.Controllers
{
    [Route("api/pokemons")]
    [ApiController]
    public class PokemonsController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;
        private readonly ILogger _logger;
        public PokemonsController(IPokemonService pokemonService, ILogger<PokemonsController> logger)
        {
            _pokemonService = pokemonService;
            _logger = logger;
        }
        
        [Route("poke-api")]
        [HttpGet]
        public IActionResult GetPokeApimons() => Ok(_pokemonService.GetAllFromPokeApi());

        [HttpGet("poke-api/{pokeId}")]
        public IActionResult GetPokeApimon(string pokeId) => Ok(_pokemonService.GetByIdFromPokeApi(pokeId));
        
        [HttpGet]
        public async Task<IActionResult> GetPokemons()
        {
            var pokemons = await _pokemonService.FindAllPokemonsAsync();
            if (pokemons == null) return NotFound();
            return Ok(pokemons);
        }
        
        [HttpGet("{pokemonId}", Name = "GetPokemonById")]
        [ServiceFilter(typeof(ValidatePokemonExistsAttribute))]
        public async Task<IActionResult> GetPokemonById(int pokemonId)
        {
            var pokemon =  await _pokemonService.FindPokemonByIdAsync(pokemonId);
            return Ok(pokemon);
        }
        
        [HttpGet("collection/{pokemonIds}", Name = "PokemonsCollection")]
        public async Task<IActionResult> GetPokemonByIds([ModelBinder(BinderType =
        typeof(ArrayModelBinder))]IEnumerable<int> pokemonIds)
        {
            var pokemons = await _pokemonService.FindPokemonsByIdsAsync(pokemonIds);

             if (pokemonIds == null)
             {
                 _logger.LogError("Parameter ids is null");
                 return BadRequest("Parameter ids is null");
             }
             
             return Ok(pokemons);
        }
        
        [HttpPost(Name = "CreatePokemon")]
        [ServiceFilter(typeof(ModelValidationFilterAttribute))]
        public async Task<IActionResult> CreatePokemon([FromBody]PokemonForCreationDto pokemon)
        {
            var pokemonDto = await _pokemonService.PostPokemonAsync(pokemon);
            //return CreatedAtRoute("PokemonById", new {id = pokemonDto.Id}, pokemonDto);
            return Created($"api/pokemons/{pokemonDto.Id}", pokemonDto);
        }

        [HttpPost("collection")]
        [ServiceFilter(typeof(ModelValidationFilterAttribute))]
        public async Task<IActionResult> CreatePokemonCollection([FromBody] IEnumerable<PokemonForCreationDto> pokemonForCreation)
        {
            var pokemons = await _pokemonService.PostPokemonCollectionAsync(pokemonForCreation);
            var ids = string.Join(",", pokemons.Select(p => p.Id));
            return Created($"api/pokemons/collection/{ids}", pokemons);
        }

        [HttpDelete("{pokemonId}")]
        [ServiceFilter(typeof(ValidatePokemonExistsAttribute))]
        public async Task<IActionResult> DeletePokemon(int pokemonId)
        {
            var pokemon = await _pokemonService.DeletePokemonAsync(pokemonId);
            return NoContent();
        }

        [HttpPut("{pokemonId}")]
        [ServiceFilter(typeof(ValidatePokemonExistsAttribute))]
        [ServiceFilter(typeof(ModelValidationFilterAttribute))]
        public async Task<IActionResult> UpdatePokemon(int pokemonId, [FromBody] PokemonForUpdateDto pokemonForUpdate)
        {
            var pokemon = await _pokemonService.UpdatePokemonAsync(pokemonId, pokemonForUpdate);
            return NoContent();
        }

        [HttpPatch("{pokemonId}")]
        [ServiceFilter(typeof(ModelValidationFilterAttribute))]
        public async Task<IActionResult> PatchPokemon(int pokemonId, JsonPatchDocument<PokemonForUpdateDto> patchDtoDocument)
        {
            var pokemon = await _pokemonService.PartiallyUpdatePokemonAsync(pokemonId, patchDtoDocument);
            patchDtoDocument.ApplyTo(pokemon, ModelState);
            await _pokemonService.SaveAndMapAsync(pokemonId, pokemon);
            return NoContent();
        }
    }
}