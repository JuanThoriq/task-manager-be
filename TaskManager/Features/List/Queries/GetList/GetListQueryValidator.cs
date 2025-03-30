using FluentValidation;

namespace TaskManager.Features.List.Queries.GetList
{
    public class GetListQueryValidator : AbstractValidator<GetListQuery>
    {
        public GetListQueryValidator()
        {
            RuleFor(x => x.ListId).NotEmpty().WithMessage("ListId is required.");
        }
    }
}
