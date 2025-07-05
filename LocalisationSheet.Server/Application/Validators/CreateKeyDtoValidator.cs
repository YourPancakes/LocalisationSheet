using FluentValidation;
using LocalisationSheet.Server.Application.DTOs;

namespace LocalisationSheet.Server.Validators
{
    public class CreateKeyDtoValidator : AbstractValidator<CreateKeyDto>
    {
        public CreateKeyDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Key name is required.")
                .Matches(@"^[A-Za-z0-9_.]+$")
                    .WithMessage("Allowed: letters, digits, underscore, dot.")
                .MaximumLength(200);
        }
    }
}