using FluentValidation;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBan
{
    public class GetTimesheetsPhongBanValidator : AbstractValidator<GetTimesheetsPhongBanQuery>
    {
        public GetTimesheetsPhongBanValidator() {
            RuleFor(p => p.ThoiGian)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
