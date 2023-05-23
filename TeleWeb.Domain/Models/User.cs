using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

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
        
    }
}
