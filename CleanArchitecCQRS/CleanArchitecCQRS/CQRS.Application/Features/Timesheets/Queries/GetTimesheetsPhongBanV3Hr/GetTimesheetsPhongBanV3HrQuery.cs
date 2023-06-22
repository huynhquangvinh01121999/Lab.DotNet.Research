using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanV3Hr
{
    public class GetTimesheetsPhongBanV3HrQuery : IRequest<PagedResponse<IEnumerable<GetTimesheetsPhongBanV3HrViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public DateTime ThoiGian { get; set; }
        public int PhongId { get; set; }
        public int BanId { get; set; }
        public string Keyword { get; set; }
    }

    public class GetTimesheetsPhongBanV3HrQueryHandler : IRequestHandler<GetTimesheetsPhongBanV3HrQuery, PagedResponse<IEnumerable<GetTimesheetsPhongBanV3HrViewModel>>>
    {
        private readonly ITimesheetRepositoryAsync _timesheetRepositoryAsync;

        public GetTimesheetsPhongBanV3HrQueryHandler(ITimesheetRepositoryAsync timesheetRepositoryAsync)
        {
            _timesheetRepositoryAsync = timesheetRepositoryAsync;
        }

        public async Task<PagedResponse<IEnumerable<GetTimesheetsPhongBanV3HrViewModel>>> Handle(GetTimesheetsPhongBanV3HrQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var tsViewModel = await _timesheetRepositoryAsync.SP_GetTimesheetsPhongBanV3Hr(request.PageNumber
                                                                                             , request.PageSize
                                                                                             , request.ThoiGian
                                                                                             , request.PhongId
                                                                                             , request.BanId
                                                                                             , request.Keyword);
                var totalItems = await _timesheetRepositoryAsync.GetTotalItem();

                return new PagedResponse<IEnumerable<GetTimesheetsPhongBanV3HrViewModel>>(tsViewModel, request.PageNumber, request.PageSize, totalItems);
            }
            catch (Exception ex) {
                throw new ApiException(ex.Message);
            }
        }
    }
}
