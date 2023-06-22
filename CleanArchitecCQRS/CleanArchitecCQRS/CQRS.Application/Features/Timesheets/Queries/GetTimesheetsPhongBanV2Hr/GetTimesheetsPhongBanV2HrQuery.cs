using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanV2Hr
{
    public class GetTimesheetsPhongBanV2HrQuery : IRequest<PagedResponse<IEnumerable<GetTimesheetsPhongBanV2HrViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public DateTime ThoiGian { get; set; }
        public int PhongId { get; set; }
        public int BanId { get; set; }
        public string Keyword { get; set; }
    }

    public class GetTimesheetsPhongBanV2HrQueryHandler : IRequestHandler<GetTimesheetsPhongBanV2HrQuery, PagedResponse<IEnumerable<GetTimesheetsPhongBanV2HrViewModel>>>
    {
        private readonly ITimesheetRepositoryAsync _timesheetRepositoryAsync;

        public GetTimesheetsPhongBanV2HrQueryHandler(ITimesheetRepositoryAsync timesheetRepositoryAsync)
        {
            _timesheetRepositoryAsync = timesheetRepositoryAsync;
        }

        public async Task<PagedResponse<IEnumerable<GetTimesheetsPhongBanV2HrViewModel>>> Handle(GetTimesheetsPhongBanV2HrQuery request, CancellationToken cancellationToken)
        {
            var tsViewModel = await _timesheetRepositoryAsync.SP_GetTimesheetsPhongBanV2Hr(request.PageNumber
                                                                                             , request.PageSize
                                                                                             , request.ThoiGian
                                                                                             , request.PhongId
                                                                                             , request.BanId
                                                                                             , request.Keyword);
            var totalItems = await _timesheetRepositoryAsync.GetTotalItem();

            return new PagedResponse<IEnumerable<GetTimesheetsPhongBanV2HrViewModel>>(tsViewModel, request.PageNumber, request.PageSize, totalItems);
        }
    }
}
