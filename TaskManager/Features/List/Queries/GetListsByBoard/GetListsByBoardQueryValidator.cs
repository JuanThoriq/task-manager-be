using FluentValidation;
using TaskManager.Features.List.Queries;
using System;

namespace TaskManager.Features.List.Queries.GetListsByBoard
{
    public class GetListsByBoardQueryValidator : AbstractValidator<GetListsByBoardQuery>
    {
        public GetListsByBoardQueryValidator()
        {
            RuleFor(x => x.BoardId).NotEmpty().WithMessage("BoardId is required.");
        }
    }
}
