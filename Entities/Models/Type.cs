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
}