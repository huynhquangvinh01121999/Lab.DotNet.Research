using FluentValidation;

namespace EsuhaiHRM.Application.Features.TangCas.Commands.HrXetDuyetTangCa
{
    public class HrXetDuyetTangCaCommandValidator : AbstractValidator<HrXetDuyetTangCaCommand>
    {
        public HrXetDuyetTangCaCommandValidator()
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
