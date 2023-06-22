using System;
using System.Collections.Generic;

namespace EsuhaiHRM.Application.Features.TongHopDuLieu.Queries.GetTongHopNgayCong
{
    public class GetTongHopNgayCongViewModel
    {
        public DateTime a_Ngay { get; set; }
        public float? b_NgayCong { get; set; }
        public string c_GioVao { get; set; }
        public string d_GioRa { get; set; }

        public float? e_nc_nghiphep_hl { get; set; }
        public float? f_nc_nghiphep_khl { get; set; }
        public float? g_nc_nghile { get; set; }

        public string h_ltg_ngaythuong_bd { get; set; }
        public string i_ltg_ngaythuong_kt { get; set; }
        public float? j_ltg_ngaythuong_sogio { get; set; }
        public string k_ltg_ngaythuong_noidung { get; set; }

        public string l_ltg_ngayle_bd { get; set; }
        public string m_ltg_ngayle_kt { get; set; }
        public float? n_ltg_ngayle_sogio { get; set; }
        public string o_ltg_ngayle_noidung { get; set; }

        public string p_ltg_ngaynghi_bd { get; set; }
        public string q_ltg_ngaynghi_kt { get; set; }
        public float? r_ltg_ngaynghi_sogio { get; set; }
        public string s_ltg_ngaynghi_noidung { get; set; }


        public float? t_DiTre { get; set; }
        public float? u_VeSom { get; set; }
        public float? v_vcn_sophut { get; set; }
        public string w_vcn_mota { get; set; }
        public IList<ViecBenNgoai> x_ViecBenNgoais { get; set; }
        public bool? y_isCuoiTuan { get; set; }
        public bool? z_isNgayLe { get; set; }

        public class ViecBenNgoai
        {
            //public DateTime? ThoiGianBatDau { get; set; }
            //public DateTime? ThoiGianKetThuc { get; set; }
            public string TGBD_Display { get; set; }
            public string TGKT_Display { get; set; }
            public string MoTa { get; set; }
            public string DiaDiem { get; set; }
        }
    }
}
