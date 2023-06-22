using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.TongHopDuLieu.Queries.GetTongHopDuLieuNhanVien
{
    public class GetTongHopDuLieuNhanVienQuery : IRequest<Response<IEnumerable<GetTongHopDuLieuNhanVienVModel>>>
    {
        public Guid NhanVienId { get; set; }
        public DateTime ThoiGian { get; set; }
    }

    public class GetTongHopDuLieusQueryHandler : IRequestHandler<GetTongHopDuLieuNhanVienQuery, Response<IEnumerable<GetTongHopDuLieuNhanVienVModel>>>
    {
        private readonly ITongHopDuLieuRepositoryAsync _tonghopdulieuRepository;

        public GetTongHopDuLieusQueryHandler(ITongHopDuLieuRepositoryAsync tonghopdulieuRepository)
        {
            _tonghopdulieuRepository = tonghopdulieuRepository;
        }

        public async Task<Response<IEnumerable<GetTongHopDuLieuNhanVienVModel>>> Handle(GetTongHopDuLieuNhanVienQuery request, CancellationToken cancellationToken)
        {
            var thdl = await _tonghopdulieuRepository.S2_GetTongHopDuLieuNhanVien(request.NhanVienId, request.ThoiGian);

            return new Response<IEnumerable<GetTongHopDuLieuNhanVienVModel>>(thdl);
        }
    }
}
