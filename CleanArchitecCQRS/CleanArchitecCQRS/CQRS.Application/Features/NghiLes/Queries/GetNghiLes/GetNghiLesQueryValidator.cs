using FluentValidation;

namespace EsuhaiHRM.Application.Features.NghiLes.Queries.GetNghiLes
{
    public class GetNghiLesQueryValidator : AbstractValidator<GetNghiLesQuery>
    {
        public GetNghiLesQueryValidator()
        {
            RuleFor(p => p.Nam)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
