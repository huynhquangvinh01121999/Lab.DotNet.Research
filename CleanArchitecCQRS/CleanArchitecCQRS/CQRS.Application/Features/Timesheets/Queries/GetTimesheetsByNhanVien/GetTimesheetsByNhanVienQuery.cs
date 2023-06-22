
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsByNhanVien
{
    public class GetTimesheetsByNhanVienQuery : IRequest<PagedResponse<IEnumerable<GetTimesheetsByNhanVienViewModel>>>
    {
        public int Thang { get; set; }
        public int Nam { get; set; }
        public Guid NhanVienId { get; set; }
    }
    public class GetTimesheetsByNhanVienQueryHandler : IRequestHandler<GetTimesheetsByNhanVienQuery, PagedResponse<IEnumerable<GetTimesheetsByNhanVienViewModel>>>
    {
        private readonly IMapper _mapper;
        private readonly ITimesheetRepositoryAsync _timesheetRepository;

        public GetTimesheetsByNhanVienQueryHandler(ITimesheetRepositoryAsync timesheetRepository, IMapper mapper)
        {
            _timesheetRepository = timesheetRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetTimesheetsByNhanVienViewModel>>> Handle(GetTimesheetsByNhanVienQuery request, CancellationToken cancellationToken)
        {
            int totalItems = 0;

            var validFilter = _mapper.Map<GetTimesheetsByNhanVienParameter>(request);
            var ts = await _timesheetRepository.S2_GetTimesheetsInMonnth(validFilter.NhanVienId, validFilter.Thang, validFilter.Nam);

            totalItems = await _timesheetRepository.GetTotalItem();
            var tsViewModel = _mapper.Map<IEnumerable<GetTimesheetsByNhanVienViewModel>>(ts);

            return new PagedResponse<IEnumerable<GetTimesheetsByNhanVienViewModel>>(tsViewModel, 1, 100, totalItems);
        }
    }
}
