using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EsuhaiHRM.Domain.Entities;
using EsuhaiHRM.Application.Features.TinhThanhs.Queries.GetAllTinhThanhs;

namespace EsuhaiHRM.Application.Features.TinhThanhs.Queries.GetHuyenXaById
{
    public class GetAllHuyensByTinhIdQuery : IRequest<PagedResponse<IEnumerable<Huyen>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TinhId { get; set; }
    }
    public class GetAllHuyensByTinhIdQueryHandler : IRequestHandler<GetAllHuyensByTinhIdQuery, PagedResponse<IEnumerable<Huyen>>>
    {
        private readonly ITinhThanhRepositoryAsync _tinhThanhRepository;
        private readonly IMapper _mapper;
        public GetAllHuyensByTinhIdQueryHandler(ITinhThanhRepositoryAsync tinhThanhRepository, IMapper mapper)
        {
            _tinhThanhRepository = tinhThanhRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<Huyen>>> Handle(GetAllHuyensByTinhIdQuery request, CancellationToken cancellationToken)
        {
            var tinhThanhs = await _tinhThanhRepository.S2_GetHuyenByTinhIdAsync(request.PageNumber, request.PageSize, request.TinhId);
            return new PagedResponse<IEnumerable<Huyen>>(tinhThanhs, request.PageNumber, request.PageSize);
        }
    }
}
