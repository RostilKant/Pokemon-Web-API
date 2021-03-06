using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;


namespace Repository
{
    public class PokemonRepository : RepositoryBase<Pokemon>, IPokemonRepository
    {
        public PokemonRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        //public Entities.JsonModels.RootObject GetAllPoke() => _client.GetPokemons();

        //public Entities.GetPokemonModels.Pokemon GetPoke(int pokeId) => _client.GetPokemon(pokeId);
        public async Task<PagedList<Pokemon>> GetAllPokemonsAsync(PokemonParameters pokemonParameters, bool trackChanges)
        {
            var pokemons = await 
                FindAll(trackChanges)
                    .FilterByType(pokemonParameters.Type)
                    .SearchByName(pokemonParameters.Name)
                    .Sort(pokemonParameters.OrderBy)
                    .Include(p => p.Types)
                    .ToListAsync();
            
            return PagedList<Pokemon>.ToPagedList(pokemons, pokemonParameters.PageNumber,
                pokemonParameters.PageSize);
        }
        
        public async Task<Pokemon> GetPokemonAsync(int pokemonId, bool trackChanges) => await 
            FindByCondition(p => p.Id.Equals(pokemonId), trackChanges)
                .Include(p => p.Types)
                .SingleOrDefaultAsync();

        public async Task<IEnumerable<Pokemon>> GetPokemonsByIdsAsync(IEnumerable<int> ids, bool trackChanges) => await 
            FindByCondition(p => ids.Contains(p.Id), trackChanges)
                .ToListAsync();

        public void CreatePokemon(Pokemon pokemon) => Create(pokemon);
        public void DeletePokemon(Pokemon pokemon) => Delete(pokemon);
    }
}