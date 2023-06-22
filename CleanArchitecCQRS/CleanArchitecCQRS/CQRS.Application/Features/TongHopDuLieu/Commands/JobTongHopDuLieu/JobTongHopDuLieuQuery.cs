using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.TongHopDuLieu.Commands.JobTongHopDuLieu
{
    public class JobTongHopDuLieuQuery : IRequest<Response<string>>
    {
        public int Thang { get; set; }
        public int Nam { get; set; }
    }

    public class JobTongHopDuLieuQueryHandler : IRequestHandler<JobTongHopDuLieuQuery, Response<string>>
    {
        private readonly ITongHopDuLieuRepositoryAsync _tonghopdulieuRepository;

        public JobTongHopDuLieuQueryHandler(ITongHopDuLieuRepositoryAsync tonghopdulieuRepository)
        {
            _tonghopdulieuRepository = tonghopdulieuRepository;
        }

        public async Task<Response<string>> Handle(JobTongHopDuLieuQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _tonghopdulieuRepository.S2_JobTongHopDuLieu(request.Thang, request.Nam);
                return new Response<string>(result.ToString(), "Successed");
            }
            catch (Exception ex)
            {
                return new Response<string>(ex.Message);
            }
        }
    }
}
