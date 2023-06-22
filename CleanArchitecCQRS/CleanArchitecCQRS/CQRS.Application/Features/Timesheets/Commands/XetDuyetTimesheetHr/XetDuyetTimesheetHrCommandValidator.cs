using FluentValidation;

namespace EsuhaiHRM.Application.Features.Timesheets.Commands.XetDuyetTimesheetHr
{
    public class XetDuyetTimesheetHrCommandValidator : AbstractValidator<XetDuyetTimesheetHrCommand>
    {
        public XetDuyetTimesheetHrCommandValidator()
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