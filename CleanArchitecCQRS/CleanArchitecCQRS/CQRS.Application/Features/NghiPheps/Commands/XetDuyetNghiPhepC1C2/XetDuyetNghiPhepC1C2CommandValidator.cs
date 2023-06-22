using FluentValidation;

namespace EsuhaiHRM.Application.Features.NghiPheps.Commands.XetDuyetNghiPhepC1C2
{
    public class XetDuyetNghiPhepC1C2CommandValidator : AbstractValidator<XetDuyetNghiPhepC1C2Command>
    {
        public XetDuyetNghiPhepC1C2CommandValidator()
        {
            RuleFor(p => p.NhanVienId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.DanhSachXetDuyet)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
