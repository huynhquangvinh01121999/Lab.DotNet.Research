using FluentValidation;

namespace EsuhaiHRM.Application.Features.ViecBenNgoais.Commands.DeleteViecBenNgoai
{
    public class DeleteViecBenNgoaiCommandValidator : AbstractValidator<DeleteViecBenNgoaiCommand>
    {
        public DeleteViecBenNgoaiCommandValidator()
        {
            RuleFor(p => p.Id)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();
        }
    }
}
