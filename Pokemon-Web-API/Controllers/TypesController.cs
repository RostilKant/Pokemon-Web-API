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

        [HttpGet]
        public IActionResult GetTypes(int pokemonId)
        {
            var types = _typeService.GetTypesOfPokemon(pokemonId);
            if (types == null) return NotFound();
            return Ok(types);
        }
        
    }
}