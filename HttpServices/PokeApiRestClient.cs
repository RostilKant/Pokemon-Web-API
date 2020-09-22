using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.GETAllFromPokeApi;
using RestSharp;
using Pokemon = Entities.GetPokemonsFromPokeApi.Pokemon;

namespace HttpServices
{
    public class PokeApiRestClient
    {
        public IRestClient RestClient { get; set; }
        private const string BaseUrl = "https://pokeapi.co/api/v2/";
        
        public PokeApiRestClient()
        {
            RestClient = new RestClient(BaseUrl);
        }

        public RootObject GetPokes()
        {
            var request = new RestRequest("pokemon?limit=807",DataFormat.Json);
            return RestClient.Get<RootObject>(request).Data;
        }

        public Pokemon GetPoke(string pokeId)
        {
            var request = new RestRequest($"pokemon/{pokeId}",DataFormat.Json);
            return RestClient.Get<Pokemon>(request).Data;
        }
        
        //public Entities.GetPokemonModels.Pokemon GetPoke(string poke)
       // {
       //     var request = new RestRequest($"pokemon/{poke}",DataFormat.Json);
       //     return RestClient.Get<Entities.GetPokemonModels.Pokemon>(request).Data;
       // }
        
        
    }
}