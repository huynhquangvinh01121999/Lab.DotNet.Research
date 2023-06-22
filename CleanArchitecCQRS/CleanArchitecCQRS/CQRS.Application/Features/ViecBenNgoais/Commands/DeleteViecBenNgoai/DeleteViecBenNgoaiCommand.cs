using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.ViecBenNgoais.Commands.DeleteViecBenNgoai
{
    public class DeleteViecBenNgoaiCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public Guid NhanVienId { get; set; }
    }

    public class DeleteViecBenNgoaiCommandHandler : IRequestHandler<DeleteViecBenNgoaiCommand, Response<string>>
    {
        private readonly IViecBenNgoaiRepositoryAsync _viecBenNgoaiRepositoryAsync;
        private readonly INhanVienRepositoryAsync _nhanVienRepositoryAsync;

        public DeleteViecBenNgoaiCommandHandler(IViecBenNgoaiRepositoryAsync viecBenNgoaiRepositoryAsync, INhanVienRepositoryAsync nhanVienRepositoryAsync)
        {
            _viecBenNgoaiRepositoryAsync = viecBenNgoaiRepositoryAsync;
            _nhanVienRepositoryAsync = nhanVienRepositoryAsync;
        }

        public async Task<Response<string>> Handle(DeleteViecBenNgoaiCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // kiem tra vbn co ton tai ko?
                var viecBenNgoai = await _viecBenNgoaiRepositoryAsync.S2_GetByGuidAsync(request.Id);
                if (viecBenNgoai == null)
                    //return new Response<string>($"ViecBenNgoai {request.Id} khong tim thay!");
                    return new Response<string>("VBN001");

                // kiem tra user đang login co trung vs user so huu don vbn can dieu chinh ko?
                if (viecBenNgoai.NhanVienId != request.NhanVienId)
                    //return new Response<string>($"NhanVienId {request.NhanVienId} khong hop le!");
                    return new Response<string>("VBN006");

                // kiem tra user gui don dieu chinh vbn co ton tai ko?
                var nhanvien = await _nhanVienRepositoryAsync.S2_GetByIdAsync(request.NhanVienId);
                if (nhanvien == null)
                    //return new Response<string>($"NhanVienId {request.NhanVienId} khong tim thay!");
                    return new Response<string>("VBN002");

                // kiem tra don da duoc duyet chua
                //if(viecBenNgoai.TrangThaiXetDuyet != null)
                //    return new Response<string>($"Ban khong co quyen xoa don nay!");

                await _viecBenNgoaiRepositoryAsync.DeleteAsync(viecBenNgoai);

                return new Response<string>(viecBenNgoai.Id.ToString(), null);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}
