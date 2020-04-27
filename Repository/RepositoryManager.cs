using Contracts;
using Entities;
using HttpServices;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext RepositoryContext { get; }
        //private PokeApiRestClient RestClient { get; }
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
            //RestClient = restClient;
        }

        private IPokemonRepository _pokemonRepository;

        public IPokemonRepository Pokemon
        {
            get { return _pokemonRepository ??= new PokemonRepository(RepositoryContext); }
        }

        public void Save() => RepositoryContext.SaveChanges();
    }
}