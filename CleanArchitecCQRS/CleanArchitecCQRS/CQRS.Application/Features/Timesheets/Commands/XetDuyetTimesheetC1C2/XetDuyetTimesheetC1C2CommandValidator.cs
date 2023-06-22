using FluentValidation;

namespace EsuhaiHRM.Application.Features.Timesheets.Commands.XetDuyetTimesheetC1C2
{
    public class XetDuyetTimesheetC1C2CommandValidator : AbstractValidator<XetDuyetTimesheetC1C2Command>
    {
        public XetDuyetTimesheetC1C2CommandValidator()
        {
            RuleFor(p => p.NhanVienId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.DanhSachXetDuyet)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
