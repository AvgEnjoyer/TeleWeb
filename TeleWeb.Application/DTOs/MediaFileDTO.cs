using System.ComponentModel.DataAnnotations;

namespace TeleWeb.Application.DTOs;

public class MediaFileDTO
{
    public string Url { get; set; } = string.Empty;
    [RegularExpression("^(?i)(Image|Video|Audio|Document)$")]
    public string Type { get; set; }

}