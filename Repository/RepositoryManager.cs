using System.Threading.Tasks;
using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext RepositoryContext { get; }
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        private IPokemonRepository _pokemonRepository;
        private ITypeRepository _typeRepository;
        
        public ITypeRepository Type
        {
            get { return _typeRepository ??= new TypeRepository(RepositoryContext); }
        }

        public IPokemonRepository Pokemon
        {
            get { return _pokemonRepository ??= new PokemonRepository(RepositoryContext); }
        }

        public Task Save() => RepositoryContext.SaveChangesAsync();
    }
}