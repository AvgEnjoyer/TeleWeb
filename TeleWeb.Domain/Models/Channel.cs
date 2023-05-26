using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeleWeb.Domain.Models
{
    public class Channel
    {
        [Key]
        public Guid Id { get;  init; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public int SubscribersCount { get; set; }

        [Required]
        public User PrimaryAdmin { get; set; } = new();
        
        public ICollection<User>? Subscribers { get; set; }
        public ICollection<Post>? Posts { get; set; }
        [Required]
        
        public ICollection<User> Admins { get; set; } = new List<User>();
        
    }
}
