using EsuhaiHRM.Application.Features.PhuCaps.Queries.GetPhuCapsByNhanVien;
using EsuhaiHRM.Application.Interfaces.Repositories;
using FluentValidation;

namespace EsuhaiHRM.Application.Features.PhuCaps.Commands.CreatePhuCaps
{
    public class GetPhuCapsByNhanVienValidator : AbstractValidator<GetPhuCapsByNhanVienQuery>
    {
        private readonly IPhuCapRepositoryAsync _phuCapRepository;

        public GetPhuCapsByNhanVienValidator(IPhuCapRepositoryAsync phuCapRepository)
        {
            _phuCapRepository = phuCapRepository;

            RuleFor(p => p.NhanVienId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Thang)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
        }
    }
}
