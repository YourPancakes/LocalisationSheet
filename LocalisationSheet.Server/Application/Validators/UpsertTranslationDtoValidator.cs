using FluentValidation;
using LocalisationSheet.Server.Application.DTOs;

namespace LocalisationSheet.Server.Validators
{
    public class UpsertTranslationDtoValidator : AbstractValidator<UpsertTranslationDto>
    {
        public UpsertTranslationDtoValidator()
        {
            RuleFor(x => x.KeyId)
                .NotEmpty().WithMessage("KeyId is required.");

            RuleFor(x => x.LanguageId)
                .NotEmpty().WithMessage("LanguageId is required.");

            RuleFor(x => x.Value)
                .NotEmpty().WithMessage("Value is required.")
                .MaximumLength(1000);
        }
    }
}