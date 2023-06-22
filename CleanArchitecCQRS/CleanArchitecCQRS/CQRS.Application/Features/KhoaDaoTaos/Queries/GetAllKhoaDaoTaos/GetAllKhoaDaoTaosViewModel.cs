using EsuhaiHRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Application.Features.KhoaDaoTaos.Queries.GetAllKhoaDaoTaos
{
    public class GetAllKhoaDaoTaosViewModel
    {
        public int Id { get; set; }
        public string TenVN { get; set; }
        public string TenJP { get; set; }
        public DateTime? NgayDaoTao { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string NguoiDaoTao { get; set; }
        public string MucDich { get; set; }
        public string DiaDiem { get; set; }
        public string GhiChu { get; set; }
        public int TongNhanVien { get; set; }
        public IList<GetAllKhoaDaoTaoNhanViensViewModel> NhanViens { get; set; }
    }
}
