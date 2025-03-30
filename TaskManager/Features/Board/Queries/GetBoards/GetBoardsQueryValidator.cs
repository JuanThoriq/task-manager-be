using FluentValidation;

namespace TaskManager.Features.Board.Queries.GetBoards
{
    public class GetBoardsQueryValidator : AbstractValidator<GetBoardsQuery>
    {
        public GetBoardsQueryValidator()
        {
            RuleFor(x => x.OrgId).NotEmpty().WithMessage("OrgId is required.");
        }
    }
}
