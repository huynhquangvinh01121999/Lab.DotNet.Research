using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EsuhaiHRM.Domain.Entities;

namespace EsuhaiHRM.Application.Features.DashBoards.Queries.GetAllDashBoardInfos
{
    public class GetAllDashBoardInfosQuery : IRequest<Response<DashBoard>>
    {
    }
    public class GetAllDashBoardInfosQueryHandler : IRequestHandler<GetAllDashBoardInfosQuery, Response<DashBoard>>
    {
        private readonly IDashBoardRepositoryAsync _dashboardRepositoryAsync;
        public GetAllDashBoardInfosQueryHandler(IDashBoardRepositoryAsync dashboardRepositoryAsync)
        {
            _dashboardRepositoryAsync = dashboardRepositoryAsync;
        }

        public async Task<Response<DashBoard>> Handle(GetAllDashBoardInfosQuery request, CancellationToken cancellationToken)
        {
            var result = await _dashboardRepositoryAsync.S2_GetDashBoardAsync();
            return new Response<DashBoard>(result);
        }
    }
}
