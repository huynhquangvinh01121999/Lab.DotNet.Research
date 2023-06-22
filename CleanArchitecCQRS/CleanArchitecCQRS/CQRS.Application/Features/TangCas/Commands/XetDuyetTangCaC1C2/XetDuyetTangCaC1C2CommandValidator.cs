using FluentValidation;

namespace EsuhaiHRM.Application.Features.TangCas.Commands.XetDuyetTangCaC1C2
{
    public class XetDuyetTangCaC1C2CommandValidator : AbstractValidator<XetDuyetTangCaC1C2Command>
    {
        public XetDuyetTangCaC1C2CommandValidator()
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
