
using System.Collections.ObjectModel;


namespace TeleWeb.Application.DTOs
{
    public class GetPostDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        
        public int likes { get; set; }
        public int dislikes { get; set; }
        
        public  Collection<MediaFileDTO> MediaFilesDTOs { get; set; }
    }
}
