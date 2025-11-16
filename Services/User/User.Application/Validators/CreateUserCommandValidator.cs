using FluentValidation;
using User.Application.Commands;

namespace User.Application.Validators;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(o => o.Name)
            .NotEmpty()
            .WithMessage("{Name} is required.")
            .NotNull()
            .MaximumLength(70)
            .WithMessage("{Name} length less than 70");
        RuleFor(o => o.Email)
            .NotEmpty()
            .WithMessage("{Email} is required.")
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
            .WithMessage("{Email} must be valid.")
            .NotNull()
            .MaximumLength(70)
            .WithMessage("{Email} length less than 70");
    }
}
