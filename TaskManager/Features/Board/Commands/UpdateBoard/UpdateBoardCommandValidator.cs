using FluentValidation;

namespace TaskManager.Features.Board.Commands.UpdateBoard
{
    public class UpdateBoardCommandValidator : AbstractValidator<UpdateBoardCommand>
    {
        public UpdateBoardCommandValidator()
        {
            RuleFor(x => x.BoardId).NotEmpty().WithMessage("BoardId is required.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Board title is required.");
            // Tambahkan validasi tambahan sesuai kebutuhan.
        }
    }
}
