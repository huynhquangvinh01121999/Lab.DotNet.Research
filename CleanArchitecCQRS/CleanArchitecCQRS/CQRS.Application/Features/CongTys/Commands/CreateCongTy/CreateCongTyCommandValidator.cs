using EsuhaiHRM.Application.Interfaces.Repositories;
using FluentValidation;

namespace EsuhaiHRM.Application.Features.CongTys.Commands.CreateCongTy
{
    public class CreateCongTyCommandValidator : AbstractValidator<CreateCongTyCommand>
    {
        private readonly ICongTyRepositoryAsync _congTyRepository;

        public CreateCongTyCommandValidator(ICongTyRepositoryAsync congTyRepository)
        {
            this._congTyRepository = congTyRepository;

            RuleFor(p => p.TenCongTyVN)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
