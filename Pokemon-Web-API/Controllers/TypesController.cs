using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Pokemon_Web_API.Controllers
{
    [Route("api/pokemons/{pokemonId}/types")]
    public class TypesController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public TypesController(IRepositoryManager repositoryManager, ILogger<PokemonsController> logger, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetTypes(int pokemonId)
        {
            var pokemon = _repositoryManager.Pokemon.GetPokemon(pokemonId, false);
            if (pokemon == null)
            {
                _logger.LogInformation($"Pokemon with Id {pokemonId} doesn't exists in DB.");
                return NotFound();
            }
            else
            {
                var types = _repositoryManager.Type.GetAllTypes(pokemonId, false);
                var typesDto = _mapper.Map<IEnumerable<TypeDto>>(types);
                return Ok(typesDto);
            }
        }
        
    }
}