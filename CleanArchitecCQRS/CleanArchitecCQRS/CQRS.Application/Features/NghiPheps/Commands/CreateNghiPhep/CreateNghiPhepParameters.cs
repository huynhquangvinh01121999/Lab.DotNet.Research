﻿using System;

namespace EsuhaiHRM.Application.Features.NghiPheps.Commands.CreateNghiPhep
{
    public class CreateNghiPhepParameters
    {
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public float SoNgayDangKy { get; set; }
        public string MoTa { get; set; }
        public string CongViecThayThe { get; set; }
        public Guid NhanVienThayTheId { get; set; }
    }
}
