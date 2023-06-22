using FluentValidation;

namespace EsuhaiHRM.Application.Features.PhuCaps.Queries.GetPhuCapsHrView
{
    public class GetPhuCapsHrViewValidator : AbstractValidator<GetPhuCapsHrViewQuery>
    {
        public GetPhuCapsHrViewValidator()
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
