using FluentValidation;
using FluentValidation.Validators;
using TeleWeb.Application.DTOs;

namespace TeleWeb.Validation
{
    public class UpdatePostDTOValidator : AbstractValidator<UpdatePostDTO>
    {
        public UpdatePostDTOValidator()
        {
            RuleForEach(x => x.MediaFileDTOs).SetValidator(new MediaFileDTOValidator());
        }
    }

    public class MediaFileDTOValidator : AbstractValidator<MediaFileDTO>
    {
        public MediaFileDTOValidator()
        {
            RuleFor(x => x.Url).NotEmpty().WithMessage("URL is required.");
            RuleFor(x => x.Url).Matches(@"^(https?|ftp)://[^\s/$.?#].[^\s]*$").WithMessage("Invalid URL format.");
            RuleFor(x => x.Type).IsInEnum().WithMessage("Invalid media file type.");
        }
    }
}