using FluentValidation;

namespace EsuhaiHRM.Application.Features.ViecBenNgoais.Queries.GetViecBenNgoaiHrView
{
    public class GetViecBenNgoaiHrViewValidator : AbstractValidator<GetViecBenNgoaiHrViewQuery>
    {
        public GetViecBenNgoaiHrViewValidator()
        {
            RuleFor(p => p.ThoiGianBatDau)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.ThoiGianKetThuc)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
