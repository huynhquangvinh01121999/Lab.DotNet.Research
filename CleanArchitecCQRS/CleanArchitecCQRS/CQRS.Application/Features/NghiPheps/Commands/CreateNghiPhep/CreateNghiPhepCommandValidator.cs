using FluentValidation;

namespace EsuhaiHRM.Application.Features.NghiPheps.Commands.CreateNghiPhep
{
    public class CreateNghiPhepCommandValidator : AbstractValidator<CreateNghiPhepCommand>
    {
        public CreateNghiPhepCommandValidator()
        {
            RuleFor(p => p.ThoiGianBatDau)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();

            RuleFor(p => p.ThoiGianKetThuc)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();

            RuleFor(p => p.SoNgayDangKy)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();

            RuleFor(p => p.MoTa)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();

            RuleFor(p => p.CongViecThayThe)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();

            RuleFor(p => p.NhanVienThayTheId)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();
        }
    }
}
