using EsuhaiHRM.Application.Features.TongHopDuLieus.Queries.GetTongHopDuLieusByNhanVien;
using FluentValidation;

namespace EsuhaiHRM.Application.Features.TongHopDuLieu.Queries.GetTongHopDuLieusByNhanVien
{
    public class GetTongHopDuLieusByNhanVienValidator : AbstractValidator<GetTongHopDuLieusByNhanVienQuery>
    {
        public GetTongHopDuLieusByNhanVienValidator()
        {
            RuleFor(p => p.NhanVienId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Nam)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Thang)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
