using FluentValidation;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanV3Hr
{
    public class GetTimesheetsPhongBanV3HrValidator : AbstractValidator<GetTimesheetsPhongBanV3HrQuery>
    {
        public GetTimesheetsPhongBanV3HrValidator()
        {
            RuleFor(p => p.ThoiGian)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
