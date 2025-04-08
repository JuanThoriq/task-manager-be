using FluentValidation;

namespace TaskManager.Features.Board.Commands.UpdateBoard
{
    public class UpdateBoardCommandValidator : AbstractValidator<UpdateBoardCommand>
    {
        public UpdateBoardCommandValidator()
        {
            RuleFor(x => x.BoardId).NotEmpty().WithMessage("BoardId is required.");
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Board title is required.")
                .MinimumLength(3).WithMessage("Board title must be at least 3 characters.");

            // Tambahkan validasi tambahan sesuai kebutuhan.
        }
    }
}
