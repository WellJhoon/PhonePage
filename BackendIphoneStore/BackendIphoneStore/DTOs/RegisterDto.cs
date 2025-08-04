using System.ComponentModel.DataAnnotations;
using BackendIphoneStore.Models;

namespace BackendIphoneStore.DTOs
{
    public class RegisterDto
    {
        [Required]
        [StringLength(100)]
        public string Username { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        
        public UserRole Role { get; set; } = UserRole.User;
    }
}