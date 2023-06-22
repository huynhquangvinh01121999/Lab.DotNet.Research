
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace EsuhaiHRM.Application.Features.TongHopDuLieus.Queries.GetTongHopDuLieusByNhanVien
{
    public class GetTongHopDuLieusByNhanVienQuery : IRequest<PagedResponse<IEnumerable<GetTongHopDuLieusByNhanVienViewModel>>>
    {
        public int Thang { get; set; }
        public int Nam { get; set; }
        public Guid NhanVienId { get; set; }
    }
    public class GetTongHopDuLieusByNhanVienQueryHandler : IRequestHandler<GetTongHopDuLieusByNhanVienQuery, PagedResponse<IEnumerable<GetTongHopDuLieusByNhanVienViewModel>>>
    {
        private readonly IMapper _mapper;
        private readonly ITongHopDuLieuRepositoryAsync _tonghopdulieuRepository;

        public GetTongHopDuLieusByNhanVienQueryHandler(ITongHopDuLieuRepositoryAsync tongHopDuLieuRepository, IMapper mapper)
        {
            _tonghopdulieuRepository = tongHopDuLieuRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetTongHopDuLieusByNhanVienViewModel>>> Handle(GetTongHopDuLieusByNhanVienQuery request, CancellationToken cancellationToken)
        {
            int totalItems = 0;

            var validFilter = _mapper.Map<GetTongHopDuLieusByNhanVienParameter>(request);
            var ts = await _tonghopdulieuRepository.S2_GetTimesheetsInMonth(validFilter.NhanVienId, validFilter.Thang, validFilter.Nam);

            totalItems = await _tonghopdulieuRepository.GetTotalItem();
            //var tsViewModel = _mapper.Map<IEnumerable<GetTongHopDuLieusByNhanVienViewModel>>(ts);

            return new PagedResponse<IEnumerable<GetTongHopDuLieusByNhanVienViewModel>>(ts, 1, 100, totalItems);
        }
    }
}
