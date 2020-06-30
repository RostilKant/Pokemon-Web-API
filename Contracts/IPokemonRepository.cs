using System.Collections.Generic;
using Entities.Models;

namespace Contracts
{
    public interface IPokemonRepository
    {
        IEnumerable<Pokemon> GetAllPokemons(bool trackChanges);
        Pokemon GetPokemon(int pokemonId, bool trackChanges);
        IEnumerable<Pokemon> GetPokemonsByIds(IEnumerable<int> ids, bool trackchanges);

        void CreatePokemon(Pokemon pokemon);
    }
}