using AutoMapper;
using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.CauHinhNgayCongs.Commands.CreateCauHinhNgayCong
{
    public class CreateCauHinhNgayCongCommand : IRequest<Response<string>>
    {
        public int Thang { get; set; }
        public int Nam { get; set; }
        public float TongNgayCong { get; set; }
        public bool? ChotCong { get; set; }
    }

    public class CreateCauHinhNgayCongCommandHandler : IRequestHandler<CreateCauHinhNgayCongCommand, Response<string>>
    {
        private readonly ICauHinhNgayCongRepositoryAsync _cauHinhNgayCongRepositoryAsync;
        private readonly IMapper _mapper;

        public CreateCauHinhNgayCongCommandHandler(ICauHinhNgayCongRepositoryAsync cauHinhNgayCongRepositoryAsync, IMapper mapper)
        {
            _cauHinhNgayCongRepositoryAsync = cauHinhNgayCongRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateCauHinhNgayCongCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var chnc = await _cauHinhNgayCongRepositoryAsync.S2_GetCauHinhNgayCongByDate(request.Thang, request.Nam);
                if (chnc != null)
                    return new Response<string>($"CHC001");

                if(request.Thang > 12 || request.Thang < 1)
                    return new Response<string>($"CHC003");

                if (request.TongNgayCong > DateTime.DaysInMonth(request.Nam, request.Thang))
                    return new Response<string>($"CHC004");

                var cauHinhNgayCong = _mapper.Map<CauHinhNgayCong>(request);

                await _cauHinhNgayCongRepositoryAsync.AddAsync(cauHinhNgayCong);

                return new Response<string>(cauHinhNgayCong.Id.ToString(), null);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}
