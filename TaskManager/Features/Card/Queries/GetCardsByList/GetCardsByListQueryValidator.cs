using FluentValidation;
using TaskManager.Features.Card.Queries;
using System;

namespace TaskManager.Features.Card.Queries.GetCardsByLIst
{
    public class GetCardsByListQueryValidator : AbstractValidator<GetCardsByListQuery>
    {
        public GetCardsByListQueryValidator()
        {
            RuleFor(x => x.ListId).NotEmpty().WithMessage("ListId is required.");
        }
    }
}
