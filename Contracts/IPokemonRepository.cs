using System.Collections.Generic;
using Entities.JsonModels;
using Entities.Models;
using RestSharp;
using Pokemon = Entities.Models.Pokemon;

namespace Contracts
{
    public interface IPokemonRepository
    {
        RootObject GetAllPoke();
        IEnumerable<Pokemon> GetPoke();
    }
}