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
}