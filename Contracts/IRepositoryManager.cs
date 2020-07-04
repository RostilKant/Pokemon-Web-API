using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IPokemonRepository Pokemon { get; }
        ITypeRepository Type { get; }
        Task Save();
    }
}