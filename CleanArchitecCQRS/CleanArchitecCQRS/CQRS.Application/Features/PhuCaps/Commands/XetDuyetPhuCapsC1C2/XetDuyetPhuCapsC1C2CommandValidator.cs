using FluentValidation;

namespace EsuhaiHRM.Application.Features.PhuCaps.Commands.XetDuyetPhuCapsC1C2
{
    public class XetDuyetPhuCapsC1C2CommandValidator : AbstractValidator<XetDuyetPhuCapsC1C2Command>
    {
        public XetDuyetPhuCapsC1C2CommandValidator()
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
