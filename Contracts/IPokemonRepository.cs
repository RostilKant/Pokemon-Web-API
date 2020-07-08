using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;

namespace Contracts
{
    public interface IPokemonRepository
    {
        Task<PagedList<Pokemon>>  GetAllPokemonsAsync(PokemonParameters pokemonParameters ,bool trackChanges);
        Task<Pokemon> GetPokemonAsync(int pokemonId, bool trackChanges);
        Task<IEnumerable<Pokemon>> GetPokemonsByIdsAsync(IEnumerable<int> ids, bool trackchanges);

        void CreatePokemon(Pokemon pokemon);
        void DeletePokemon(Pokemon pokemon);
    }
}