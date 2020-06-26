namespace Contracts
{
    public interface IRepositoryManager
    {
        IPokemonRepository Pokemon { get; }
        ITypeRepository Type { get; }
        void Save();
    }
}