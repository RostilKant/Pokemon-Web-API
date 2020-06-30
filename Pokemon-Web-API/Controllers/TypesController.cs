using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;

namespace Pokemon_Web_API.Controllers
{
    [Route("api/pokemons/{pokemonId}/types")]
    public class TypesController : ControllerBase
    {
        private readonly ITypeService _typeService;

        public TypesController(ITypeService typeService)
        {
            _typeService = typeService;
        }

        [HttpGet( Name = "GetTypesForPokemon")]
        public IActionResult GetTypes(int pokemonId)
        {
            var types = _typeService.GetAllTypesOfPokemon(pokemonId);
            if (types == null) return NotFound();
            return Ok(types);
        }

        // [HttpPost]
        // public IActionResult CreateType(int pokemonId, TypeForCreationDto typeForCreationDto)
        // {
        //     var type = _typeService.PostType(pokemonId, typeForCreationDto);
        //     if (type == null) return BadRequest();
        //     if (type.Name.Equals("null")) return NotFound();
        //     return Created($"api/pokemons/{pokemonId}/types/{type.Id}", type);
        // }
        
    }
}