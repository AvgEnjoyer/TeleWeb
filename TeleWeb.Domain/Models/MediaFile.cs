using System.ComponentModel.DataAnnotations;

namespace TeleWeb.Domain.Models
{
    public class MediaFile
    {
        [Key]
        public Guid Id { get; private set; }
        [Required]
        public string Url { get; set; } = string.Empty;
        [Required]
        
        [RegularExpression("^(?i)(Image|Video|Audio|Document)$")]
        public string Type { get; set; }

        [Required]
        public Post Post { get; set; } = new ();
    }
}
