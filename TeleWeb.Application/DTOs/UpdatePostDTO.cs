namespace TeleWeb.Application.DTOs;

public class UpdatePostDTO
{
    public string Text { get; set; }
    public IEnumerable<MediaFileDTO>? MediaFileDTOs { get; set; }
}