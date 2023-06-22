using AutoMapper;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.PhuCaps.Queries.GetPhuCaps
{
    public partial class GetPhuCapsQuery : IRequest<PagedResponse<IEnumerable<GetPhuCapsViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? PhongId { get; set; }
        public int? BanId { get; set; }
    }

    public class GetAllCongTacsQueryHandler : IRequestHandler<GetPhuCapsQuery, PagedResponse<IEnumerable<GetPhuCapsViewModel>>>
    {
        private readonly IPhuCapRepositoryAsync _phuCapRepositoryAsync;
        private readonly IMapper _mapper;

        public GetAllCongTacsQueryHandler(IPhuCapRepositoryAsync phuCapRepositoryAsync, IMapper mapper)
        {
            _phuCapRepositoryAsync = phuCapRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetPhuCapsViewModel>>> Handle(GetPhuCapsQuery request, CancellationToken cancellationToken)
        {
            int totalItems = 0;
            var ts = await _phuCapRepositoryAsync.S2_GetPagedReponseAsync(request.PageNumber, request.PageSize, request.PhongId, request.BanId);

            totalItems = await _phuCapRepositoryAsync.GetTotalItem();
            var tsViewModel = _mapper.Map<IEnumerable<GetPhuCapsViewModel>>(ts);

            return new PagedResponse<IEnumerable<GetPhuCapsViewModel>>(tsViewModel, request.PageNumber, request.PageSize, totalItems);
        }
    }
}
