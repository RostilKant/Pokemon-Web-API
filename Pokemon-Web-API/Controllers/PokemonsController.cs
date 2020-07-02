using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Contracts;
using Entities.GETAllFromPokeApi;
using Entities.Models;
using HttpServices;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        public IActionResult GetPokeApimons()
        {
            return Ok(_pokemonService.GetAllFromPokeApi());
        }

        [HttpGet("poke-api/{pokeId}")]
        public IActionResult GetPokeApimon(string pokeId)
        {
            return Ok(_pokemonService.GetByIdFromPokeApi(pokeId));
        }
        
        [HttpGet]
        public IActionResult GetPokemons()
        {
            var pokemons = _pokemonService.FindAllPokemons();
            if (pokemons == null) return NotFound();
            return Ok(pokemons);
        }
        
        [HttpGet("{pokemonId}", Name = "GetPokemonById")]
        public IActionResult GetPokemonById(int pokemonId)
        {
            var pokemon = _pokemonService.FindPokemonById(pokemonId);
            if (pokemon == null) return NotFound();
            return Ok(pokemon);
        }
        
        [HttpGet("collection/{pokemonIds}", Name = "PokemonsCollection")]
        public IActionResult GetPokemonByIds([ModelBinder(BinderType =
        typeof(ArrayModelBinder))]IEnumerable<int> pokemonIds)
        {
             if (pokemonIds == null)
             {
                 _logger.LogError("Parameter ids is null");
                 return BadRequest("Parameter ids is null");
             }
             
             var pokemons = _pokemonService.FindPokemonsByIds(pokemonIds);
             if (pokemons == null) return NotFound();
             return Ok(pokemons);
        }
        
        [HttpPost(Name = "CreatePokemon")]
        public IActionResult CreatePokemon([FromBody]PokemonForCreationDto pokemon)
        {
            if(!ModelState.IsValid) return UnprocessableEntity(ModelState);
            
            if (pokemon == null)
            {
                _logger.LogInformation("PokemonForCreationDto object sent from client is null.");
                return BadRequest("PokemonForCreationDto object is null!");
            }
            
            var pokemonDto = _pokemonService.PostPokemon(pokemon);
            //return CreatedAtRoute("PokemonById", new {id = pokemonDto.Id}, pokemonDto);
            return Created($"api/pokemons/{pokemonDto.Id}", pokemonDto);
        }

        [HttpPost("collection")]
        public IActionResult CreatePokemonCollection([FromBody] IEnumerable<PokemonForCreationDto> pokemonForCreation)
        {
            if (pokemonForCreation == null)
            {
                _logger.LogInformation("PokemonForUpdateDto object sent from client is null.");
                return BadRequest("PokemonForCreationDto object is null!");
            }
            
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }
            var pokemons = _pokemonService.PostPokemonCollection(pokemonForCreation);
            var ids = string.Join(",", pokemons.Select(p => p.Id));
            return Created($"api/pokemons/collection/{ids}", pokemons);
        }

        [HttpDelete("{pokemonId}")]
        public IActionResult DeletePokemon(int pokemonId)
        {
            var pokemon = _pokemonService.DeletePokemon(pokemonId);
            if (!pokemon) return NotFound();
            return NoContent();
        }

        [HttpPut("{pokemonId}")]
        public IActionResult UpdatePokemon(int pokemonId, [FromBody] PokemonForUpdateDto pokemonForUpdate)
        {
            if (pokemonForUpdate == null)
            {
                _logger.LogInformation("PokemonForUpdateDto object sent from client is null.");
                return BadRequest("PokemonForUpdateDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }
            var pokemon = _pokemonService.UpdatePokemon(pokemonId, pokemonForUpdate);
            if (!pokemon) return NotFound();
            return NoContent();
        }

        [HttpPatch("{pokemonId}")]
        public IActionResult PatchPokemon(int pokemonId, JsonPatchDocument<PokemonForUpdateDto> patchDocument)
        {
            if (patchDocument == null)
            {                
                _logger.LogInformation("PatchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var pokemon = _pokemonService.PartiallyUpdatePokemon(pokemonId, patchDocument);
            patchDocument.ApplyTo(pokemon, ModelState);
            
            if(!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }
            _pokemonService.SaveAndMap(pokemonId, pokemon);
            return NoContent();
        }
    }
}