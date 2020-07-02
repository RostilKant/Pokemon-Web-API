using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Contracts;
using Entities.GETAllFromPokeApi;
using Entities.Models;
using HttpServices;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;

namespace Services
{
    public class PokemonService : IPokemonService
    {
        private readonly PokeApiRestClient _client;
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public PokemonService(PokeApiRestClient client, IRepositoryManager repositoryManager,
            ILogger<PokemonService> logger, IMapper mapper)
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

            if (poke == null) _logger.LogInformation($"Pokemon with id: {pokeId} doesn't exist on the poke.api.");
            return poke;
        }

        public IEnumerable<PokemonDto> FindAllPokemons()
        {
            var pokemons = _repositoryManager.Pokemon.GetAllPokemons(false);

            if (pokemons == null) _logger.LogInformation($"Pokemons doesn't exist in the DB.");

            return _mapper.Map<IEnumerable<PokemonDto>>(pokemons);

        }

        public PokemonDto FindPokemonById(int pokemonId)
        {
            var pokemon = _repositoryManager.Pokemon.GetPokemon(pokemonId, false);

            if (pokemon == null) _logger.LogInformation($"Company with id: {pokemonId} doesn't exist in the database.");

            return _mapper.Map<PokemonDto>(pokemon);
        }

        public IEnumerable<PokemonDto> FindPokemonsByIds(IEnumerable<int> ids)
        {
            var pokemons = _repositoryManager.Pokemon.GetPokemonsByIds(ids, false);
            if (ids.Count() != pokemons.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return null;
            }

            var pokemonsToReturn = _mapper.Map<IEnumerable<PokemonDto>>(pokemons);
            return pokemonsToReturn;
        }

        public PokemonDto PostPokemon(PokemonForCreationDto pokemonForCreationDto)
        {
            var pokemonEntity = _mapper.Map<Pokemon>(pokemonForCreationDto);
            _repositoryManager.Pokemon.CreatePokemon(pokemonEntity);
            _repositoryManager.Save();
            return _mapper.Map<PokemonDto>(pokemonEntity);
        }

        public IEnumerable<PokemonDto> PostPokemonCollection(IEnumerable<PokemonForCreationDto> pokemonForCreation)
        {
            if (pokemonForCreation == null) _logger.LogError("Pokemon collection sent from client is null.");

            var pokemons = _mapper.Map<IEnumerable<Pokemon>>(pokemonForCreation);
            foreach (var pokemon in pokemons) _repositoryManager.Pokemon.CreatePokemon(pokemon);
            _repositoryManager.Save();
            var pokemonsToReturn = _mapper.Map<IEnumerable<PokemonDto>>(pokemons);
            return pokemonsToReturn;
        }

        public bool DeletePokemon(int pokemonId)
        {
            var pokemon = _repositoryManager.Pokemon.GetPokemon(pokemonId, false);
            if (pokemon == null)
            {
                _logger.LogInformation($"Pokemon with id {pokemonId} doesn't exists on DB.");
                return false;
            }
            
            _repositoryManager.Pokemon.DeletePokemon(pokemon);
            _repositoryManager.Save();
            return true;
        }

        public bool UpdatePokemon(int pokemonId, PokemonForUpdateDto pokemonForUpdate)
        {
            var pokemon = _repositoryManager.Pokemon.GetPokemon(pokemonId, true);
            if (pokemon == null)
            {
                _logger.LogInformation($"Pokemon with id: {pokemonId} doesn't exist in the database.");
                return false;
            }

            _mapper.Map(pokemonForUpdate, pokemon);
            _repositoryManager.Save();
            return true;
        }

        public PokemonForUpdateDto PartiallyUpdatePokemon(int pokemonId, JsonPatchDocument<PokemonForUpdateDto> patchDoc)
        {
            var pokemon = _repositoryManager.Pokemon.GetPokemon(pokemonId, false);
            if (pokemon == null)
            {
                _logger.LogInformation($"Pokemon with id {pokemonId} doesn't exists in DB.");
                return null;
            }

            var pokemonEntity = _repositoryManager.Pokemon.GetPokemon(pokemonId, true);
            
            var pokemonToPatch = _mapper.Map<PokemonForUpdateDto>(pokemonEntity);
            
            return pokemonToPatch;
        }

        public void SaveAndMap(int pokemonId, PokemonForUpdateDto pokemonForUpdateDto)
        {
            var pokemonEntity = _repositoryManager.Pokemon.GetPokemon(pokemonId, true);
            
            var pokemonToPatch = _mapper.Map<PokemonForUpdateDto>(pokemonEntity);

            _mapper.Map(pokemonToPatch, pokemonEntity);
            _repositoryManager.Save();
        }
    }
}
