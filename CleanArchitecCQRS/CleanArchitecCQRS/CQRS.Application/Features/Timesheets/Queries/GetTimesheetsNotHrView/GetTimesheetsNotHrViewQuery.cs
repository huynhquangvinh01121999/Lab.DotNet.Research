using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsNotHrView
{
    public class GetTimesheetsNotHrViewQuery : IRequest<PagedResponse<IEnumerable<GetTimesheetsNotHrViewModel>>>
    {
        public Guid NhanVienId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public string TrangThai { get; set; }
        public string Keyword { get; set; }
    }

    public class GetTimesheetsNotHrViewQueryHandler : IRequestHandler<GetTimesheetsNotHrViewQuery, PagedResponse<IEnumerable<GetTimesheetsNotHrViewModel>>>
    {
        private readonly ITimesheetRepositoryAsync _timesheetRepositoryAsync;

        public GetTimesheetsNotHrViewQueryHandler(ITimesheetRepositoryAsync timesheetRepositoryAsync)
        {
            _timesheetRepositoryAsync = timesheetRepositoryAsync;
        }

        public async Task<PagedResponse<IEnumerable<GetTimesheetsNotHrViewModel>>> Handle(GetTimesheetsNotHrViewQuery request, CancellationToken cancellationToken)
        {
            var tsViewModel = await _timesheetRepositoryAsync.S2_GetTimesheetsNotHrView(request.PageNumber,
                                                                                    request.PageSize,
                                                                                    request.NhanVienId,
                                                                                    request.ThoiGianBatDau,
                                                                                    request.ThoiGianKetThuc,
                                                                                    request.TrangThai,
                                                                                    request.Keyword);
            var totalItems = await _timesheetRepositoryAsync.GetTotalItem();

            return new PagedResponse<IEnumerable<GetTimesheetsNotHrViewModel>>(tsViewModel, request.PageNumber, request.PageSize, totalItems);
        }
    }
}
