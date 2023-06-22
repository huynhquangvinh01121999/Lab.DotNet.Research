using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsHrView
{
    public class GetTimesheetsHrViewQuery : IRequest<PagedResponse<IEnumerable<GetTimesheetsHrViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int PhongId { get; set; }
        public int BanId { get; set; }
        public string TrangThai { get; set; }
        public string Keyword { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
    }

    public class GetTimesheetsHrViewQueryHandler : IRequestHandler<GetTimesheetsHrViewQuery, PagedResponse<IEnumerable<GetTimesheetsHrViewModel>>>
    {
        private readonly ITimesheetRepositoryAsync _timesheetRepositoryAsync;

        public GetTimesheetsHrViewQueryHandler(ITimesheetRepositoryAsync timesheetRepositoryAsync)
        {
            _timesheetRepositoryAsync = timesheetRepositoryAsync;
        }

        public async Task<PagedResponse<IEnumerable<GetTimesheetsHrViewModel>>> Handle(GetTimesheetsHrViewQuery request, CancellationToken cancellationToken)
        {
            var tsViewModel = await _timesheetRepositoryAsync.S2_GetTimesheetsHrView(request.PageNumber,
                                                                                    request.PageSize,
                                                                                    request.PhongId,
                                                                                    request.BanId,
                                                                                    request.TrangThai,
                                                                                    request.Keyword,
                                                                                    request.ThoiGianBatDau,
                                                                                    request.ThoiGianKetThuc);
            var totalItems = await _timesheetRepositoryAsync.GetTotalItem();

            return new PagedResponse<IEnumerable<GetTimesheetsHrViewModel>>(tsViewModel, request.PageNumber, request.PageSize, totalItems);
        }
    }
}
