﻿using FluentValidation;

namespace TaskManager.Features.List.Commands.UpdateList
{
    public class UpdateListCommandValidator : AbstractValidator<UpdateListCommand>
    {
        public UpdateListCommandValidator()
        {
            RuleFor(x => x.ListId).NotEmpty().WithMessage("ListId is required.");
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("List title is required.")
                .MinimumLength(3).WithMessage("Board title must be at least 3 characters.");
            RuleFor(x => x.Order)
                .GreaterThanOrEqualTo(0).WithMessage("Order must be zero or positive.");
        }
    }
}
