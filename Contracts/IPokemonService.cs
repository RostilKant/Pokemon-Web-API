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
        IEnumerable<PokemonDto> FindAllPokemons();
        PokemonDto FindPokemonById(int pokemonId);

        IEnumerable<PokemonDto> FindPokemonsByIds(IEnumerable<int> ids);

        PokemonDto PostPokemon(PokemonForCreationDto pokemonForCreationDto);
        IEnumerable<PokemonDto> PostPokemonCollection(IEnumerable<PokemonForCreationDto> pokemonForCreation);

        bool DeletePokemon(int pokemonId);

        bool UpdatePokemon(int pokemonId, PokemonForUpdateDto pokemonForUpdate);

        PokemonForUpdateDto PartiallyUpdatePokemon(int pokemonId, JsonPatchDocument<PokemonForUpdateDto> patchDoc);
        void SaveAndMap(int pokemonId, PokemonForUpdateDto pokemonForUpdateDto);
    }
}