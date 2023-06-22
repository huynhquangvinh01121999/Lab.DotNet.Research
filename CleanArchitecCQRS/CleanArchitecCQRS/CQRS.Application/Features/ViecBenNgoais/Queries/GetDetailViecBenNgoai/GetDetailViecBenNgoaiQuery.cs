using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.ViecBenNgoais.Queries.GetDetailViecBenNgoai
{
    public class GetDetailViecBenNgoaiQuery : IRequest<Response<GetDetailViecBenNgoaiViewModel>>
    {
        public Guid Id { get; set; }
        public Guid NhanVienId { get; set; }
    }

    public class GetDetailViecBenNgoaiQueryHandler : IRequestHandler<GetDetailViecBenNgoaiQuery, Response<GetDetailViecBenNgoaiViewModel>>
    {
        private readonly IViecBenNgoaiRepositoryAsync _viecBenNgoaiRepositoryAsync;

        public GetDetailViecBenNgoaiQueryHandler(IViecBenNgoaiRepositoryAsync viecBenNgoaiRepositoryAsync)
        {
            _viecBenNgoaiRepositoryAsync = viecBenNgoaiRepositoryAsync;
        }

        public async Task<Response<GetDetailViecBenNgoaiViewModel>> Handle(GetDetailViecBenNgoaiQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var viecBenNgoai = await _viecBenNgoaiRepositoryAsync.S2_GetDetailByGuidAsync(request.Id, request.NhanVienId);
                if (viecBenNgoai == null)
                    //return new Response<GetDetailViecBenNgoaiViewModel>($"ViecBenNgoai Id {request.Id} not found.");
                    return new Response<GetDetailViecBenNgoaiViewModel>("VBN001");

                return new Response<GetDetailViecBenNgoaiViewModel>(viecBenNgoai);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}
