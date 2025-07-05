using FluentValidation;
using LocalisationSheet.Server.Application.DTOs;

namespace LocalisationSheet.Server.Validators
{
    public class UpdateLanguageDtoValidator : AbstractValidator<UpdateLanguageDto>
    {
        public UpdateLanguageDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code is required.")
                .Length(2, 5).WithMessage("Code length 2–5.")
                .Matches("^[a-z]{2}(-[A-Z]{2})?$")
                .WithMessage("Use ISO-639-1 (e.g. en or en-US).");
        }
    }
}