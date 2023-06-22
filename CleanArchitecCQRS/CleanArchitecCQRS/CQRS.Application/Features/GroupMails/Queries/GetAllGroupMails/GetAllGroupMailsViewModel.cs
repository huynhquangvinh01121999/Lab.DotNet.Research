using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Application.Features.GroupMails.Queries.GetAllGroupMails
{
    public class GetAllGroupMailsViewModel
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public Guid? NhanVienDeNghiId { get; set; }
        public string NhanVienDeNghiHoTen { get; set; }
        public DateTime? NgayTao { get; set; }
        public string MucDich { get; set; }
        public string MoTa { get; set; }
        public string PhanLoai { get; set; }
        public string GhiChu { get; set; }
        public int TongNhanVien { get; set; }
        public IList<GetAllGroupMailNhanViensViewModel> NhanViens { get; set; }
    }
}
