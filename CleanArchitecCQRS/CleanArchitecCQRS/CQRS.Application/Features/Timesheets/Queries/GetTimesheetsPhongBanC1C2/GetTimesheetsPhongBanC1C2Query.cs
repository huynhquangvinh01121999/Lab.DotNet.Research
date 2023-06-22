using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBan;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanC1C2
{
    public class GetTimesheetsPhongBanC1C2Query : IRequest<PagedResponse<IEnumerable<GetTimesheetsPhongBanViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public DateTime ThoiGian { get; set; }
        public Guid NhanVienId { get; set; }
        public string Keyword { get; set; }
    }

    public class GetTimesheetsPhongBanC1C2QueryHandler : IRequestHandler<GetTimesheetsPhongBanC1C2Query, PagedResponse<IEnumerable<GetTimesheetsPhongBanViewModel>>>
    {
        private readonly ITimesheetRepositoryAsync _timesheetRepositoryAsync;

        public GetTimesheetsPhongBanC1C2QueryHandler(ITimesheetRepositoryAsync timesheetRepositoryAsync)
        {
            _timesheetRepositoryAsync = timesheetRepositoryAsync;
        }

        public async Task<PagedResponse<IEnumerable<GetTimesheetsPhongBanViewModel>>> Handle(GetTimesheetsPhongBanC1C2Query request, CancellationToken cancellationToken)
        {
            var tsViewModel = await _timesheetRepositoryAsync.SP_GetTimesheetsPhongBanC1C2(request.PageNumber
                                                                                            , request.PageSize
                                                                                            , request.ThoiGian
                                                                                            , request.NhanVienId
                                                                                            , request.Keyword);
            var totalItems = await _timesheetRepositoryAsync.GetTotalItem();

            return new PagedResponse<IEnumerable<GetTimesheetsPhongBanViewModel>>(tsViewModel, request.PageNumber, request.PageSize, totalItems);
        }
    }
}
