using System.Collections;
using System.Collections.Generic;
using Entities.Models;

namespace Contracts
{
    public interface ITypeService
    {
        IEnumerable<TypeDto> GetAllTypesOfPokemon(int pokemonId); 
        // TypeDto PostType(int pokemonId, TypeForCreationDto typeForCreation);
    }
}