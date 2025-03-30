using FluentValidation;

namespace TaskManager.Features.Board.Commands.CreateBoard
{
    public class CreateBoardCommandValidator : AbstractValidator<CreateBoardCommand>
    {
        public CreateBoardCommandValidator()
        {
            RuleFor(x => x.OrgId)
                .NotEmpty().WithMessage("OrgId is required.");
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Board title is required.");
            // Tambahkan validasi lain sesuai kebutuhan, misalnya validasi untuk URL gambar
        }
    }
}
