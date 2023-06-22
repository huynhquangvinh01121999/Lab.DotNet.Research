using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.CongTys.Commands.UpdateCongTy
{
    public class UpdateCongTyCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string MaSoThue { get; set; }
        public string TenCongTyVN { get; set; }
        public string TenCongTyEN { get; set; }
        public string TenCongTyJP { get; set; }
        public string TenVietTat { get; set; }
        public string TenGiamDoc { get; set; }
        public bool TrangThai { get; set; }
        public string GhiChu { get; set; }
        public class UpdateCongTyCommandHandler : IRequestHandler<UpdateCongTyCommand, Response<int>>
        {
            private readonly ICongTyRepositoryAsync _congtyRepository;
            public UpdateCongTyCommandHandler(ICongTyRepositoryAsync congtyRepository)

            {
                _congtyRepository = congtyRepository;
            }
            public async Task<Response<int>> Handle(UpdateCongTyCommand command, CancellationToken cancellationToken)
            {
                var congty = await _congtyRepository.S2_GetByIdAsync(command.Id);

                if (congty == null)
                {
                    throw new ApiException($"CongTy Not Found.");
                }
                else
                {
                    congty.Code = command.Code;
                    congty.GhiChu = command.GhiChu;
                    congty.MaSoThue = command.MaSoThue;
                    congty.TenCongTyEN = command.TenCongTyEN;
                    congty.TenCongTyJP = command.TenCongTyJP;
                    congty.TenCongTyVN = command.TenCongTyVN;
                    congty.TenGiamDoc = command.TenGiamDoc;
                    congty.TenVietTat = command.TenVietTat;
                    congty.TrangThai = command.TrangThai;

                    await _congtyRepository.UpdateAsync(congty);
                    return new Response<int>(congty.Id);
                }
            }
        }
    }
}
