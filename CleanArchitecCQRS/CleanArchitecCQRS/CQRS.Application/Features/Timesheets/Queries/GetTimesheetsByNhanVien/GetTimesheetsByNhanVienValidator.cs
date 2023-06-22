using FluentValidation;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsByNhanVien
{
    public class GetTimesheetsByNhanVienValidator : AbstractValidator<GetTimesheetsByNhanVienQuery>
    {
        public GetTimesheetsByNhanVienValidator()
        {
            RuleFor(p => p.NhanVienId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Thang)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Nam)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
