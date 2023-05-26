using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeleWeb.Domain.Models
{

    public class User
    {
        [Key]
        public Guid Id { get;  set; }
        public Guid IdentityId { get; set; }
        [Required]
        public string Name { get; set; }
        
        public DateTime? DateOfBirth { get; set; }
        public string? TelegramId { get; set; }

        public ICollection<Channel>? Subscriptions { get; set; }
        
        public ICollection<Channel>? OwnedChannels { get; set; }
        
        public ICollection<Channel>? AdministratingChannels { get; set; }
        public ICollection<Post>? Posts { get; set; }
        
    }
}
