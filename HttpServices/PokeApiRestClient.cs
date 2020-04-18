using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.JsonModels;
using RestSharp;
using Pokemon = Entities.Models.Pokemon;

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

        public RootObject GetPokemons()
        {
            var request = new RestRequest("pokemon?limit=965",DataFormat.Json);
            return RestClient.Get<RootObject>(request).Data;
        }

       // public IEnumerable<Pokemon> GetPokemon()
       // {
            
        //}
    }
}