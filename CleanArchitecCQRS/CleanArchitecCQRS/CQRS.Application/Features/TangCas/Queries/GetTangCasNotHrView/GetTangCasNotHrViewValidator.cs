using FluentValidation;

namespace EsuhaiHRM.Application.Features.TangCas.Queries.GetTangCasNotHrView
{
    public class GetTangCasNotHrViewValidator : AbstractValidator<GetTangCasNotHrViewQuery>
    {
        public GetTangCasNotHrViewValidator()
        {

            RuleFor(p => p.NhanVienId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.ThoiGianBatDau)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.ThoiGianKetThuc)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
