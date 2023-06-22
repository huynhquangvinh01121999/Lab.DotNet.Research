using AutoMapper;
using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.PhuCaps.Queries.GetPhuCapsByNhanVien
{
    public class GetPhuCapsByNhanVienQuery : IRequest<PagedResponse<IEnumerable<GetPhuCapsByNhanVienViewModel>>>
    {
        public Guid NhanVienId { get; set; }
        public DateTime Thang { get; set; }
    }

    public class GetCongTacsByNhanVienQueryHandler : IRequestHandler<GetPhuCapsByNhanVienQuery, PagedResponse<IEnumerable<GetPhuCapsByNhanVienViewModel>>>
    {
        private readonly IMapper _mapper;
        private readonly IPhuCapRepositoryAsync _phuCapRepositoryAsync;

        public GetCongTacsByNhanVienQueryHandler(IMapper mapper, IPhuCapRepositoryAsync phuCapRepositoryAsync)
        {
            _mapper = mapper;
            _phuCapRepositoryAsync = phuCapRepositoryAsync;
        }

        public async Task<PagedResponse<IEnumerable<GetPhuCapsByNhanVienViewModel>>> Handle(GetPhuCapsByNhanVienQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var phucaps = await _phuCapRepositoryAsync.S2_GetPhuCapByNhanVien(request.Thang, request.NhanVienId);

                var totalItems = await _phuCapRepositoryAsync.GetTotalItem();

                return new PagedResponse<IEnumerable<GetPhuCapsByNhanVienViewModel>>(phucaps, 1, 10, totalItems);
            }
            catch (Exception ex) {
                throw new ApiException(ex.Message);
            }
        }
    }
}
