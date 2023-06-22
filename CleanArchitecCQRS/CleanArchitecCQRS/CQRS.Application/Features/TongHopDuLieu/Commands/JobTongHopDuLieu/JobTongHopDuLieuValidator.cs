using FluentValidation;

namespace EsuhaiHRM.Application.Features.TongHopDuLieu.Commands.JobTongHopDuLieu
{
    public class JobTongHopDuLieuValidator : AbstractValidator<JobTongHopDuLieuQuery>
    {
        public JobTongHopDuLieuValidator()
        {
            RuleFor(p => p.Thang)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Nam)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
