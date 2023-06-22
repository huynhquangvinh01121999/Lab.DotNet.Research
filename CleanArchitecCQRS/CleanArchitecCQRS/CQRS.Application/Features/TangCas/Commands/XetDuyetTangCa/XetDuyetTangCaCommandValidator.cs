using FluentValidation;

namespace EsuhaiHRM.Application.Features.TangCas.Commands.XetDuyetTangCa
{
    public class XetDuyetTangCaCommandValidator : AbstractValidator<XetDuyetTangCaCommand>
    {
        public XetDuyetTangCaCommandValidator()
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
