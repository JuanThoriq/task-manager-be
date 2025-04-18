﻿using FluentValidation;

namespace TaskManager.Features.List.Commands.CreateList
{
    public class CreateListCommandValidator : AbstractValidator<CreateListCommand>
    {
        public CreateListCommandValidator()
        {
            RuleFor(x => x.BoardId)
                .NotEmpty().WithMessage("BoardId is required.");
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("List title is required.")
                .MinimumLength(3).WithMessage("Board title must be at least 3 characters.");
            RuleFor(x => x.Order)
                .GreaterThanOrEqualTo(0).WithMessage("Order must be zero or positive.");
        }
    }
}
