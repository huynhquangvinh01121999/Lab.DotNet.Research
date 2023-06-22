using EsuhaiHRM.Application.Interfaces.Repositories;
using FluentValidation;

namespace EsuhaiHRM.Application.Features.ChucVus.Commands.CreateChucVu
{
    public class CreateChucVuCommandValidator : AbstractValidator<CreateChucVuCommand>
    {
        private readonly IChucVuRepositoryAsync _chucVuRepository;

        public CreateChucVuCommandValidator(IChucVuRepositoryAsync chucVuRepository)
        {
            this._chucVuRepository = chucVuRepository;

            RuleFor(p => p.TenVN)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.PhanLoai)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
