using EsuhaiHRM.Application.Interfaces.Repositories;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.NhanViens.Commands.CreateNhanVien
{
    public class CreateNhanVienCommandValidator : AbstractValidator<CreateNhanVienCommand>
    {
        private readonly INhanVienRepositoryAsync _nhanvienRepository;

        public CreateNhanVienCommandValidator(INhanVienRepositoryAsync nhanvienRepository)
        {
            this._nhanvienRepository = nhanvienRepository;

            RuleFor(p => p.Username)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MustAsync(IsUniqueUsername).WithMessage("{PropertyName} already exists.");
        }

        private async Task<bool> IsUniqueUsername(string username, CancellationToken cancellationToken)
        {
            return await _nhanvienRepository.IsUniqueUsernameAsync(username);
        }
    }
}
