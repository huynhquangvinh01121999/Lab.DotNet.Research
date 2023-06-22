using EsuhaiHRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Application.Features.NhanViens.Queries.GetAllNhanViens
{
    public class GetAllNhanViensForDropDownViewModel
    {
        public Guid Id { get; set; }
        public string MaNhanVien { get; set; }
        public string HoTenVN { get; set; }
    }
}
