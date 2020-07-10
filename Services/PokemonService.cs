using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities;
using Entities.GETAllFromPokeApi;
using Entities.LinkModels;
using Entities.Models;
using Entities.RequestFeatures;
using HttpServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Utility;

namespace Services
{
    public class PokemonService : IPokemonService
    {
        private readonly PokeApiRestClient _client;
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly PokemonLinks _pokemonLinks;

        public PokemonService(PokeApiRestClient client, IRepositoryManager repositoryManager,
            ILogger<PokemonService> logger, IMapper mapper, PokemonLinks pokemonLinks)
        {
            _client = client;
            _repositoryManager = repositoryManager;
            _logger = logger;
            _mapper = mapper;
            _pokemonLinks = pokemonLinks;
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

        public async Task<IEnumerable<PokemonDto>> FindAllPokemonsAsync(PokemonParameters pokemonParameters)
        {
            var pokemons = await _repositoryManager.Pokemon.GetAllPokemonsAsync(pokemonParameters,false);
            if (pokemons == null) _logger.LogInformation($"Pokemons doesn't exist in the DB.");

            return _mapper.Map<IEnumerable<PokemonDto>>(pokemons);

        }

        public async Task<PokemonDto> FindPokemonByIdAsync(int pokemonId)
        {
            var pokemon = await _repositoryManager.Pokemon.GetPokemonAsync(pokemonId, false);
            

            return _mapper.Map<PokemonDto>(pokemon);
        }

        public async Task<IEnumerable<PokemonDto>> FindPokemonsByIdsAsync(IEnumerable<int> ids)
        {
            var pokemons = await _repositoryManager.Pokemon.GetPokemonsByIdsAsync(ids, false);
            if (pokemons == null)
            {
                _logger.LogInformation($"Pokemon with id: {ids} doesn't exist in the database.");
                return null;
            }
            if (ids.Count() != pokemons.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return null;
            }

            var pokemonsToReturn = _mapper.Map<IEnumerable<PokemonDto>>(pokemons);
            return pokemonsToReturn;
        }

        public async Task<PokemonDto> PostPokemonAsync(PokemonForCreationDto pokemonForCreationDto)
        {
            var pokemonEntity = _mapper.Map<Pokemon>(pokemonForCreationDto);
            _repositoryManager.Pokemon.CreatePokemon(pokemonEntity);
            await _repositoryManager.Save();
            return _mapper.Map<PokemonDto>(pokemonEntity);
        }

        public async Task<IEnumerable<PokemonDto>> PostPokemonCollectionAsync(IEnumerable<PokemonForCreationDto> pokemonForCreation)
        {

            var pokemons = _mapper.Map<IEnumerable<Pokemon>>(pokemonForCreation);
            foreach (var pokemon in pokemons) _repositoryManager.Pokemon.CreatePokemon(pokemon);
            await _repositoryManager.Save();
            
            var pokemonsToReturn = _mapper.Map<IEnumerable<PokemonDto>>(pokemons);
            return pokemonsToReturn;
        }

        public async Task DeletePokemonAsync(int pokemonId)
        {
            var pokemon = await _repositoryManager.Pokemon.GetPokemonAsync(pokemonId, false);
          
            _repositoryManager.Pokemon.DeletePokemon(pokemon);
            await _repositoryManager.Save();
        }

        public async Task UpdatePokemonAsync(int pokemonId, PokemonForUpdateDto pokemonForUpdate)
        {
            var pokemon = await _repositoryManager.Pokemon.GetPokemonAsync(pokemonId, true);
            
            _mapper.Map(pokemonForUpdate, pokemon);
             await _repositoryManager.Save();
        }

        public async Task<PokemonForUpdateDto> PartiallyUpdatePokemonAsync(int pokemonId, JsonPatchDocument<PokemonForUpdateDto> patchDoc)
        {
            var pokemon = await _repositoryManager.Pokemon.GetPokemonAsync(pokemonId, false);
            if (pokemon == null)
            {
                _logger.LogInformation($"Pokemon with id {pokemonId} doesn't exists in DB.");
                return null;
            }

            var pokemonEntity = await _repositoryManager.Pokemon.GetPokemonAsync(pokemonId, true);
            
            var pokemonToPatch = _mapper.Map<PokemonForUpdateDto>(pokemonEntity);
            
            return pokemonToPatch;
        }

        public async Task SaveAndMapAsync(int pokemonId, PokemonForUpdateDto pokemonForUpdateDto)
        {
            var pokemonEntity = await _repositoryManager.Pokemon.GetPokemonAsync(pokemonId, true);
            
            var pokemonToPatch = _mapper.Map<PokemonForUpdateDto>(pokemonEntity);

            _mapper.Map(pokemonToPatch, pokemonEntity);
            await _repositoryManager.Save();
        }

        public LinkResponse GenerateLinksOrShapePokemons(IEnumerable<PokemonDto> pokemons,
            PokemonParameters pokemonParameters, HttpContext context) => 
            _pokemonLinks.TryGenerateLinks(pokemons, pokemonParameters.Fields, context);
            
        

    }
}
