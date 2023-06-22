using FluentValidation;

namespace EsuhaiHRM.Application.Features.PhuCaps.Commands.XetDuyetPhuCaps
{
    public class XetDuyetPhuCapCommandValidator : AbstractValidator<XetDuyetPhuCapsCommand>
    {
        public XetDuyetPhuCapCommandValidator()
        {

            RuleFor(p => p.NhanVienId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.PhanLoai)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.DanhSachXetDuyet)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
