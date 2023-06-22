using FluentValidation;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanV3C1C2
{
    public class GetTimesheetsPhongBanV3C1C2Validator : AbstractValidator<GetTimesheetsPhongBanV3C1C2Query>
    {
        public GetTimesheetsPhongBanV3C1C2Validator()
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