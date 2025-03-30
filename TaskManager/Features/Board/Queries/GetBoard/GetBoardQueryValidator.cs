using FluentValidation;

namespace TaskManager.Features.Board.Queries.GetBoard
{
    public class GetBoardQueryValidator : AbstractValidator<GetBoardQuery>
    {
        public GetBoardQueryValidator()
        {
            RuleFor(x => x.BoardId).NotEmpty().WithMessage("BoardId is required.");
        }
    }
}
