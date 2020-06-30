using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Entities.GETAllFromPokeApi;
using Entities.Models;
using HttpServices;
using Microsoft.Extensions.Logging;

namespace Services
{
    public class PokemonService: IPokemonService
    {
        private readonly PokeApiRestClient _client;
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public PokemonService(PokeApiRestClient client, IRepositoryManager repositoryManager, ILogger<PokemonService> logger, IMapper mapper)
        {
            _client = client;
            _repositoryManager = repositoryManager;
            _logger = logger;
            _mapper = mapper;
        }
        public NewRootObject GetAllFromPokeApi()
        {
            var poke = _client.GetPokes();
            var pokemons = _mapper.Map<NewRootObject>(poke);
            return pokemons;
        }

        public Entities.GetPokemonsFromPokeApi.Pokemon GetByIdFromPokeApi(string pokeId)
        {
            Entities.GetPokemonsFromPokeApi.Pokemon poke = _client.GetPoke(pokeId);

            if (poke != null) return poke;
            _logger.LogInformation($"Pokemon with id: {pokeId} doesn't exist on the poke.api.");
            return null;

        }

        public IEnumerable<PokemonDto> FindAllPokemons()
        {
            var pokemons = _repositoryManager.Pokemon.GetAllPokemons(false);
            
            if (pokemons != null) return _mapper.Map<IEnumerable<PokemonDto>>(pokemons);
            _logger.LogInformation($"Pokemons doesn't exist in the DB.");
            return null;

        }

        public PokemonDto FindPokemonById(int pokemonId)
        {
            var pokemon = _repositoryManager.Pokemon.GetPokemon(pokemonId, false);

            if (pokemon != null) return _mapper.Map<PokemonDto>(pokemon);
            _logger.LogInformation($"Company with id: {pokemonId} doesn't exist in the database.");
            return null;
        }

        public PokemonDto PostPokemon(PokemonForCreationDto pokemonForCreationDto)
        {
            if (pokemonForCreationDto != null)
            {
                var pokemonEntity = _mapper.Map<Pokemon>(pokemonForCreationDto);
                _repositoryManager.Pokemon.CreatePokemon(pokemonEntity);
                _repositoryManager.Save();
                return _mapper.Map<PokemonDto>(pokemonEntity);
            }

            _logger.LogInformation("PokemonForCreationDto object sent from client is null.");
            return null;
        }
    }
}