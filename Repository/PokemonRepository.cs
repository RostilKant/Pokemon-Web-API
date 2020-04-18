using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities;
using Entities.JsonModels;
using HttpServices;
using RestSharp;
using Pokemon = Entities.Models.Pokemon;

namespace Repository
{
    public class PokemonRepository : RepositoryBase<Pokemon>, IPokemonRepository
    {
        private readonly PokeApiRestClient _client;
        public PokemonRepository(RepositoryContext repositoryContext, PokeApiRestClient client) : base(repositoryContext)
        {
            _client = client;
        }

        public RootObject GetAllPoke()
        {
            return _client.GetPokemons();
        }

        public IEnumerable<Pokemon> GetPoke()
        {
            throw new System.NotImplementedException();
        }
    }
}