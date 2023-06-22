using FluentValidation;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanV2C1C2
{
    public class GetTimesheetsPhongBanV2C1C2Validator : AbstractValidator<GetTimesheetsPhongBanV2C1C2Query>
    {
        public GetTimesheetsPhongBanV2C1C2Validator()
        {
            RuleFor(p => p.ThoiGian)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.NhanVienId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
