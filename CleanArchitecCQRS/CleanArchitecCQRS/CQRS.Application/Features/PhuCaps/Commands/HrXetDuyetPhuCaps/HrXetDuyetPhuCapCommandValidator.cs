using FluentValidation;

namespace EsuhaiHRM.Application.Features.PhuCaps.Commands.HrXetDuyetPhuCaps
{
    public class HrXetDuyetPhuCapCommandValidator : AbstractValidator<HrXetDuyetPhuCapsCommand>
    {
        public HrXetDuyetPhuCapCommandValidator()
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
