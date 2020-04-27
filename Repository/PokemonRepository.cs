using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities;
using Entities.Models;
using HttpServices;
using RestSharp;

namespace Repository
{
    public class PokemonRepository : RepositoryBase<Pokemon>, IPokemonRepository
    {
       // private readonly PokeApiRestClient _client;
        public PokemonRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            //_client = client;
        }

        //public Entities.JsonModels.RootObject GetAllPoke() => _client.GetPokemons();

        //public Entities.GetPokemonModels.Pokemon GetPoke(int pokeId) => _client.GetPokemon(pokeId);
    }
}