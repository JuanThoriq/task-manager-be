using FluentValidation;

namespace TaskManager.Features.Card.Queries.GetCard
{
    public class GetCardQueryValidator : AbstractValidator<GetCardQuery>
    {
        public GetCardQueryValidator()
        {
            RuleFor(x => x.CardId).NotEmpty().WithMessage("CardId is required.");
        }
    }
}
