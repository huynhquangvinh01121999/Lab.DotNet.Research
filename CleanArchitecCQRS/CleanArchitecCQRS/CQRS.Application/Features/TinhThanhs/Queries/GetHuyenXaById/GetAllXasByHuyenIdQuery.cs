using EsuhaiHRM.Application.Filters;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EsuhaiHRM.Domain.Entities;

namespace EsuhaiHRM.Application.Features.TinhThanhs.Queries.GetAllTinhThanhs
{
    public class GetAllXasByHuyenIdQuery : IRequest<PagedResponse<IEnumerable<Xa>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int HuyenId { get; set; }
    }
    public class GetAllXasByHuyenIdQueryHandler : IRequestHandler<GetAllXasByHuyenIdQuery, PagedResponse<IEnumerable<Xa>>>
    {
        private readonly ITinhThanhRepositoryAsync _tinhThanhRepository;
        private readonly IMapper _mapper;
        public GetAllXasByHuyenIdQueryHandler(ITinhThanhRepositoryAsync tinhThanhRepository, IMapper mapper)
        {
            _tinhThanhRepository = tinhThanhRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<Xa>>> Handle(GetAllXasByHuyenIdQuery request, CancellationToken cancellationToken)
        {
            var tinhThanhs = await _tinhThanhRepository.S2_GetXaByHuyenIdAsync(request.PageNumber, request.PageSize,request.HuyenId);
            return new PagedResponse<IEnumerable<Xa>>(tinhThanhs, request.PageNumber, request.PageSize);
        }
    }
}
