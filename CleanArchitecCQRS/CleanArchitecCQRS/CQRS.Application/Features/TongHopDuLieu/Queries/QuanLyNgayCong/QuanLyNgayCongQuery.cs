using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.TongHopDuLieu.Queries.QuanLyNgayCong
{
    public class QuanLyNgayCongQuery : IRequest<PagedResponse<IEnumerable<QuanLyNgayCongViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public DateTime Thang { get; set; }
        public int PhongId { get; set; }
        public string Keyword { get; set; }
        public string OrderBy { get; set; }
    }

    public class QuanLyNgayCongQueryHandler : IRequestHandler<QuanLyNgayCongQuery, PagedResponse<IEnumerable<QuanLyNgayCongViewModel>>>
    {
        private readonly ITongHopDuLieuRepositoryAsync _tongHopDuLieuRepositoryAsync;

        public QuanLyNgayCongQueryHandler(ITongHopDuLieuRepositoryAsync tongHopDuLieuRepositoryAsync)
        {
            _tongHopDuLieuRepositoryAsync = tongHopDuLieuRepositoryAsync;
        }

        public async Task<PagedResponse<IEnumerable<QuanLyNgayCongViewModel>>> Handle(QuanLyNgayCongQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var tonghopNgayCongs = await _tongHopDuLieuRepositoryAsync.S2_QuanLyNgayCong(request.PageNumber,
                                                                                    request.PageSize,
                                                                                    request.Thang,
                                                                                    request.PhongId,
                                                                                    request.Keyword,
                                                                                    request.OrderBy);
                var totalItems = await _tongHopDuLieuRepositoryAsync.GetTotalItem();

                return new PagedResponse<IEnumerable<QuanLyNgayCongViewModel>>(tonghopNgayCongs, request.PageNumber, request.PageSize, totalItems);
            }
            catch(Exception ex) {
                throw new ApiException(ex.Message);
            }
        }
    }
}
