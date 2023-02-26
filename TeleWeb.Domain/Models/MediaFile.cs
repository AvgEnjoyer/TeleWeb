using System.ComponentModel.DataAnnotations;

namespace TeleWeb.Domain.Models
{
    public class MediaFile
    {
        public enum FileType
        {
            Image,
            Video,
            Audio,
            Document
        }
        [Key]
        public int Id { get; private set; }
        [Required]
        public string Url { get; set; } = string.Empty;
        [Required]
        public FileType Type { get; }
        [Required]
        public Post Post { get; set; } = new ();
    }
}
