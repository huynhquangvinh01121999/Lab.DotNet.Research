using FluentValidation;

namespace EsuhaiHRM.Application.Features.CauHinhNgayCongs.Commands.CreateCauHinhNgayCong
{
    public class CreateCauHinhNgayCongCommandValidator : AbstractValidator<CreateCauHinhNgayCongCommand>
    {
        public CreateCauHinhNgayCongCommandValidator()
        {
            RuleFor(p => p.Thang)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Nam)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.TongNgayCong)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}