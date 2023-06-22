using FluentValidation;

namespace EsuhaiHRM.Application.Features.NghiPheps.Queries.GetNghiPhepsHrView
{
    public class GetNghiPhepsHrViewValidator : AbstractValidator<GetNghiPhepsHrViewQuery>
    {
        public GetNghiPhepsHrViewValidator()
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
