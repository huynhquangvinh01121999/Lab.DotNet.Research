using FluentValidation;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanHr
{
    public class GetTimesheetsPhongBanHrValidator : AbstractValidator<GetTimesheetsPhongBanHrQuery>
    {
        public GetTimesheetsPhongBanHrValidator()
        {
            RuleFor(p => p.ThoiGian)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
