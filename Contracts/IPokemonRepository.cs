using System.Collections.Generic;
using Entities.Models;
using Pokemon = Entities.Models.Pokemon;

namespace Contracts
{
    public interface IPokemonRepository
    {
        IEnumerable<Pokemon> GetAllPokemons(bool trackChanges);
        Pokemon GetPokemon(int pokemonId, bool trackChanges);
    }
}