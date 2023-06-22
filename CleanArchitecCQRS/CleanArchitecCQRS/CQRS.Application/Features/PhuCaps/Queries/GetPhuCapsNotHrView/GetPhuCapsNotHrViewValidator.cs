using FluentValidation;

namespace EsuhaiHRM.Application.Features.PhuCaps.Queries.GetPhuCapsNotHrView
{
    public class GetPhuCapsNotHrViewValidator : AbstractValidator<GetPhuCapsNotHrViewQuery>
    {
        public GetPhuCapsNotHrViewValidator()
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
