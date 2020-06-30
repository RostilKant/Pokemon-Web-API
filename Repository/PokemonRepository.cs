using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities;
using Entities.Models;


namespace Repository
{
    public class PokemonRepository : RepositoryBase<Pokemon>, IPokemonRepository
    {
        public PokemonRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        //public Entities.JsonModels.RootObject GetAllPoke() => _client.GetPokemons();

        //public Entities.GetPokemonModels.Pokemon GetPoke(int pokeId) => _client.GetPokemon(pokeId);
        public IEnumerable<Pokemon> GetAllPokemons(bool trackChanges) =>
            FindAll(trackChanges)
                .OrderBy(p => p.Name)
                .ToList();

        public Pokemon GetPokemon(int pokemonId, bool trackChanges) =>
            FindByCondition(p => p.Id.Equals(pokemonId), trackChanges)
                .SingleOrDefault();

        public void CreatePokemon(Pokemon pokemon) => Create(pokemon);
    }
}