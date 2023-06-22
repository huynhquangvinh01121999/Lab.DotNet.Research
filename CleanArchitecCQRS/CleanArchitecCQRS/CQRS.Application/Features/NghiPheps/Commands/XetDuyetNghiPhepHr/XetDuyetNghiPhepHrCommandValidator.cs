using FluentValidation;

namespace EsuhaiHRM.Application.Features.NghiPheps.Commands.XetDuyetNghiPhepHr
{
    public class XetDuyetNghiPhepHrCommandValidator : AbstractValidator<XetDuyetNghiPhepHrCommand>
    {
        public XetDuyetNghiPhepHrCommandValidator()
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
