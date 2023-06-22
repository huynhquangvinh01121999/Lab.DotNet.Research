using EsuhaiHRM.Application.Interfaces.Repositories;
using FluentValidation;

namespace EsuhaiHRM.Application.Features.GroupMails.Commands.CreateGroupMail
{
    public class CreateGroupMailCommandValidator : AbstractValidator<CreateGroupMailCommand>
    {
        private readonly IGroupMailRepositoryAsync _groupMailRepository;

        public CreateGroupMailCommandValidator(IGroupMailRepositoryAsync groupMailRepository)
        {
            this._groupMailRepository = groupMailRepository;

            RuleFor(p => p.Ten)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.PhanLoai)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
