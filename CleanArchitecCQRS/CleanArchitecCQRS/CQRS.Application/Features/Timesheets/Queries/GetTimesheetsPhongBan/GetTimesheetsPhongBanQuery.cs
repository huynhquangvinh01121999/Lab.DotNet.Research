using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBan
{
    public class GetTimesheetsPhongBanQuery : IRequest<PagedResponse<IEnumerable<GetTimesheetsPhongBanViewModel>>>
    {
        public Guid NhanVienId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public DateTime ThoiGian { get; set; }
        //public DateTime ThoiGianBatDau { get; set; }
        //public DateTime ThoiGianKetThuc { get; set; }
        //public string TrangThai { get; set; }
        //public string Keyword { get; set; }
    }

    public class GetTimesheetsNvNotHrViewQueryHandler : IRequestHandler<GetTimesheetsPhongBanQuery, PagedResponse<IEnumerable<GetTimesheetsPhongBanViewModel>>>
    {
        private readonly ITimesheetRepositoryAsync _timesheetRepositoryAsync;

        public GetTimesheetsNvNotHrViewQueryHandler(ITimesheetRepositoryAsync timesheetRepositoryAsync)
        {
            _timesheetRepositoryAsync = timesheetRepositoryAsync;
        }

        public async Task<PagedResponse<IEnumerable<GetTimesheetsPhongBanViewModel>>> Handle(GetTimesheetsPhongBanQuery request, CancellationToken cancellationToken)
        {
            var tsViewModel = await _timesheetRepositoryAsync.SP_GetTimesheetsPhongBan(request.PageNumber
                                                                                            , request.PageSize
                                                                                            , request.ThoiGian);
            var totalItems = await _timesheetRepositoryAsync.GetTotalItem();

            return new PagedResponse<IEnumerable<GetTimesheetsPhongBanViewModel>>(tsViewModel, request.PageNumber, request.PageSize, totalItems);
        }
    }
}
