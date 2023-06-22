using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.PhongBans.Commands.CreatePhongBan
{
    public class CreatePhongBanCommandValidator : AbstractValidator<CreatePhongBanCommand>
    {
        private readonly IPhongBanRepositoryAsync _phongBanRepository;

        public CreatePhongBanCommandValidator(IPhongBanRepositoryAsync phongBanRepository)
        {
            this._phongBanRepository = phongBanRepository;

            RuleFor(p => p.TenVN)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.PhanLoai)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
