using FluentValidation;

namespace EsuhaiHRM.Application.Features.CauHinhNgayCongs.Commands.DeleteCauHinhNgayCong
{
    public class DeleteCauHinhNgayCongCommandValidator : AbstractValidator<DeleteCauHinhNgayCongCommand>
    {
        public DeleteCauHinhNgayCongCommandValidator()
        {
            RuleFor(p => p.Id)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
        }
    }
}