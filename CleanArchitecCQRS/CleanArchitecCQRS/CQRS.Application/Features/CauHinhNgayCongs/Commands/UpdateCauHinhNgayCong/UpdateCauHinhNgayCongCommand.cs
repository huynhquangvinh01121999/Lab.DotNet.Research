using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.CauHinhNgayCongs.Commands.UpdateCauHinhNgayCong
{
    public class UpdateCauHinhNgayCongCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public int? Thang { get; set; }
        public int? Nam { get; set; }
        public float? TongNgayCong { get; set; }
        public bool? ChotCong { get; set; }
    }

    public class UpdateCauHinhNgayCongCommandHandler : IRequestHandler<UpdateCauHinhNgayCongCommand, Response<string>>
    {
        private readonly ICauHinhNgayCongRepositoryAsync _cauHinhNgayCongRepositoryAsync;

        public UpdateCauHinhNgayCongCommandHandler(ICauHinhNgayCongRepositoryAsync cauHinhNgayCongRepositoryAsync)
        {
            _cauHinhNgayCongRepositoryAsync = cauHinhNgayCongRepositoryAsync;
        }

        public async Task<Response<string>> Handle(UpdateCauHinhNgayCongCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var chnc = await _cauHinhNgayCongRepositoryAsync.S2_GetCauHinhNgayCongByIdAsync(request.Id);

                if (chnc == null)
                    return new Response<string>($"CHC002");

                if (request.Thang > 12 || request.Thang < 1)
                    return new Response<string>($"CHC003");

                var chncByDate = await _cauHinhNgayCongRepositoryAsync.S2_GetCauHinhNgayCongByDate(request.Thang, request.Nam);
                if(chncByDate != null && chncByDate.Id != chnc.Id)
                    return new Response<string>($"CHC001");

                if((request.TongNgayCong != null) && (request.TongNgayCong > DateTime.DaysInMonth((int)request.Nam, (int)request.Thang)))
                    return new Response<string>($"CHC004");

                chnc.Thang = request.Thang == null ? chnc.Thang : request.Thang;
                chnc.Nam = request.Nam == null ? chnc.Nam : request.Nam;
                chnc.TongNgayCong = request.TongNgayCong;
                chnc.ChotCong = request.ChotCong == null ? chnc.ChotCong : request.ChotCong;

                await _cauHinhNgayCongRepositoryAsync.UpdateAsync(chnc);

                return new Response<string>(chnc.Id.ToString(), null);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}
