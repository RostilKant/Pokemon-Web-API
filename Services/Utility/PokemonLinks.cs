using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities.LinkModels;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;

namespace Services.Utility
{
    public class PokemonLinks
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IDataShaper<PokemonDto> _dataShaper;

        public PokemonLinks(LinkGenerator linkGenerator, IDataShaper<PokemonDto> dataShaper)
        {
            _linkGenerator = linkGenerator;
            _dataShaper = dataShaper;
        }

        public LinkResponse TryGenerateLinks(IEnumerable<PokemonDto> pokemons, string fields,
             HttpContext context)
        {
            var shapedPokemons = ShapeData(pokemons, fields).ToList();

            return ShouldGenerateLinks(context) ? 
                ReturnLinkedPokemons(pokemons, fields, context, shapedPokemons) : 
                ReturnShapedPokemons(shapedPokemons);
        }

        private IEnumerable<Entity> ShapeData(IEnumerable<PokemonDto> pokemons, string fields) =>
            _dataShaper.ShapeData(pokemons, fields)
                .Select(e => e.Entity);

        private static bool ShouldGenerateLinks(HttpContext context)
        {
            var mediaType =  (MediaTypeHeaderValue) context.Items["AcceptHeaderMediaType"];
            return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas",
                StringComparison.InvariantCultureIgnoreCase);
        }
        
        private static LinkResponse ReturnShapedPokemons(List<Entity> shapedPokemons) => 
            new LinkResponse{ ShapedObjects = shapedPokemons };

        private LinkResponse ReturnLinkedPokemons(IEnumerable<PokemonDto> pokemons, string fields,
            HttpContext context, List<Entity> shapedPokemons)
        {
            var pokemonsList = pokemons.ToList();

            for (var i = 0; i < pokemonsList.Count; i++)
            {
                var pokemonLinks = CreateLinksForPokemon(context, pokemonsList[i].Id, fields);
                shapedPokemons[i].Add("Links", pokemonLinks);
            }
            var pokemonCollection = new LinkCollectionWrapper<Entity>(shapedPokemons);
            var linkedPokemons = CreateLinksForPokemons(context, pokemonCollection);
            return new LinkResponse {HasLinks = true, LinkedShapedObjects = linkedPokemons};
        }

        private List<Link> CreateLinksForPokemon(HttpContext httpContext, 
            int pokemonId, string fields = "") => 
        new List<Link>
        {
            new Link(_linkGenerator.GetUriByAction(httpContext,"GetPokemonById",
                    "Pokemons", 
                    values: new{ pokemonId, fields }), 
                "self",
                "GET"),
            new Link(_linkGenerator.GetUriByAction(httpContext,"DeletePokemon", 
                    "Pokemons", 
                    values: new { pokemonId}),
                "delete_pokemon",
                "DELETE"),
            new Link(_linkGenerator.GetUriByAction(httpContext,"UpdatePokemon",
                    "Pokemons", 
                    values: new { pokemonId}),
                "update_pokemon",
                "UPDATE"),
            new Link(_linkGenerator.GetUriByAction(httpContext,"PartiallyUpdatePokemon", 
                    "Pokemons",
                    values: new { pokemonId}),
                "partially_update_pokemon",
                "PATCH")
        };

        private LinkCollectionWrapper<Entity> CreateLinksForPokemons(HttpContext context,
            LinkCollectionWrapper<Entity> pokemonWrapper)
        {
            pokemonWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(context,"GetPokemons",
                    "Pokemons", 
                    values: new{ }), 
                "self",
                "GET"));
            return pokemonWrapper;
        }

    }
}