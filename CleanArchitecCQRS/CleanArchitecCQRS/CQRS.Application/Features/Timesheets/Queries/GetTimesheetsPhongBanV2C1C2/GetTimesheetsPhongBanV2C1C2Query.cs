using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanV2Hr;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanV2C1C2
{
    public class GetTimesheetsPhongBanV2C1C2Query : IRequest<PagedResponse<IEnumerable<GetTimesheetsPhongBanV2HrViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public DateTime ThoiGian { get; set; }
        public Guid NhanVienId { get; set; }
        public string Keyword { get; set; }
    }

    public class GetTimesheetsPhongBanV2C1C2QueryHandler : IRequestHandler<GetTimesheetsPhongBanV2C1C2Query, PagedResponse<IEnumerable<GetTimesheetsPhongBanV2HrViewModel>>>
    {
        private readonly ITimesheetRepositoryAsync _timesheetRepositoryAsync;

        public GetTimesheetsPhongBanV2C1C2QueryHandler(ITimesheetRepositoryAsync timesheetRepositoryAsync)
        {
            _timesheetRepositoryAsync = timesheetRepositoryAsync;
        }

        public async Task<PagedResponse<IEnumerable<GetTimesheetsPhongBanV2HrViewModel>>> Handle(GetTimesheetsPhongBanV2C1C2Query request, CancellationToken cancellationToken)
        {
            var tsViewModel = await _timesheetRepositoryAsync.SP_GetTimesheetsPhongBanV2C1C2(request.PageNumber
                                                                                           , request.PageSize
                                                                                           , request.ThoiGian
                                                                                           , request.NhanVienId
                                                                                           , request.Keyword);
            var totalItems = await _timesheetRepositoryAsync.GetTotalItem();

            return new PagedResponse<IEnumerable<GetTimesheetsPhongBanV2HrViewModel>>(tsViewModel, request.PageNumber, request.PageSize, totalItems);
        }
    }
}
