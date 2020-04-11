using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class PokemonRepository : RepositoryBase<Pokemon>, IPokemonRepository
    {
        public PokemonRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}