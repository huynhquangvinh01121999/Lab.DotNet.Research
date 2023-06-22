using FluentValidation;

namespace EsuhaiHRM.Application.Features.NghiPheps.Queries.GetNghiPhepsNotHrView
{
    public class GetNghiPhepsNotHrViewValidator : AbstractValidator<GetNghiPhepsNotHrViewQuery>
    {
        public GetNghiPhepsNotHrViewValidator()
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
