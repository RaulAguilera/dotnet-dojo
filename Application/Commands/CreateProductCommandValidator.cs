using FluentValidation;

namespace Application.Commands
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(p => p.Description)
                .MaximumLength(512);
            RuleFor(p => p.NumberInStock)
                .GreaterThanOrEqualTo(0);
            RuleFor(p => p.Sku)
                .NotEmpty()
                .MaximumLength(12);
            RuleFor(p => p.Cost)
                .GreaterThan(0);
            RuleFor(p => p.Category)
                .NotEmpty()
                .MaximumLength(32);

        }
    }
}
