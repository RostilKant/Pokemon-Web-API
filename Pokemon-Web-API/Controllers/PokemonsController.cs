using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Contracts;
using Entities.GETAllFromPokeApi;
using Entities.Models;
using HttpServices;
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
        public PokemonsController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
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
            if (pokemonIds == null) return BadRequest("Parameter ids is null");
            var pokemons = _pokemonService.FindPokemonsByIds(pokemonIds);
            if (pokemons == null) return NotFound();
            return Ok(pokemons);
        }
        
        [HttpPost(Name = "CreatePokemon")]
        public IActionResult CreatePokemon([FromBody]PokemonForCreationDto pokemon)
        {
            if (pokemon == null) return BadRequest("PokemonForCreationDto object is null!");
            var pokemonDto = _pokemonService.PostPokemon(pokemon);
            //return CreatedAtRoute("PokemonById", new {id = pokemonDto.Id}, pokemonDto);
            return Created($"api/pokemons/{pokemonDto.Id}", pokemonDto);
        }

        [HttpPost("collection")]
        public IActionResult CreatePokemonCollection([FromBody] IEnumerable<PokemonForCreationDto> pokemonForCreation)
        {
            if (pokemonForCreation == null) return BadRequest("PokemonForCreationDto object is null!");
            var pokemons = _pokemonService.PostPokemonCollection(pokemonForCreation);
            var ids = string.Join(",", pokemons.Select(p => p.Id));
            return Created($"api/pokemons/collection/{ids}", pokemons);
        }
    }
}