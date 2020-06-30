using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.GETAllFromPokeApi;
using Entities.Models;

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
    }
}