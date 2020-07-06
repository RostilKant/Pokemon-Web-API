using System.Linq;
using Entities.Models;

namespace Repository.Extensions
{
    public static class PokemonExtensions
    {
        public static IQueryable<Pokemon> FilterByType(this IQueryable<Pokemon> pokemons, string type)
        {
            return string.IsNullOrWhiteSpace(type) ? pokemons : 
                pokemons.Where(p => p.Types.Any(t => t.Name.Equals(type)));
        }


        public static IQueryable<Pokemon> SearchingByName(this IQueryable<Pokemon> pokemons, string name)
        {
            return string.IsNullOrWhiteSpace(name) ? pokemons : 
                pokemons.Where(p => p.Name.Contains(name));
        }
    }
    
    
}