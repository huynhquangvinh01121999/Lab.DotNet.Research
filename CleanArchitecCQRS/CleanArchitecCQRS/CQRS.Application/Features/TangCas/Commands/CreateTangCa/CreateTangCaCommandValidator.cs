using EsuhaiHRM.Application.Interfaces.Repositories;
using FluentValidation;

namespace EsuhaiHRM.Application.Features.TangCas.Commands.CreateTangCa
{
    public class CreateTangCaCommandValidator : AbstractValidator<CreateTangCaCommand>
    {
        private readonly ITangCaRepositoryAsync _tangCaRepository;

        public CreateTangCaCommandValidator(ITangCaRepositoryAsync tangCaRepository)
        {
            _tangCaRepository = tangCaRepository;

            RuleFor(p => p.NgayTangCa)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
