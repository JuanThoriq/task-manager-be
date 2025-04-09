using FluentValidation;

namespace TaskManager.Features.Card.Commands.CreateCard
{
    public class CreateCardCommandValidator : AbstractValidator<CreateCardCommand>
    {
        public CreateCardCommandValidator()
        {
            // RuleFor(x => x.ListId)
            //     .NotEmpty().WithMessage("ListId is required.");
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Card title is required.")
                .MinimumLength(3).WithMessage("Board title must be at least 3 characters.");
            RuleFor(x => x.Order)
                .GreaterThanOrEqualTo(0).WithMessage("Order must be zero or positive.");
            // Jika diperlukan, tambahkan validasi untuk Description (misalnya, maksimum panjang)
        }
    }
}
