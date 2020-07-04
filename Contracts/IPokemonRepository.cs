using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IPokemonRepository
    {
        Task<IEnumerable<Pokemon>>  GetAllPokemonsAsync(bool trackChanges);
        Task<Pokemon> GetPokemonAsync(int pokemonId, bool trackChanges);
        Task<IEnumerable<Pokemon>> GetPokemonsByIdsAsync(IEnumerable<int> ids, bool trackchanges);

        void CreatePokemon(Pokemon pokemon);
        void DeletePokemon(Pokemon pokemon);
    }
}