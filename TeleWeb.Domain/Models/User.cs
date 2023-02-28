using System.ComponentModel.DataAnnotations;

namespace TeleWeb.Domain.Models
{

    public class User
    {
        [Key]
        public int Id { get; private set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        [Required]
        public DateTime DateOfBirth { get; set; }
        public string? TelegramId { get; set; }

        public ICollection<Channel>? Subscriptions { get; set; }
        
    }
}
