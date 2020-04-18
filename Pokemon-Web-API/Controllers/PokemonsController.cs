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
            try
            {
                var pokemons = _repositoryManager.Pokemon.GetAllPoke();
                var pokemons1 = _mapper.Map<NewRootObject>(pokemons);

                return Ok(pokemons1);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetPokemons)} action {ex}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        
    }
}