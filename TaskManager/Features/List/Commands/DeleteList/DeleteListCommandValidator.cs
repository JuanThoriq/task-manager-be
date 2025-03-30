using FluentValidation;

namespace TaskManager.Features.List.Commands.DeleteList
{
    public class DeleteListCommandValidator : AbstractValidator<DeleteListCommand>
    {
        public DeleteListCommandValidator()
        {
            RuleFor(x => x.ListId).NotEmpty().WithMessage("ListId is required.");
        }
    }
}
