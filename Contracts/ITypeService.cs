using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ITypeService
    {
        Task<IEnumerable<TypeDto>> GetAllTypesOfPokemon(int pokemonId); 
        // TypeDto PostType(int pokemonId, TypeForCreationDto typeForCreation);
        Task<bool> DeleteType(int pokemonId);
        
    }
}