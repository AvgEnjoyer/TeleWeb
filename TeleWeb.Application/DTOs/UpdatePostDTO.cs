namespace TeleWeb.Application.DTOs;

public class UpdatePostDTO
{
    public string Text { get; set; }
    public ICollection<MediaFileDTO>? MediaFileDTOs { get; set; }
}