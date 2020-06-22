using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using AutoMapper;
using Contracts;
using Entities.JsonModels;
using HttpServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Repository;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using Pokemon = Entities.Models.Pokemon;

namespace Pokemon_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonsController : ControllerBase
    {
        readonly PokeApiRestClient _client;
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public PokemonsController(IRepositoryManager repositoryManager, ILogger<PokemonsController> logger, IMapper mapper)
        {
            _client = new PokeApiRestClient();
            _repositoryManager = repositoryManager;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetPokemons()
        {
            
                var poke = _client.GetPokes();
                var pokemons = _mapper.Map<NewRootObject>(poke);
                throw new Exception("Exception");
                return Ok(pokemons);
        }

        [HttpGet("{pokeId}")]
        public IActionResult GetPokemon(string pokeId)
        {
            var poke = _client.GetPoke(pokeId);

            if (poke == null)
            {
                _logger.LogInformation($"Pokemon with id: {pokeId} doesn't exist on the poke.api.");
                return NotFound();

            }

            return Ok(poke);
        }
        
        
    }
}