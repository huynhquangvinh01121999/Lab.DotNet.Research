using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetById
{
    public class GetDieuChinhTsDetailQuery : IRequest<Response<GetDieuChinhTsDetailViewModel>>
    {
        public Guid Id { get; set; }
        public class GetDieuChinhTsDetailQueryHandler : IRequestHandler<GetDieuChinhTsDetailQuery, Response<GetDieuChinhTsDetailViewModel>>
        {
            private readonly ITimesheetRepositoryAsync _timesheetRepository;
            public GetDieuChinhTsDetailQueryHandler(ITimesheetRepositoryAsync timesheetRepository)
            {
                _timesheetRepository = timesheetRepository;
            }
            public async Task<Response<GetDieuChinhTsDetailViewModel>> Handle(GetDieuChinhTsDetailQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    var timesheet = await _timesheetRepository.S2_GetDieuChinhDetail(query.Id);
                    if (timesheet == null)
                        return new Response<GetDieuChinhTsDetailViewModel>($"Timesheet Id {query.Id} Not Found.");
                    return new Response<GetDieuChinhTsDetailViewModel>(timesheet);
                }
                catch (Exception ex)
                {
                    throw new ApiException(ex.Message);
                }
            }
        }
    }
}
