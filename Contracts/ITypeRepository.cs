using System.Collections;
using System.Collections.Generic;
using Entities.Models;

namespace Contracts
{
    public interface ITypeRepository
    {
        IEnumerable<Type> GetAllTypes(int pokemonId, bool trackChanges);
    }
}