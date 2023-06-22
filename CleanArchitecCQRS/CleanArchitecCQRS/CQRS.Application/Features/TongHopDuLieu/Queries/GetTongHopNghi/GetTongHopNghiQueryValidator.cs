using FluentValidation;

namespace EsuhaiHRM.Application.Features.TongHopDuLieu.Queries.GetTongHopNghi
{
    public class GetTongHopNghiQueryValidator : AbstractValidator<GetTongHopNghiQuery>
    {
        public GetTongHopNghiQueryValidator()
        {
            RuleFor(p => p.Thang)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Nam)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}