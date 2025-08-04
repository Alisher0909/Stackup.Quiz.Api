using FluentValidation;
using Stackup.Quiz.Api.Dtos;

namespace Stackup.Quiz.Api.Validators;

public class CreateQuizDtoValidator : AbstractValidator<CreateQuizDto>
{
    public CreateQuizDtoValidator()
    {
        ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Title)
            .MinimumLength(4)
            .MaximumLength(100);
        RuleFor(x => x.Description)
            .MaximumLength(100);
        RuleFor(x => x.StartsAt)
            .NotEmpty()
            .When(x => x.EndsAt is not null);
        RuleFor(x => x.EndsAt)
            .NotEmpty()
            .When(x => x.StartsAt is not null);
        When(x => x.IsPrivate, () =>
        {
            RuleFor(x => x.Password)
                .NotEmpty()
                .Length(6)
                .Must(x => x is not null && x.All(char.IsAsciiLetterOrDigit))
                .WithMessage("'{PropertyName}' must be alpha-numeric.");
        });
    }
}