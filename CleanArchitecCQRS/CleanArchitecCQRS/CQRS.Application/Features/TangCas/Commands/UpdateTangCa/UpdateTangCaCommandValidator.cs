using FluentValidation;

namespace EsuhaiHRM.Application.Features.TangCas.Commands.UpdateTangCa
{
    public class UpdateTangCaCommandValidator : AbstractValidator<UpdateTangCaCommand>
    {
        public UpdateTangCaCommandValidator()
        {
            RuleFor(p => p.NgayTangCa)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.ThoiGianBatDau)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.ThoiGianKetThuc)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}