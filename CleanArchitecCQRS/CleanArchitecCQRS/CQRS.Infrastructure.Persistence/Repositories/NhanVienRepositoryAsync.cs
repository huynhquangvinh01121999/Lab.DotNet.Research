using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Domain.Entities;
using EsuhaiHRM.Infrastructure.Persistence.Contexts;
using EsuhaiHRM.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using EsuhaiHRM.Infrastructure.Identity.Seeds;
using EsuhaiHRM.Infrastructure.Identity.Contexts;
using EsuhaiHRM.Application.Exceptions;
using EsuhaiHRM.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using EsuhaiHRM.Application.Enums;

namespace EsuhaiHRM.Infrastructure.Persistence.Repositories
{
    public class NhanVienRepositoryAsync : GenericRepositoryAsync<NhanVien>, INhanVienRepositoryAsync
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DbSet<NhanVien> _nhanViens;
        private readonly DbSet<DanhMuc> _danhMucs;
        private readonly ApplicationDbContext _dbContext;
        private readonly IdentityContext _idenContext;
        private int _totalItem = 0;

        public NhanVienRepositoryAsync(ApplicationDbContext dbContext,
                                       IdentityContext idenContext,
                                       UserManager<ApplicationUser> userManager) : base(dbContext)
        {
            _userManager = userManager;
            _nhanViens = dbContext.Set<NhanVien>();
            _danhMucs = dbContext.Set<DanhMuc>();
            _dbContext = dbContext;
            _idenContext = idenContext;
        }
        public async Task<NhanVien> S2_GetByIdAsync(Guid? guid)
        {
            return await _dbContext.NhanViens.Where(n => n.Deleted != true)
                                             .Include(n => n.TenCongTy)
                                             .Include(n => n.TenChucDanh)
                                             .Include(n => n.TenChucVu)
                                             .Include(n => n.QuocTich)
                                             .Include(n => n.HonNhan)
                                             .Include(n => n.GioiTinh)
                                             .Include(n => n.TonGiao)
                                             .Include(n => n.DanToc)
                                             .Include(n => n.NguyenQuan)
                                             .Include(n => n.NoiSinh)
                                             .Include(n => n.TenPhong)
                                             .Include(n => n.TenBan)
                                             .Include(n => n.TenNhom)
                                             .Include(n => n.TrangThai)
                                             .Include(n => n.NhanVienXetDuyetCap1)
                                             .Include(n => n.NhanVienXetDuyetCap2)
                                             .Include(n => n.CaLamViec)
                                             .FirstOrDefaultAsync(n => n.Id == guid);
        }

        public async Task<IReadOnlyList<NhanVien>> S2_GetPagedReponseAsync(int pageNumber
                                                                         , int pageSize
                                                                         , string sortParam
                                                                         , string filterParams
                                                                         , string searchParam) {
            string[] sortParamlst;
            string[] filterList = null;
            var sortNm = "";
            var sortTp = "";
            var strWhere = "";
            //sort param exists
            if (sortParam != null)
            {
                //get param sort
                sortParamlst = sortParam.Split(";");
                //get sort column
                sortNm = sortParamlst[0];
                //get sort type [asc/desc]
                sortTp = sortParamlst[1];
            }
            //filter params exists
            if (filterParams != null)
            {
                //get param filter
                filterList = filterParams.Split("#");
                //whereClause looping
                strWhere += " WHERE 1 = 1 ";
                for (int i = 0; i < filterList.Length; i++)
                {
                    if(filterList[i] != null)
                    {
                        var filterCol = filterList[i].Split(";")[0];
                        var filterVal = filterList[i].Split(";")[1];
                        var type = typeof(NhanVien).GetProperty(filterCol);
                        if (type.PropertyType.Name.ToString() == "String")
                        {
                            strWhere += string.Format(" AND {0} like N'%{1}%'", filterCol, filterVal);
                        }
                        else
                        {
                            if (filterCol == "ThamNien")
                            {
                                var ThamNienTruoc = filterVal.Split("-")[0];
                                var ThamNienSau = filterVal.Split("-")[1];
                                if (ThamNienTruoc == ThamNienSau)
                                {
                                    strWhere += string.Format(" AND {0} = '{1}'", filterCol, ThamNienTruoc);
                                }
                                else
                                {
                                    strWhere += string.Format(" AND {0} >= '{1}' AND {0} <= '{2}' ", filterCol, ThamNienTruoc, ThamNienSau);
                                }
                            }
                            else
                            {
                                string listIn = "";
                                if (filterVal.Contains(","))
                                {
                                    var listFilter = filterVal.Split(",");
                                    for(int f = 0; f < listFilter.Length; f++)
                                    {
                                        if(listIn == "")
                                        {
                                            listIn = listFilter[f];
                                        }
                                        else
                                        {
                                            listIn += "," + listFilter[f];
                                        }
                                    }
                                    strWhere += string.Format(" AND {0} IN ({1})", filterCol, listIn);
                                }
                                else
                                {
                                    strWhere += string.Format(" AND {0} = '{1}'", filterCol, filterVal);
                                }
                            }
                        }
                    }
                }
            }
            //search params
            if (searchParam != null)
            {
                strWhere = " ";
                strWhere += string.Format(" WHERE MaNhanVien like N'%{0}%' " +
                                          " OR    HoTenDemVN +' '+ TenVN like N'%{0}%'", searchParam);
            }

            //main model data
            var rsNhanVien = _dbContext.NhanViens.FromSqlRaw(string.Format(" SELECT * FROM [{0}].NhanViens {1}",Schemas.NHANSU, strWhere));
                                    
            //main model with sort
            if (sortNm != null && sortTp.ToLower() == "descend")
            {
                this._totalItem = rsNhanVien.Count();
                return await rsNhanVien.Where(n => n.Deleted != true) 
                                       .OrderByDescending(nv => EF.Property<NhanVien>(nv, sortNm))
                                       .Skip((pageNumber - 1) * pageSize)
                                       .Take(pageSize)
                                       .AsNoTracking()
                                       .Include(n => n.TenCongTy)
                                       .Include(n => n.TenChucDanh)
                                       .Include(n => n.TenChucVu)
                                       .Include(n => n.HonNhan)
                                       .Include(n => n.GioiTinh)
                                       .Include(n => n.TonGiao)
                                       .Include(n => n.QuocTich)
                                       .Include(n => n.NguyenQuan)
                                       .Include(n => n.NoiSinh)
                                       .Include(n => n.TenPhong)
                                       .Include(n => n.TenBan)
                                       .Include(n => n.TenNhom)
                                       .Include(n => n.TrangThai)
                                       .Include(n => n.NhanVienXetDuyetCap1)
                                       .Include(n => n.NhanVienXetDuyetCap2)
                                       .Include(n => n.DanToc)
                                       .Include(n => n.SoBaoHiem)
                                       .Include(n => n.NhanVien_GroupMails)
                                            .ThenInclude(n => n.GroupMail)
                                       .Include(n => n.CaLamViec)
                                       .ToListAsync();
            }
            else if (sortNm != null && sortTp.ToLower() == "ascend")
            {
                this._totalItem = rsNhanVien.Count();
                return await rsNhanVien.Where(n => n.Deleted != true)
                                       .OrderBy(nv => EF.Property<NhanVien>(nv, sortNm))
                                       .Skip((pageNumber - 1) * pageSize)
                                       .Take(pageSize)
                                       .AsNoTracking()
                                       .Include(n => n.TenCongTy)
                                       .Include(n => n.TenChucDanh)
                                       .Include(n => n.TenChucVu)
                                       .Include(n => n.HonNhan)
                                       .Include(n => n.GioiTinh)
                                       .Include(n => n.TonGiao)
                                       .Include(n => n.NguyenQuan)
                                       .Include(n => n.NoiSinh)
                                       .Include(n => n.TenPhong)
                                       .Include(n => n.TenBan)
                                       .Include(n => n.TenNhom)
                                       .Include(n => n.TrangThai)
                                       .Include(n => n.NhanVienXetDuyetCap1)
                                       .Include(n => n.NhanVienXetDuyetCap2)
                                       .Include(n => n.QuocTich)
                                       .Include(n => n.DanToc)
                                       .Include(n => n.SoBaoHiem)
                                       .Include(n => n.NhanVien_GroupMails)
                                            .ThenInclude(n => n.GroupMail)
                                       .Include(n => n.CaLamViec)
                                       .ToListAsync();
            }
            else
            {
                this._totalItem = rsNhanVien.Count();
                return await rsNhanVien.Where(n => n.Deleted != true)
                                       .OrderBy(m => m.TrangThai.TenVN == null).ThenBy(n => n.TrangThai.TenVN)
                                       .ThenBy(n => n.TenChucVu.CapBac == null).ThenBy(n => n.TenChucVu.CapBac)
                                       .ThenByDescending(n => n.ThamNien)
                                       .Skip((pageNumber - 1) * pageSize)
                                       .Take(pageSize)
                                       .AsNoTracking()
                                       .Include(n => n.TenCongTy)
                                       .Include(n => n.TenChucDanh)
                                       .Include(n => n.TenChucVu)
                                       .Include(n => n.HonNhan)
                                       .Include(n => n.GioiTinh)
                                       .Include(n => n.TonGiao)
                                       .Include(n => n.NguyenQuan)
                                       .Include(n => n.NoiSinh)
                                       .Include(n => n.TenPhong)
                                       .Include(n => n.TenBan)
                                       .Include(n => n.TenNhom)
                                       .Include(n => n.TrangThai)
                                       .Include(n => n.NhanVienXetDuyetCap1)
                                       .Include(n => n.NhanVienXetDuyetCap2)
                                       .Include(n => n.QuocTich)
                                       .Include(n => n.DanToc)
                                       .Include(n => n.SoBaoHiem)
                                       .Include(n => n.NhanVien_GroupMails)
                                            .ThenInclude(n => n.GroupMail)
                                       .Include(n => n.CaLamViec)
                                       //.Include(n => n.NhanVien_ThongTinHocVans)
                                       //     .ThenInclude(n => n.TrinhDo)
                                       //.Include(n => n.NhanVien_CongTacs)
                                       //.Include(n => n.NhanVien_CMNDs)
                                       .ToListAsync();
            }
        }

        public async Task<IReadOnlyList<NhanVien>> S2_GetPagedReponseAsyncForPublic(int pageNumber
                                                                                  , int pageSize
                                                                                  , string sortParam
                                                                                  , string filterParams
                                                                                  , string searchParam)
        {
            string[] sortParamlst;
            string[] filterList = null;
            var sortNm = "";
            var sortTp = "";
            var strWhere = "";
            //sort param exists
            if (sortParam != null)
            {
                //get param sort
                sortParamlst = sortParam.Split(";");
                //get sort column
                sortNm = sortParamlst[0];
                //get sort type [asc/desc]
                sortTp = sortParamlst[1];
            }
            //filter params exists
            if (filterParams != null)
            {
                //get param filter
                filterList = filterParams.Split("#");
                //whereClause looping
                strWhere += " WHERE 1 = 1 ";
                for (int i = 0; i < filterList.Length; i++)
                {
                    if (filterList[i] != null)
                    {
                        var filterCol = filterList[i].Split(";")[0];
                        var filterVal = filterList[i].Split(";")[1];
                        var type = typeof(NhanVien).GetProperty(filterCol);
                        if (type.PropertyType.Name.ToString() == "String")
                        {
                            strWhere += string.Format(" AND {0} like N'%{1}%'", filterCol, filterVal);
                        }
                        else
                        {
                            if (filterCol == "ThamNien")
                            {
                                var ThamNienTruoc = filterVal.Split("-")[0];
                                var ThamNienSau = filterVal.Split("-")[1];
                                if (ThamNienTruoc == ThamNienSau)
                                {
                                    strWhere += string.Format(" AND {0} = '{1}'", filterCol, ThamNienTruoc);
                                }
                                else
                                {
                                    strWhere += string.Format(" AND {0} >= '{1}' AND {0} <= '{2}' ", filterCol, ThamNienTruoc, ThamNienSau);
                                }
                            }
                            else
                            {
                                string listIn = "";
                                if (filterVal.Contains(","))
                                {
                                    var listFilter = filterVal.Split(",");
                                    for (int f = 0; f < listFilter.Length; f++)
                                    {
                                        if (listIn == "")
                                        {
                                            listIn = listFilter[f];
                                        }
                                        else
                                        {
                                            listIn += "," + listFilter[f];
                                        }
                                    }
                                    strWhere += string.Format(" AND {0} IN ({1})", filterCol, listIn);
                                }
                                else
                                {
                                    strWhere += string.Format(" AND {0} = '{1}'", filterCol, filterVal);
                                }
                            }
                        }
                    }
                }
            }
            //search params
            if (searchParam != null)
            {
                strWhere = " ";
                strWhere += string.Format(" WHERE MaNhanVien like N'%{0}%' " +
                                          " OR    HoTenDemVN +' '+ TenVN like N'%{0}%'", searchParam);
            }

            //main model data
            var rsNhanVien = _dbContext.NhanViens.FromSqlRaw(string.Format(" SELECT * FROM [{0}].NhanViens {1}", Schemas.NHANSU, strWhere));

            //Get nhan vien working
            var rsNhanVienLamViec = from nv in rsNhanVien
                                    join dm in _dbContext.DanhMucs.Where(d => d.PhanLoai == "TrangThai"
                                                                           && d.TenVN != "Thôi việc")
                                    on nv.TrangThaiId equals dm.Id
                                    select nv;

            //main model with sort
            if (sortNm != null && sortTp.ToLower() == "descend")
            {
                this._totalItem = rsNhanVienLamViec.Count();
                return await rsNhanVienLamViec.Where(n => n.Deleted != true)
                                       .OrderByDescending(nv => EF.Property<NhanVien>(nv, sortNm))
                                       .Skip((pageNumber - 1) * pageSize)
                                       .Take(pageSize)
                                       .AsNoTracking()
                                       //.Include(n => n.TenCongTy)
                                       //.Include(n => n.TenChucDanh)
                                       //.Include(n => n.TonGiao)
                                       //.Include(n => n.NguyenQuan)
                                       //.Include(n => n.NoiSinh)
                                       //.Include(n => n.TenNhom)
                                       //.Include(n => n.TrangThai)
                                       //.Include(n => n.NhanVienXetDuyetCap1)
                                       //.Include(n => n.NhanVienXetDuyetCap2)
                                       .Include(n => n.TenChucVu)
                                       .Include(n => n.HonNhan)
                                       .Include(n => n.GioiTinh)
                                       .Include(n => n.QuocTich)
                                       .Include(n => n.TenPhong)
                                       .Include(n => n.TenBan)
                                       .Include(n => n.DanToc)
                                       .Include(n => n.NhanVien_GroupMails)
                                            .ThenInclude(n => n.GroupMail)
                                       .ToListAsync();
            }
            else if (sortNm != null && sortTp.ToLower() == "ascend")
            {
                this._totalItem = rsNhanVienLamViec.Count();
                return await rsNhanVienLamViec.Where(n => n.Deleted != true)
                                       .OrderBy(nv => EF.Property<NhanVien>(nv, sortNm))
                                       .Skip((pageNumber - 1) * pageSize)
                                       .Take(pageSize)
                                       .AsNoTracking()
                                       //.Include(n => n.TenCongTy)
                                       //.Include(n => n.TenChucDanh)
                                       //.Include(n => n.TonGiao)
                                       //.Include(n => n.NguyenQuan)
                                       //.Include(n => n.NoiSinh)
                                       //.Include(n => n.TenNhom)
                                       //.Include(n => n.TrangThai)
                                       //.Include(n => n.NhanVienXetDuyetCap1)
                                       //.Include(n => n.NhanVienXetDuyetCap2)
                                       .Include(n => n.TenChucVu)
                                       .Include(n => n.HonNhan)
                                       .Include(n => n.GioiTinh)
                                       .Include(n => n.QuocTich)
                                       .Include(n => n.TenPhong)
                                       .Include(n => n.TenBan)
                                       .Include(n => n.DanToc)
                                       .Include(n => n.NhanVien_GroupMails)
                                            .ThenInclude(n => n.GroupMail)
                                       .ToListAsync();
            }
            else
            {
                this._totalItem = rsNhanVienLamViec.Count();
                return await rsNhanVienLamViec.Where(n => n.Deleted != true)
                                       .OrderBy(n => n.TenChucVu.CapBac)
                                       .ThenByDescending(n => n.ThamNien)
                                       .Skip((pageNumber - 1) * pageSize)
                                       .Take(pageSize)
                                       .AsNoTracking()
                                       //.Include(n => n.TenCongTy)
                                       //.Include(n => n.TenChucDanh)
                                       //.Include(n => n.TonGiao)
                                       //.Include(n => n.NguyenQuan)
                                       //.Include(n => n.NoiSinh)
                                       //.Include(n => n.TenNhom)
                                       //.Include(n => n.TrangThai)
                                       //.Include(n => n.NhanVienXetDuyetCap1)
                                       //.Include(n => n.NhanVienXetDuyetCap2)
                                       .Include(n => n.TenChucVu)
                                       .Include(n => n.HonNhan)
                                       .Include(n => n.GioiTinh)
                                       .Include(n => n.QuocTich)
                                       .Include(n => n.TenPhong)
                                       .Include(n => n.TenBan)
                                       .Include(n => n.DanToc)
                                       .Include(n => n.NhanVien_GroupMails)
                                            .ThenInclude(n => n.GroupMail)
                                       .ToListAsync();
            }
        }

        public string GenerateMaNV(int congTyId)
        {
            string maNV = "";
            var congTy = _dbContext.CongTys.SingleOrDefault(n => n.Id == congTyId);
            if (congTy != null)
            {
                string _code = congTy.Code;
                var listNVs = _dbContext.NhanViens.Where(nv => nv.MaNhanVien.StartsWith(_code)).OrderByDescending(nv => nv.MaNhanVien);
                
                if(listNVs.Count() > 0)
                {
                    string _stt = listNVs.FirstOrDefault().MaNhanVien.Substring(_code.Length, 3);

                    int _intSTT = int.Parse(_stt) + 1;

                    maNV = string.Format("{0}{1}", _code, _intSTT.ToString().PadLeft(3, '0'));
                } else
                {
                    maNV = _code + "001";
                }                
            }

            return maNV;
        }

        public async Task<IReadOnlyList<NhanVien>> S2_GetListReponseAsync(int pageNumber, int pageSize,int? phongBanId)
        { 
            if (phongBanId == null)
            {
                return await _dbContext.NhanViens.Where(n => n.Deleted != true)
                                                 .OrderBy(nv => nv.MaNhanVien)
                                                 .Skip((pageNumber - 1) * pageSize)
                                                 .Take(pageSize)
                                                 .AsNoTracking()
                                                 .ToListAsync();
            }
            else
            {
                return await _dbContext.NhanViens.Where(nv => nv.PhongId == phongBanId && nv.Deleted != true)
                                                 .OrderBy(nv => nv.MaNhanVien)
                                                 .Skip((pageNumber - 1) * pageSize)
                                                 .Take(pageSize)
                                                 .AsNoTracking()
                                                 .ToListAsync();
            }
        }

        public async Task<int> GetToTalItem()
        {
            return _totalItem;
        }

        public async Task<string> GetAvatarByUserIdAsync(string userId)
        {
            var getNhanVienId = _idenContext.Users.Where(us => us.Id == userId)
                                                  .Select(us => us.NhanVienId)
                                                  .FirstOrDefault();
            if(getNhanVienId != null)
            {
                return _dbContext.NhanViens.Where(nv => nv.Id == getNhanVienId)
                                           .Select(nv => nv.Avatar)
                                           .FirstOrDefault();
            }
            else
            {
                return await Task.FromResult("");
            }
        }

        public Task<bool> IsUniqueUsernameAsync(string username)
        {
            return _nhanViens.AllAsync(p => p.Username != username);
        }

        public async Task<string> CreateAccountForUser(Guid nhanvienId)
        {
            try
            {
                var newGuid = Guid.NewGuid().ToString();
                //get nhan vien info
                var nhanvien = _nhanViens.Where(nv => nv.Id == nhanvienId && nv.Deleted != true)
                                         .Select(nv => new
                                         {
                                             UserId = nv.AccountId,
                                             Username = nv.Username,
                                             Email = nv.EmailCongTy,
                                             PhoneNumber = nv.DienThoaiCaNhan,
                                             FirstName = nv.TenVN,
                                             LastName = nv.HoTenDemVN,
                                             NhanVienId = nv.Id
                                         })
                                         .FirstOrDefault();
                if(nhanvien != null)
                {
                    var userWithSameUserName = await _userManager.FindByNameAsync(nhanvien.Username);
                    if (userWithSameUserName != null)
                    {
                        throw new ApiException($"Username '{nhanvien.Username}' is already taken.");
                    }
                    var user = new ApplicationUser
                    {
                        Id = nhanvien.UserId,
                        Email = nhanvien.Email,
                        FirstName = nhanvien.FirstName,
                        LastName = nhanvien.LastName,
                        UserName = nhanvien.Username,
                        EmailConfirmed = true,
                        NhanVienId = nhanvien.NhanVienId,
                    };

                    var userWithSameEmail = await _userManager.FindByEmailAsync(nhanvien.Email);
                    if (userWithSameEmail == null)
                    {
                        var result = await _userManager.CreateAsync(user);
                        if (result.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(user, "User");
                            return nhanvien.Username;
                        }
                        else
                        {
                            throw new ApiException($"{result.Errors}");
                        }
                    }
                    else
                    {
                        throw new ApiException($"Email {nhanvien.Email } is already registered.");
                    }
                }
                else
                {
                    throw new ApiException("NhanVien Not Found.");
                }
            }
            catch (Exception ex)
            {
                throw new ApiException($"Error:{ex.Message}");
            }
        }

        public async Task<IReadOnlyList<NhanVien>> S2_GetListForBirthDay(int pageNumber, int pageSize, DateTime fromDate, DateTime toDate)
        {
            var nhanviens = from nv in _nhanViens
                            join dm in _danhMucs
                            on nv.TrangThaiId equals dm.Id into leftjoin
                            from lf in leftjoin.DefaultIfEmpty()
                            where lf.TenVN != "Thôi việc"
                            && (   nv.NgaySinh.Value.Day >= fromDate.Day
                                && nv.NgaySinh.Value.Day <= toDate.Day
                                && nv.NgaySinh.Value.Month >= fromDate.Month 
                                && nv.NgaySinh.Value.Month <= toDate.Month)
                            orderby nv.NgaySinh.Value.Day
                            select nv;
            return await nhanviens.Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .AsNoTracking()
                                  .ToListAsync();
        }

        public async Task<IReadOnlyList<NhanVien>> S2_GetListForThuViec(int pageNumber, int pageSize, DateTime fromDate, DateTime toDate)
        {
            var nhanviens = from nv in _nhanViens
                            join dm in _danhMucs
                            on nv.TrangThaiId equals dm.Id into leftjoin
                            from lf in leftjoin.DefaultIfEmpty()
                            where (lf.TenVN == "Thử việc" || lf.TenVN == null)
                            && (nv.NgayBatDauLamViec >= fromDate && nv.NgayBatDauLamViec <= toDate)
                            select nv;
            return await nhanviens.Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .AsNoTracking()
                                  .ToListAsync();
        }

        public async Task<NhanVien> S2_GetByUsernameAsync(string username)
        {
            return await _dbContext.NhanViens.Where(n => n.Username.ToLower() == username.ToLower()).FirstOrDefaultAsync();
        }

        public Task<string> GetEmailById(Guid nhanvienId)
        {
            throw new NotImplementedException();
        }
    }
}
