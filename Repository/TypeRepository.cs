using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class TypeRepository: RepositoryBase<Type>, ITypeRepository
    {
        public TypeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Type>> GetAllTypesAsync(int pokemonId, bool trackChanges) => await 
            FindByCondition(t => t.PokemonId.Equals(pokemonId), trackChanges)
                .OrderBy(t => t.Name)
                .ToListAsync();

        public void DeleteType(Type type) => Delete(type);
        
        // public void CreateType(int pokemonId, Type type)
        // {
        //     type.PokemonId = pokemonId;
        //     Create(type);
        // }
    }
}