using FluentValidation;
using TeleWeb.Application.DTOs;

namespace TeleWeb.Validation;

public class ChannelValidator : AbstractValidator<ChannelDTO>
{
    public ChannelValidator()
    {
        RuleFor(x => x.Id).Empty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).MaximumLength(50);
        //RuleFor(x => x.PrimaryAdmin).NotNull();
    }
}