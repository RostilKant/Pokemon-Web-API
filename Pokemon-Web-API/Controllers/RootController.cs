using System.Collections.Generic;
using Entities.LinkModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Pokemon_Web_API.Controllers
{
    [Route("api")]
    [ApiController]
    public class RootController : ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;

        public RootController(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        [HttpGet(Name = "GetRoot")]
        public IActionResult GetRoot([FromHeader(Name = "Accept")] string mediaType)
        {
            if (!mediaType.Contains("application/vnd.rostil.apiroot")) return NoContent();
            var list = new List<Link>
            {
                new Link
                {
                    Href = _linkGenerator.GetUriByName(HttpContext, nameof(GetRoot), new{}),
                    Rel = "self",
                    Method = "GET"
                },
                new Link
                {
                    Href = _linkGenerator.GetUriByName(HttpContext, "GetPokemons", new{} ),
                    Rel = "pokemons",
                    Method = "GET"
                },
                new Link
                {
                    Href = _linkGenerator.GetUriByName(HttpContext, "CreatePokemon", new{} ),
                    Rel = "create_pokemon",
                    Method = "POST"
                }
            };
            return Ok(list);
        }
    }
}