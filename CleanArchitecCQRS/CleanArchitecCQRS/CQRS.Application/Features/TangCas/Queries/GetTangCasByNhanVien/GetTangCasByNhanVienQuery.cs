
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;
using EsuhaiHRM.Application.Exceptions;

namespace EsuhaiHRM.Application.Features.TangCas.Queries.GetTangCasByNhanVien
{
    public class GetTangCasByNhanVienQuery : IRequest<PagedResponse<IEnumerable<GetTangCasByNhanVienViewModel>>>
    {
        public DateTime NgayLamViec { get; set; }
        public Guid NhanVienId { get; set; }
    }
    public class GetTangCasByNhanVienQueryHandler : IRequestHandler<GetTangCasByNhanVienQuery, PagedResponse<IEnumerable<GetTangCasByNhanVienViewModel>>>
    {
        private readonly IMapper _mapper;
        private readonly ITangCaRepositoryAsync _tangCaRepository;

        public GetTangCasByNhanVienQueryHandler(ITangCaRepositoryAsync tangCaRepository, IMapper mapper)
        {
            _tangCaRepository = tangCaRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetTangCasByNhanVienViewModel>>> Handle(GetTangCasByNhanVienQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var tc = await _tangCaRepository.S2_GetTangCaByDate(request.NhanVienId, request.NgayLamViec);

                var totalItems = await _tangCaRepository.GetTotalItem();

                return new PagedResponse<IEnumerable<GetTangCasByNhanVienViewModel>>(tc, 1, 10, totalItems);
            }
            catch (Exception ex) {
                throw new ApiException(ex.Message);
            }
        }
    }
}
