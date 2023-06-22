﻿using FluentValidation;

namespace EsuhaiHRM.Application.Features.ViecBenNgoais.Commands.CreateViecBenNgoai
{
    public class CreateViecBenNgoaiCommandValidator : AbstractValidator<CreateViecBenNgoaiCommand>
    {
        public CreateViecBenNgoaiCommandValidator()
        {
            RuleFor(p => p.ThoiGianBatDau)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();

            RuleFor(p => p.ThoiGianKetThuc)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();

            RuleFor(p => p.LoaiCongTac)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();

            RuleFor(p => p.MoTa)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();

            RuleFor(p => p.DiemDenId)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();

            RuleFor(p => p.NguoiGap)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();

            RuleFor(p => p.SoGio)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();

            RuleFor(p => p.NhanVienThayTheId)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();
        }
    }
}

