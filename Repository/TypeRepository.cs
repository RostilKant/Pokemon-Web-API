using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class TypeRepository: RepositoryBase<Type>, ITypeRepository
    {
        public TypeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Type> GetAllTypes(int pokemonId, bool trackChanges) =>
            FindByCondition(t => t.PokemonId.Equals(pokemonId), trackChanges)
                .OrderBy(t => t.Name)
                .ToList();

        public void DeleteType(Type type) => Delete(type);
        public void CreateType(int pokemonId, Type type)
        {
            type.PokemonId = pokemonId;
            Create(type);
        }
    }
}