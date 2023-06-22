using FluentValidation;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanV2Hr
{
    public class GetTimesheetsPhongBanV2HrValidator : AbstractValidator<GetTimesheetsPhongBanV2HrQuery>
    {
        public GetTimesheetsPhongBanV2HrValidator()
        {
            RuleFor(p => p.ThoiGian)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
