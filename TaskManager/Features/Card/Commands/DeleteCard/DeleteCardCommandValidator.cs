using FluentValidation;
using TaskManager.Features.Card.Commands.DeleteCard;

namespace TaskManager.Features.Card.Validators
{
    public class DeleteCardCommandValidator : AbstractValidator<DeleteCardCommand>
    {
        public DeleteCardCommandValidator()
        {
            RuleFor(x => x.CardId).NotEmpty().WithMessage("CardId is required.");
        }
    }
}
