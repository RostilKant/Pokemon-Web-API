using System;
using System.Collections.Generic;    
using AutoMapper;
using Contracts;
using Entities.GETAllFromPokeApi;
using Entities.Models;
using HttpServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        [Route("pokeApi")]
        [HttpGet]
        public IActionResult GetPokeApimons()
        {
            return Ok(_pokemonService.GetAllFromPokeApi());
        }

        [HttpGet("pokeApi/{pokeId}")]
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
        
        [HttpGet("{pokemonId}")]
        public IActionResult GetPokemonById(int pokemonId)
        {
            var pokemon = _pokemonService.FindPokemonById(pokemonId);
            if (pokemon == null) return NotFound();
            return Ok(pokemon);
        }
    }
}