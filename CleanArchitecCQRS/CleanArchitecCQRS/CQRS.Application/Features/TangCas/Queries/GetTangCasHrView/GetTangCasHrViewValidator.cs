using FluentValidation;

namespace EsuhaiHRM.Application.Features.TangCas.Queries.GetTangCasHrView
{
    public class GetTangCasHrViewValidator : AbstractValidator<GetTangCasHrViewQuery>
    {
        public GetTangCasHrViewValidator()
        {
            RuleFor(p => p.ThoiGianBatDau)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.ThoiGianKetThuc)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
