using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetById
{
    public class GetTimesheetByIdQuery : IRequest<Response<Timesheet>>
    {
        public Guid Id { get; set; }
        public class GetTimesheetByIdQueryHandler : IRequestHandler<GetTimesheetByIdQuery, Response<Timesheet>>
        {
            private readonly ITimesheetRepositoryAsync _timesheetRepository;
            public GetTimesheetByIdQueryHandler(ITimesheetRepositoryAsync timesheetRepository)
            {
                _timesheetRepository = timesheetRepository;
            }
            public async Task<Response<Timesheet>> Handle(GetTimesheetByIdQuery query, CancellationToken cancellationToken)
            {
                var timesheet = await _timesheetRepository.S2_GetByGuidAsync(query.Id);
                if (timesheet == null) throw new ApiException($"Timesheet Not Found.");
                return new Response<Timesheet>(timesheet);
            }
        }
    }
}
