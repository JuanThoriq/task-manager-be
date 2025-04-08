using FluentValidation;

namespace TaskManager.Features.Card.Commands.UpdateCard
{
    public class UpdateCardCommandValidator : AbstractValidator<UpdateCardCommand>
    {
        public UpdateCardCommandValidator()
        {
            RuleFor(x => x.CardId).NotEmpty().WithMessage("CardId is required.");
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Card title is required.")
                .MinimumLength(3).WithMessage("Board title must be at least 3 characters.");
            RuleFor(x => x.Order)
                .GreaterThanOrEqualTo(0).WithMessage("Order must be zero or positive.");
        }
    }
}
