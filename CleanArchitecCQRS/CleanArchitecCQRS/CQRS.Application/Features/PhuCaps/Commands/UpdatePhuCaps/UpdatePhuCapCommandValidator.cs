using FluentValidation;

namespace EsuhaiHRM.Application.Features.PhuCaps.Commands.UpdatePhuCaps
{
    public class UpdatePhuCapCommandValidator : AbstractValidator<UpdatePhuCapCommand>
    {
        public UpdatePhuCapCommandValidator()
        {

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
