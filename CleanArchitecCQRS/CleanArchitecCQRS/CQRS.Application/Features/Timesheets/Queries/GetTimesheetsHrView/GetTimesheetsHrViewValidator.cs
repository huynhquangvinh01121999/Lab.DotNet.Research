using FluentValidation;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsHrView
{
    public class GetTimesheetsHrViewValidator : AbstractValidator<GetTimesheetsHrViewQuery>
    {
        public GetTimesheetsHrViewValidator()
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
