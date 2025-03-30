using FluentValidation;

namespace TaskManager.Features.Board.Commands.DeleteBoard
{
    public class DeleteBoardCommandValidator : AbstractValidator<DeleteBoardCommand>
    {
        public DeleteBoardCommandValidator()
        {
            RuleFor(x => x.BoardId).NotEmpty().WithMessage("BoardId is required.");
        }
    }
}
