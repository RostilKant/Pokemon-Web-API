using Contracts;
using Entities;
using HttpServices;

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

        public IPokemonRepository Pokemon
        {
            get { return _pokemonRepository ??= new PokemonRepository(RepositoryContext); }
        }

        public void Save() => RepositoryContext.SaveChanges();
    }
}