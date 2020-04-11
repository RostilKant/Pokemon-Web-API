using System.Security.Cryptography.X509Certificates;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<Pokemon> Pokemons { get; set; }
    }
}