using System.Collections;
using System.Collections.Generic;
using Entities.Models;

namespace Contracts
{
    public interface ITypeService
    {
        IEnumerable<TypeDto> GetTypesOfPokemon(int pokemonId);
    }
}