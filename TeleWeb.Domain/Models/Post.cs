using System.ComponentModel.DataAnnotations;

namespace TeleWeb.Domain.Models
{
    public class Post
    {
        [Key]
        public int Id { get; private set; }
        public string? Text { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public int likes { get; set; }
        public int dislikes { get; set; }

        [Required]
        public Channel Channel { get; set; } = new ();
        [Required]
        public Admin AdminWhoPosted { get; set; } = new ();
        
        public ICollection<MediaFile>? MediaFiles { get; set; }

        public ICollection<Post>? Comments{ get; set; }
    }
}
