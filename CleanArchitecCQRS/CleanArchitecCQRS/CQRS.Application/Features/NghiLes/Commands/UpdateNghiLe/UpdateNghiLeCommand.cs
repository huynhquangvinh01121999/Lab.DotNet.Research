using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.NghiLes.Commands.UpdateNghiLe
{
    public class UpdateNghiLeCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DateTime? Ngay { get; set; }
        public DateTime? NgayCoDinh { get; set; }
        public string MoTa { get; set; }
    }

    public class UpdateNghiLeCommandHandler : IRequestHandler<UpdateNghiLeCommand, Response<string>>
    {
        private readonly INghiLeRepositoryAsync _nghiLeRepositoryAsync;

        public UpdateNghiLeCommandHandler(INghiLeRepositoryAsync nghiLeRepositoryAsync)
        {
            _nghiLeRepositoryAsync = nghiLeRepositoryAsync;
        }

        public async Task<Response<string>> Handle(UpdateNghiLeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var nghile = await _nghiLeRepositoryAsync.S2_GetNghiLeByIdAsync(request.Id);
                
                if (nghile == null)
                    return new Response<string>($"NGL002");

                var isExist = await _nghiLeRepositoryAsync.S2_isExistNghiLe(request.Ngay, request.NgayCoDinh);
                if(isExist)
                    return new Response<string>($"NGL001");

                nghile.Ngay = request.Ngay == null ? nghile.Ngay : request.Ngay;
                nghile.NgayCoDinh = request.NgayCoDinh == null ? nghile.NgayCoDinh : request.NgayCoDinh;
                nghile.MoTa = request.MoTa;

                await _nghiLeRepositoryAsync.UpdateAsync(nghile);

                return new Response<string>(nghile.Id.ToString(), null);
            } catch(Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}
