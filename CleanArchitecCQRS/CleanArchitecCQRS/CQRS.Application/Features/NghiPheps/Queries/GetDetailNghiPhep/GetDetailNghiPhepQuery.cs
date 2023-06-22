using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.NghiPheps.Queries.GetDetailNghiPhep
{
    public class GetDetailNghiPhepQuery : IRequest<Response<GetDetailNghiPhepViewModel>>
    {
        public Guid Id { get; set; }
        public Guid NhanVienId { get; set; }
    }

    public class GetDetailNghiPhepQueryHandler : IRequestHandler<GetDetailNghiPhepQuery, Response<GetDetailNghiPhepViewModel>>
    {
        private readonly INghiPhepRepositoryAsync _nghiPhepRepositoryAsync;

        public GetDetailNghiPhepQueryHandler(INghiPhepRepositoryAsync nghiPhepRepositoryAsync)
        {
            _nghiPhepRepositoryAsync = nghiPhepRepositoryAsync;
        }

        public async Task<Response<GetDetailNghiPhepViewModel>> Handle(GetDetailNghiPhepQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var nghiPhep = await _nghiPhepRepositoryAsync.S2_GetNghiPhepDetailAsync(request.Id, request.NhanVienId);
                if (nghiPhep == null)
                    //return new Response<GetDetailNghiPhepViewModel>($"NghiPhep Id {request.Id} not found.");
                    return new Response<GetDetailNghiPhepViewModel>("NPH001");

                return new Response<GetDetailNghiPhepViewModel>(nghiPhep);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}
