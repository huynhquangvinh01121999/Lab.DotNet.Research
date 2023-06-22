using FluentValidation;

namespace EsuhaiHRM.Application.Features.PhuCaps.Commands.CreatePhuCapsCountDay
{
    public class CreatePhuCapsCountDayCommandValidator : AbstractValidator<CreatePhuCapsCountDayCommand>
    {
        public CreatePhuCapsCountDayCommandValidator()
        {
            RuleFor(p => p.NhanVienId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            //RuleFor(p => p.NguoiXetDuyetCap2Id)
            //    .NotEmpty().WithMessage("{PropertyName} is required.")
            //    .NotNull();

            RuleFor(p => p.LoaiPhuCapId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.ThoiGianBatDau)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.ThoiGianKetThuc)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.MoTa)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
