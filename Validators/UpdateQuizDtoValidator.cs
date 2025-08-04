using FluentValidation;
using Stackup.Quiz.Api.Dtos;
using Stackup.Quiz.Api.Services.Abstractions;

namespace Stackup.Quiz.Api.Validators;

public class UpdateQuizDtoValidator : AbstractValidator<UpdateQuizDto>
{
    public UpdateQuizDtoValidator(IQuizService service)
    {
        ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Title)
            .MinimumLength(4)
            .MaximumLength(100)
            .MustAsync(async (title, token) => await service.ExistsAsync(title, token) is false)
            .WithMessage("Quiz title must be unique."); 
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