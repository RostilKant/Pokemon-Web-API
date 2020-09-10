#nullable enable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Entities.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
    
    public class UserForRegistrationDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        
        public string? UserName { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        
        public string? PhoneNumber { get; set; }
        public ICollection<string>? Roles { get; set; }
    }
    
    public class UserForAuthenticationDto
    {
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        
        [Required(ErrorMessage = "Password name is required")]
        public string? Password { get; set; }

    }


}