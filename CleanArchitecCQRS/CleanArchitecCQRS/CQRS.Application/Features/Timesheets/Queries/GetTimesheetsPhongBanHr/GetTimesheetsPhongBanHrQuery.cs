using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBan;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanHr
{
    public class GetTimesheetsPhongBanHrQuery : IRequest<PagedResponse<IEnumerable<GetTimesheetsPhongBanViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public DateTime ThoiGian { get; set; }
        public int PhongId { get; set; }
        public int BanId { get; set; }
        public string Keyword { get; set; }
    }

    public class GetTimesheetsPhongBanHrQueryHandler : IRequestHandler<GetTimesheetsPhongBanHrQuery, PagedResponse<IEnumerable<GetTimesheetsPhongBanViewModel>>>
    {
        private readonly ITimesheetRepositoryAsync _timesheetRepositoryAsync;

        public GetTimesheetsPhongBanHrQueryHandler(ITimesheetRepositoryAsync timesheetRepositoryAsync)
        {
            _timesheetRepositoryAsync = timesheetRepositoryAsync;
        }

        public async Task<PagedResponse<IEnumerable<GetTimesheetsPhongBanViewModel>>> Handle(GetTimesheetsPhongBanHrQuery request, CancellationToken cancellationToken)
        {
            var tsViewModel = await _timesheetRepositoryAsync.SP_GetTimesheetsPhongBanHr(request.PageNumber
                                                                                            , request.PageSize
                                                                                            , request.ThoiGian
                                                                                            , request.PhongId
                                                                                            , request.BanId
                                                                                            , request.Keyword);
            var totalItems = await _timesheetRepositoryAsync.GetTotalItem();

            return new PagedResponse<IEnumerable<GetTimesheetsPhongBanViewModel>>(tsViewModel, request.PageNumber, request.PageSize, totalItems);
        }
    }
}
