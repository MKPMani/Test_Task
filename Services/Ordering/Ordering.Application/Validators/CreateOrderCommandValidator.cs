using FluentValidation;
using Ordering.Application.Commands;

namespace Ordering.Application.Validators
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(o => o.UserId)
                .NotEmpty()
                .WithMessage("{UserId} is required.")
                .NotNull()
                .MaximumLength(70)
                .WithMessage("{UserId} must be Gui Id.");
            RuleFor(o => o.Price)
                .NotEmpty()
                .WithMessage("{Price} is required.")
                .GreaterThan(-1)
                .WithMessage("{Price} should not be -ve");
            RuleFor(o => o.Product)
                .NotEmpty()
                .WithMessage("{Product} is required");
            RuleFor(o => o.Quantity)
                .NotEmpty()
                .NotNull()
                .WithMessage("{Quantity} is required");
        }
    }
}
