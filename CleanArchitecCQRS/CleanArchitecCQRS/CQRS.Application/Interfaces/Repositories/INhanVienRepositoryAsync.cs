using EsuhaiHRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Interfaces.Repositories
{
    public interface INhanVienRepositoryAsync: IGenericRepositoryAsync<NhanVien>
    {
        Task<NhanVien> S2_GetByIdAsync(Guid? guid);
        Task<NhanVien> S2_GetByUsernameAsync(string username);
        Task<IReadOnlyList<NhanVien>> S2_GetPagedReponseAsync(int pageNumber
                                                            , int pageSize
                                                            , string sortParam
                                                            , string filterParams
                                                            , string searchParam);

        Task<IReadOnlyList<NhanVien>> S2_GetPagedReponseAsyncForPublic(int pageNumber
                                                                     , int pageSize
                                                                     , string sortParam
                                                                     , string filterParams
                                                                     , string searchParam);
        string GenerateMaNV(int congTyId);

        Task<IReadOnlyList<NhanVien>> S2_GetListReponseAsync( int pageNumber
                                                            , int pageSize
                                                            , int? phongBanId);
        Task<int> GetToTalItem();
        Task<string> GetAvatarByUserIdAsync(string userId);
        Task<bool> IsUniqueUsernameAsync(string username);
        Task<string> CreateAccountForUser(Guid NhanVienId);
        Task<IReadOnlyList<NhanVien>> S2_GetListForBirthDay(int pageNumber, int pageSize, DateTime fromDate, DateTime toDate);
        Task<IReadOnlyList<NhanVien>> S2_GetListForThuViec(int pageNumber, int pageSize, DateTime fromDate, DateTime toDate);
        Task<string> GetEmailById(Guid nhanvienId);
    }
}
