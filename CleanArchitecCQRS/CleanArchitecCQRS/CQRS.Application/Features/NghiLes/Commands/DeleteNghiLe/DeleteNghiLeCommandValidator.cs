using FluentValidation;

namespace EsuhaiHRM.Application.Features.NghiLes.Commands.DeleteNghiLe
{
    public class DeleteNghiLeCommandValidator : AbstractValidator<DeleteNghiLeCommand>
    {
        public DeleteNghiLeCommandValidator()
        {
            RuleFor(p => p.Id)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
        }
    }
}