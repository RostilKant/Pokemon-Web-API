using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Models
{
    public class TypeConfiguration : IEntityTypeConfiguration<Type>
    {
        public void Configure(EntityTypeBuilder<Type> builder)
        {
            builder.HasData(
                new Type
                {
                    Id = 1,
                    Name = "grass",
                    PokemonId = 1
                },
                new Type
                {
                    Id = 2,
                    Name = "poison",
                    PokemonId = 1
                }
            );
        }
    }
    public class PokemonConfiguration : IEntityTypeConfiguration<Pokemon>
    {
        public void Configure(EntityTypeBuilder<Pokemon> builder)
        {
            builder.HasData(
                new Pokemon
                {
                    Id = 1,
                    Height = 64,
                    Name = "bulbasaur",
                    Weight = 22
                }
            );
        }
    }
}