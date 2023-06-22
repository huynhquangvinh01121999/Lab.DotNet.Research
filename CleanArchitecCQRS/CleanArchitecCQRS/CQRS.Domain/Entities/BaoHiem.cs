using EsuhaiHRM.Domain.Entities;
using EsuhaiHRM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Entities
{
    public class BaoHiem : AuditableBaseEntity
    {
        public int Id { get; set; }
		public Guid NhanVienId { get; set; }
		public string SoBHXH { get; set; }
		public string SoBHYT { get; set; }
        public DateTime? NgayNhanSo { get; set; }
        public DateTime? NgayTraSo { get; set; }
        public bool? GiuSo { get; set; }
        public int? XaId { get; set; }
        public int? HuyenId { get; set; }
        public int? TinhId { get; set; }
        public string DiaChi { get; set; }
        public string GhiChu { get; set; }
        public NhanVien NhanVien { get; set; }
        public Xa BaoHiemXa { get; set; }
        public Huyen BaoHiemHuyen { get; set; }
        public TinhThanh BaoHiemTinh { get; set; }
    }
}
