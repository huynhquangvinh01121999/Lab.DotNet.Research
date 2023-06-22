using EsuhaiHRM.Application.Interfaces.Repositories;
using FluentValidation;

namespace EsuhaiHRM.Application.Features.CaLamViecs.Commands.CreateCaLamViec
{
    public class CreateCaLamViecCommandValidator : AbstractValidator<CreateCaLamViecCommand>
    {
        private readonly ICaLamViecRepositoryAsync _calamviecRepository;

        public CreateCaLamViecCommandValidator(ICaLamViecRepositoryAsync calamviecRepository)
        {
            this._calamviecRepository = calamviecRepository;

            RuleFor(p => p.TenVN)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.GioBatDau)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.GioKetThuc)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.BatDauNghi)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.KetThucNghi)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.GioKetThuc)
                .GreaterThan(r => r.GioBatDau).WithMessage("GioKetThuc phai sau GioBatDau")
                .When(m => m.GioBatDau.HasValue)
                .GreaterThan(r => r.BatDauNghi).WithMessage("GioKetThuc phai sau BatDauNghi")
                .When(m => m.BatDauNghi.HasValue)
                .GreaterThan(r => r.KetThucNghi).WithMessage("GioKetThuc phai sau KetThucNghi")
                .When(m => m.KetThucNghi.HasValue);

            RuleFor(p => p.BatDauNghi)
                .GreaterThan(r => r.GioBatDau).WithMessage("BatDauNghi phai sau GioBatDau")
                .When(m => m.GioBatDau.HasValue);

            RuleFor(p => p.KetThucNghi)
                .GreaterThan(r => r.BatDauNghi).WithMessage("KetThucNghi phai sau BatDauNghi")
                .When(m => m.BatDauNghi.HasValue)
                .GreaterThan(r => r.GioBatDau).WithMessage("KetThucNghi phai sau GioBatDau")
                .When(m => m.GioBatDau.HasValue);
        }
    }
}
