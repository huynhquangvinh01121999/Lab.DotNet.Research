using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Features.NghiPheps.Commands.DeleteNghiPhep
{
    public class DeleteNghiPhepCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public Guid NhanVienId { get; set; }
    }

    public class DeleteNghiPhepCommandHandler : IRequestHandler<DeleteNghiPhepCommand, Response<string>>
    {
        private readonly INghiPhepRepositoryAsync _nghiPhepRepositoryAsync;
        private readonly INhanVienRepositoryAsync _nhanVienRepositoryAsync;

        public DeleteNghiPhepCommandHandler(INghiPhepRepositoryAsync nghiPhepRepositoryAsync, INhanVienRepositoryAsync nhanVienRepositoryAsync)
        {
            _nghiPhepRepositoryAsync = nghiPhepRepositoryAsync;
            _nhanVienRepositoryAsync = nhanVienRepositoryAsync;
        }

        public async Task<Response<string>> Handle(DeleteNghiPhepCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // lay ra thong tin don nghi phep
                var nghiPhep = await _nghiPhepRepositoryAsync.S2_GetByGuidAsync(request.Id);
                if (nghiPhep == null)
                    //return new Response<string>($"NghiPhep Id {request.Id} khong tim thay!");
                    return new Response<string>("NPH001");

                // kiem tra nhan vien gui don nghi phep co ton tai ko?
                var nhanvien = await _nhanVienRepositoryAsync.S2_GetByIdAsync(request.NhanVienId);
                if (nhanvien == null)
                    //return new Response<string>($"NhanVienId {request.NhanVienId} khong tim thay!");
                    return new Response<string>("NPH002");

                // kiem tra nhanVienId cua don nghi phep co khop voi nhanVienId dang login hay khong
                if (nghiPhep.NhanVienId != request.NhanVienId)
                    //return new Response<string>($"Ban khong co quyen xoa don nay!");
                    return new Response<string>("NPH006");

                // kiem tra don nghi phep da duoc TPB/HR duyet chua
                // neu co 1 trong 3 da duyet thi khong cho xoa don
                if((nghiPhep.NguoiXetDuyetCap1Id != null && nghiPhep.NXD1_TrangThai != null) 
                    || (nghiPhep.NguoiXetDuyetCap2Id != null && nghiPhep.NXD2_TrangThai != null) 
                    || nghiPhep.HR_TrangThai != null)
                    //return new Response<string>($"Khong the xoa don nay!");
                    return new Response<string>("NPH007");

                await _nghiPhepRepositoryAsync.DeleteAsync(nghiPhep);

                return new Response<string>(nghiPhep.Id.ToString(), null);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}
