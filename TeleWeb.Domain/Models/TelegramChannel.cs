using System.ComponentModel.DataAnnotations;

namespace TeleWeb.Domain.Models
{
    public class TelegramChannel: Channel
    {
        [Required]
        public string TelegramId { get; set; } = String.Empty;
        [Required]
        public string TelegramUsername { get; set; } = String.Empty;
    }
}
