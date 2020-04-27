using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Name is a required field.")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Height is a required field.")]
        public int Height { get; set; }
        
        [Required(ErrorMessage = "Weight name is a required field.")]
        public int Weight { get; set; }
        
        
        public ICollection<Type> Types { get; set; }
    }
}