using FluentValidation;

namespace EsuhaiHRM.Application.Features.TongHopDuLieu.Queries.GetTongHopNgayCong
{
    public class GetTongHopNgayCongValidator : AbstractValidator<GetTongHopNgayCongQuery>
    {
        public GetTongHopNgayCongValidator()
        {
            RuleFor(p => p.NhanVienId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Thang)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Nam)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}