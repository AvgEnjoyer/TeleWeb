using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace TeleWeb.Application.DTOs
{
    public class AccountRegisterDTO 
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_\- ]{3,50}$", ErrorMessage = "Name must have from 3 to 20 characters in it")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^@[a-zA-Z0-9_]{5,20}$", ErrorMessage = "Username must start with @ and have from 5 to 20 characters after it")]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
