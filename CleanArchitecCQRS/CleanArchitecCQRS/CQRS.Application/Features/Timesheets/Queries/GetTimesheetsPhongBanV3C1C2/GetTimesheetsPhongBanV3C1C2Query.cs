using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanV3Hr;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanV3C1C2
{
    public class GetTimesheetsPhongBanV3C1C2Query : IRequest<PagedResponse<IEnumerable<GetTimesheetsPhongBanV3HrViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public DateTime ThoiGian { get; set; }
        public Guid NhanVienId { get; set; }
        public string Keyword { get; set; }
    }

    public class GetTimesheetsPhongBanV3C1C2QueryHandler : IRequestHandler<GetTimesheetsPhongBanV3C1C2Query, PagedResponse<IEnumerable<GetTimesheetsPhongBanV3HrViewModel>>>
    {
        private readonly ITimesheetRepositoryAsync _timesheetRepositoryAsync;

        public GetTimesheetsPhongBanV3C1C2QueryHandler(ITimesheetRepositoryAsync timesheetRepositoryAsync)
        {
            _timesheetRepositoryAsync = timesheetRepositoryAsync;
        }

        public async Task<PagedResponse<IEnumerable<GetTimesheetsPhongBanV3HrViewModel>>> Handle(GetTimesheetsPhongBanV3C1C2Query request, CancellationToken cancellationToken)
        {
            try
            {
                var tsViewModel = await _timesheetRepositoryAsync.SP_GetTimesheetsPhongBanV3C1C2(request.PageNumber
                                                                                           , request.PageSize
                                                                                           , request.ThoiGian
                                                                                           , request.NhanVienId
                                                                                           , request.Keyword);
                var totalItems = await _timesheetRepositoryAsync.GetTotalItem();

                return new PagedResponse<IEnumerable<GetTimesheetsPhongBanV3HrViewModel>>(tsViewModel, request.PageNumber, request.PageSize, totalItems);
            }
            catch(Exception ex) {
                throw new ApiException(ex.Message);
            }
        }
    }
}
