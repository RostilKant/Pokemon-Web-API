using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Entities;
using Entities.GETAllFromPokeApi;
using Entities.LinkModels;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;

namespace Contracts
{
    public interface IPokemonService
    {
        NewRootObject GetAllFromPokeApi();
        Entities.GetPokemonsFromPokeApi.Pokemon  GetByIdFromPokeApi(string pokeId);
        public string GetPokemonFrontView(string pokeId);
        Task<IEnumerable<PokemonDto>> FindAllPokemonsAsync(PokemonParameters pokemonParameters, HttpResponse response);
        Task<PokemonDto> FindPokemonByIdAsync(int pokemonId);

        Task<IEnumerable<PokemonDto>> FindPokemonsByIdsAsync(IEnumerable<int> ids);

        Task<PokemonDto> PostPokemonAsync(PokemonForCreationDto pokemonForCreationDto);
        Task<IEnumerable<PokemonDto>> PostPokemonCollectionAsync(IEnumerable<PokemonForCreationDto> pokemonForCreation);

        Task DeletePokemonAsync(int pokemonId);

        Task UpdatePokemonAsync(int pokemonId, PokemonForUpdateDto pokemonForUpdate);

        Task<PokemonForUpdateDto> PartiallyUpdatePokemonAsync(int pokemonId, JsonPatchDocument<PokemonForUpdateDto> patchDoc);
        Task SaveAndMapAsync(int pokemonId, PokemonForUpdateDto pokemonForUpdateDto);

        public LinkResponse GenerateLinksOrShapePokemons(IEnumerable<PokemonDto> pokemons,
            PokemonParameters pokemonParameters, HttpContext context);
    }
}