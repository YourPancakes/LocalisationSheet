using FluentValidation;
using LocalisationSheet.Server.Application.DTOs;

namespace LocalisationSheet.Server.Validators
{
    public class CreateLanguageDtoValidator : AbstractValidator<CreateLanguageDto>
    {
        public CreateLanguageDtoValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code is required.")
                .Length(2, 5).WithMessage("Code length 2–5.")
                .Matches("^[a-z]{2}(-[A-Z]{2})?$")
                .WithMessage("Use ISO-639-1 (e.g. en or en-US).");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100);
        }
    }
}