using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TeleWeb.Domain.Models
{

    public class UserI : IdentityUser
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public DateTime DateOfBirth { get; set; }
        public string? TelegramId { get; set; }
        
        public ICollection<Channel>? Subscriptions { get; set; }
        
    }
}
