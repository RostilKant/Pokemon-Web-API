#nullable enable
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class Pokemon
    {
        [Column("PokemonId")]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Name is a required field.")]
        public string? Name { get; set; }
        
        [Required(ErrorMessage = "Height is a required field.")]
        public int? Height { get; set; }
        
        [Required(ErrorMessage = "Weight name is a required field.")]
        public int? Weight { get; set; }
        
        public ICollection<Type>? Types { get; set; }
    }
    public class PokemonDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public ICollection<TypeDto>? Types { get; set; }
    }

    public class PokemonForCreationDto : PokemonForManipulation { }
    public class PokemonForUpdateDto: PokemonForManipulation { }

    public abstract class PokemonForManipulation
    {
        [Required(ErrorMessage = "Name is a required field.")]
        public string? Name { get; set; }
        
        [Required(ErrorMessage = "Height is a required field.")]
        public int? Height { get; set; }
        
        [Required(ErrorMessage = "Weight is required")]
        [Range(18, int.MaxValue, ErrorMessage = "Age is required and it can't be lower than 18")]
        public int? Weight { get; set; }
        
        public IEnumerable<TypeForCreationDto>? Types { get; set; }
    }
    
}