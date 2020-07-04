using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IPokemonRepository
    {
        Task<IEnumerable<Pokemon>>  GetAllPokemons(bool trackChanges);
        Task<Pokemon> GetPokemon(int pokemonId, bool trackChanges);
        Task<IEnumerable<Pokemon>> GetPokemonsByIds(IEnumerable<int> ids, bool trackchanges);

        void CreatePokemon(Pokemon pokemon);
        void DeletePokemon(Pokemon pokemon);
    }
}