using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ITypeRepository
    {
        Task<IEnumerable<Type>> GetAllTypesAsync(int pokemonId, bool trackChanges);
        void DeleteType(Type type);
    }
}