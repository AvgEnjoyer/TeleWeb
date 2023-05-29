using FluentValidation;
using TeleWeb.Application.DTOs;

namespace TeleWeb.Validation;

public class ChannelValidator : AbstractValidator<UpdateChannelDTO>
{
    public ChannelValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).MaximumLength(500);
    }
}