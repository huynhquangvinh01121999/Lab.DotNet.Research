using EsuhaiHRM.Application.Interfaces.Repositories;
using FluentValidation;

namespace EsuhaiHRM.Application.Features.PhuCaps.Commands.CreatePhuCaps
{
    public class CreatePhuCapCommandValidator : AbstractValidator<CreatePhuCapCommand>
    {
        public CreatePhuCapCommandValidator()
        {

            RuleFor(p => p.NhanVienId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

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
