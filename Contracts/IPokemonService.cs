using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Entities.GETAllFromPokeApi;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Contracts
{
    public interface IPokemonService
    {
        NewRootObject GetAllFromPokeApi();
        Entities.GetPokemonsFromPokeApi.Pokemon  GetByIdFromPokeApi(string pokeId);
        Task<IEnumerable<PokemonDto>> FindAllPokemons();
        Task<PokemonDto> FindPokemonById(int pokemonId);

        Task<IEnumerable<PokemonDto>> FindPokemonsByIds(IEnumerable<int> ids);

        Task<PokemonDto> PostPokemon(PokemonForCreationDto pokemonForCreationDto);
        Task<IEnumerable<PokemonDto>> PostPokemonCollection(IEnumerable<PokemonForCreationDto> pokemonForCreation);

        Task<bool> DeletePokemon(int pokemonId);

        Task<bool> UpdatePokemon(int pokemonId, PokemonForUpdateDto pokemonForUpdate);

        Task<PokemonForUpdateDto> PartiallyUpdatePokemon(int pokemonId, JsonPatchDocument<PokemonForUpdateDto> patchDoc);
        Task SaveAndMap(int pokemonId, PokemonForUpdateDto pokemonForUpdateDto);
    }
}