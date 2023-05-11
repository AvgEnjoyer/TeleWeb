using System.ComponentModel.DataAnnotations;
namespace TeleWeb.Domain.Models
{
    public class Channel
    {
        [Key]
        public int Id { get;  init; }
        [Required]
        public string Name { get; set; } = String.Empty;
        public string? Description { get; set; }
        public int? SubscribersCount { get; set; }

        [Required]
        public Admin PrimaryAdmin { get; set; } = new();

        public ICollection<User>? Subscribers { get; set; }
        public ICollection<Post>? Posts { get; set; }
        [Required]
        public ICollection<Admin> Admins { get; set; } = new List<Admin>();
        
    }
}
