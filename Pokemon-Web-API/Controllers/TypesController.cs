using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            if (types == null || !types.Any()) return NotFound();
            return Ok(types);
        }

        [HttpDelete]
        public IActionResult DeleteTypes(int pokemonId)
        {
            var type = _typeService.DeleteType(pokemonId);
            if (!type) return NotFound();
            return NoContent();
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