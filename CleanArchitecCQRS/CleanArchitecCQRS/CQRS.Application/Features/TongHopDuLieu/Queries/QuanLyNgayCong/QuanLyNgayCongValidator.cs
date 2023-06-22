using FluentValidation;

namespace EsuhaiHRM.Application.Features.TongHopDuLieu.Queries.QuanLyNgayCong
{
    public class QuanLyNgayCongValidator : AbstractValidator<QuanLyNgayCongQuery>
    {
        public QuanLyNgayCongValidator()
        {
            RuleFor(p => p.Thang)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}