using EsuhaiHRM.Application.Interfaces.Repositories;
using FluentValidation;

namespace EsuhaiHRM.Application.Features.KhoaDaoTaos.Commands.CreateKhoaDaoTao
{
    public class CreateKhoaDaoTaoCommandValidator : AbstractValidator<CreateKhoaDaoTaoCommand>
    {
        private readonly IKhoaDaoTaoRepositoryAsync _khoaDaoTaoRepository;

        public CreateKhoaDaoTaoCommandValidator(IKhoaDaoTaoRepositoryAsync khoaDaoTaoRepository)
        {
            this._khoaDaoTaoRepository = khoaDaoTaoRepository;

            RuleFor(p => p.TenVN)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }

    }
}
