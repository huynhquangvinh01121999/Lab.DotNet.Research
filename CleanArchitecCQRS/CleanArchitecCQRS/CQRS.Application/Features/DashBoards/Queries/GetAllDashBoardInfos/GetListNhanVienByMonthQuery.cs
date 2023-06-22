using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EsuhaiHRM.Domain.Entities;
using System;

namespace EsuhaiHRM.Application.Features.DashBoards.Queries.GetAllDashBoardInfos
{
    public class GetListNhanVienByMonthQuery : IRequest<Response<IEnumerable<DashBoard_12Months>>>
    {
        public int? Year { get; set; }
    }
    
    public class GetListNhanVienByMonthQueryHandler : IRequestHandler<GetListNhanVienByMonthQuery, Response<IEnumerable<DashBoard_12Months>>>
    {
        private readonly IDashBoardRepositoryAsync _dashboardRepositoryAsync;
        public GetListNhanVienByMonthQueryHandler(IDashBoardRepositoryAsync dashboardRepositoryAsync)
        {
            _dashboardRepositoryAsync = dashboardRepositoryAsync;
        }

        public async Task<Response<IEnumerable<DashBoard_12Months>>> Handle(GetListNhanVienByMonthQuery request, CancellationToken cancellationToken)
        {
            int? year = request.Year;
            if(year == null)
            {
                year = DateTime.Now.Year;
            }
            var result = await _dashboardRepositoryAsync.S2_GetListNhanVienInMonths(year);

            return new Response<IEnumerable<DashBoard_12Months>>(await Task.FromResult(result));
        }
    }
}
