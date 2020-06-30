using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Type
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        
        [ForeignKey(nameof(Pokemon))]
        public int PokemonId { get; set; }
        public Pokemon Pokemon { get; set; }
    }

    public class TypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TypeForCreationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    // public class RootType
    // {
    //     public IEnumerable<Type> Type;
    // }

    // public class TypeForCreationDto
    // {
    //     public RootType Types;
    // }
}