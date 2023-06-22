using FluentValidation;

namespace EsuhaiHRM.Application.Features.NghiLes.Commands.UpdateNghiLe
{
    public class UpdateNghiLeCommandValidator : AbstractValidator<UpdateNghiLeCommand>
    {
        public UpdateNghiLeCommandValidator()
        {
            RuleFor(p => p.Id)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
        }
    }
}
