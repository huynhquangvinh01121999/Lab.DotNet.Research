using FluentValidation;

namespace EsuhaiHRM.Application.Features.CauHinhNgayCongs.Queries.GetCauHinhNgayCongs
{
    public class GetCauHinhNgayCongsQueryValidator : AbstractValidator<GetCauHinhNgayCongsQuery>
    {
        public GetCauHinhNgayCongsQueryValidator()
        {
            RuleFor(p => p.Nam)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
