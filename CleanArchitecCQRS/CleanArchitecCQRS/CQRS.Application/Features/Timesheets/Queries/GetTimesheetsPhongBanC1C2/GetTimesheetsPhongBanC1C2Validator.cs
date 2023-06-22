using FluentValidation;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanC1C2
{
    public class GetTimesheetsPhongBanC1C2Validator : AbstractValidator<GetTimesheetsPhongBanC1C2Query>
    {
        public GetTimesheetsPhongBanC1C2Validator()
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
