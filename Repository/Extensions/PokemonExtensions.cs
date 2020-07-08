using System;
using System.Globalization;
using System.Linq;
using Entities.Models;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace Repository.Extensions
{
    public static class PokemonExtensions
    {
        public static IQueryable<Pokemon> FilterByType(this IQueryable<Pokemon> pokemons, string type)
        {
            return string.IsNullOrWhiteSpace(type) ? pokemons : 
                pokemons.Where(p => p.Types.Any(t => t.Name.Equals(type)));
        }


        public static IQueryable<Pokemon> SearchByName(this IQueryable<Pokemon> pokemons, string name)
        {
            return string.IsNullOrWhiteSpace(name) ? pokemons : 
                pokemons.Where(p => p.Name.Contains(name));
        }

        public static IQueryable<Pokemon> Sort(this IQueryable<Pokemon> pokemons, string orderByQuery)
        {
            if (string.IsNullOrWhiteSpace(orderByQuery)) return pokemons.OrderBy(p => p.Name);

            var orderParams = orderByQuery.Trim().Split(',');

            var propInfos = typeof(Pokemon).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if(string.IsNullOrWhiteSpace(param)) continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propInfos.FirstOrDefault(pi =>
                    pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
                if(objectProperty == null) continue;

                var direction = param.EndsWith("desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name} {direction},");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            return string.IsNullOrWhiteSpace(orderQuery) ? 
                pokemons.OrderBy(p => p.Name) : 
                pokemons.OrderBy(orderQuery);
        }
    }
    
    
}