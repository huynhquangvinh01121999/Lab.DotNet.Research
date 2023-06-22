using FluentValidation;

namespace EsuhaiHRM.Application.Features.NghiPheps.Commands.DeleteNghiPhep
{
    public class DeleteNghiPhepCommandValidator : AbstractValidator<DeleteNghiPhepCommand>
    {
        public DeleteNghiPhepCommandValidator()
        {
            RuleFor(p => p.Id)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();
        }
    }
}
