namespace Contracts
{
    public interface IRepositoryManager
    {
        IPokemonRepository Pokemon { get; }
        void Save();
    }
}