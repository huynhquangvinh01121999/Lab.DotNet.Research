USE [DevEsuhaiDb]
GO

/****** Object:  StoredProcedure [Hrm].[SP_ExportExcel_TongHopNgayCong]    Script Date: 2/3/2023 10:50:17 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [Hrm].[SP_ExportExcel_TongHopNgayCong]
	@nhanVienId uniqueidentifier,
	@thang int,
	@nam int
as
begin
	select(
		select thdl.NgayLamViec as [a_Ngay], isnull(thdl.NgayCong, 0) as [b_NgayCong]
			, convert(varchar(5), thdl.Timesheet_GioVao, 108) as [c_GioVao]
			, convert(varchar(5), thdl.Timesheet_GioRa, 108) as [d_GioRa]

			--, thdl.NghiPhep1Id, thdl.NghiPhep1ThoiGianBatDau, thdl.NghiPhep1ThoiGianKetThuc, thdl.NghiPhep1TrangThaiNghi
			--, thdl.NghiPhep2Id, thdl.NghiPhep2ThoiGianBatDau, thdl.NghiPhep2ThoiGianKetThuc, thdl.NghiPhep2TrangThaiNghi
			, (case
					when thdl.isNgayLe <> 1
						and thdl.NghiPhep1Id is not null
						and thdl.NghiPhep2Id is null
						and thdl.NghiPhep1TrangThaiNghi = N'Hợp lệ' 
					then 0.5
					when thdl.isNgayLe <> 1
						and thdl.NghiPhep2Id is not null
						and thdl.NghiPhep1Id is null
						and thdl.NghiPhep2TrangThaiNghi = N'Hợp lệ' 
					then 0.5
					when thdl.isNgayLe <> 1
						and thdl.NghiPhep1Id is not null
						and thdl.NghiPhep2Id is not null
						and thdl.NghiPhep1TrangThaiNghi = N'Hợp lệ'
						and thdl.NghiPhep2TrangThaiNghi = N'Hợp lệ'
					then 1
					when thdl.isNgayLe <> 1
						and thdl.NghiPhep1Id is not null
						and thdl.NghiPhep2Id is not null
						and thdl.NghiPhep1TrangThaiNghi = N'Hợp lệ'
						and thdl.NghiPhep2TrangThaiNghi = N'Không hợp lệ'
					then 0.5
					when thdl.isNgayLe <> 1
						and thdl.NghiPhep1Id is not null
						and thdl.NghiPhep2Id is not null
						and thdl.NghiPhep1TrangThaiNghi = N'Không Hợp lệ'
						and thdl.NghiPhep2TrangThaiNghi = N'Hợp lệ'
					then 0.5
					else 0
				end) as [e_nc_nghiphep_hl]

					, (case
					when thdl.isNgayLe <> 1
						and thdl.NghiPhep1Id is not null
						and thdl.NghiPhep2Id is null
						and thdl.NghiPhep1TrangThaiNghi = N'Không hợp lệ' 
					then 0.5
					when thdl.isNgayLe <> 1
						and thdl.NghiPhep2Id is not null
						and thdl.NghiPhep1Id is null
						and thdl.NghiPhep2TrangThaiNghi = N'Không hợp lệ' 
					then 0.5
					when thdl.isNgayLe <> 1
						and thdl.NghiPhep1Id is not null
						and thdl.NghiPhep2Id is not null
						and thdl.NghiPhep1TrangThaiNghi = N'Không hợp lệ'
						and thdl.NghiPhep2TrangThaiNghi = N'Không hợp lệ'
					then 1
					when thdl.isNgayLe <> 1
						and thdl.NghiPhep1Id is not null
						and thdl.NghiPhep2Id is not null
						and thdl.NghiPhep1TrangThaiNghi = N'Hợp lệ'
						and thdl.NghiPhep2TrangThaiNghi = N'Không hợp lệ'
					then 0.5
					when thdl.isNgayLe <> 1
						and thdl.NghiPhep1Id is not null
						and thdl.NghiPhep2Id is not null
						and thdl.NghiPhep1TrangThaiNghi = N'Không Hợp lệ'
						and thdl.NghiPhep2TrangThaiNghi = N'Hợp lệ'
					then 0.5
					else 0
				end) as [f_nc_nghiphep_khl]

				, (case when thdl.isNgayLe = 1 and thdl.isCuoiTuan <> 1 then NgayCong else 0 end) as [g_nc_nghile]

				, (case when thdl.isNgayLe <> 1 and thdl.isCuoiTuan <> 1 then convert(varchar(5), tc.ThoiGianBatDau, 108) end) as [h_ltg_ngaythuong_bd]
				, (case when thdl.isNgayLe <> 1 and thdl.isCuoiTuan <> 1 then convert(varchar(5), tc.ThoiGianKetThuc, 108) end) as [i_ltg_ngaythuong_kt]
				, (case when thdl.isNgayLe <> 1 and thdl.isCuoiTuan <> 1 then SoGioDuocDuyet end) as [j_ltg_ngaythuong_sogio]
				, (case when thdl.isNgayLe <> 1 and thdl.isCuoiTuan <> 1 then MoTa end) as [k_ltg_ngaythuong_noidung]

				, (case when thdl.isNgayLe = 1 then convert(varchar(5), tc.ThoiGianBatDau, 108) end) as [l_ltg_ngayle_bd]
				, (case when thdl.isNgayLe = 1 then convert(varchar(5), tc.ThoiGianKetThuc, 108) end) as [m_ltg_ngayle_kt]
				, (case when thdl.isNgayLe = 1 then SoGioDuocDuyet end) as [n_ltg_ngayle_sogio]
				, (case when thdl.isNgayLe = 1 then MoTa end) as [o_ltg_ngayle_noidung]
		
				, (case when thdl.isCuoiTuan = 1 then convert(varchar(5), tc.ThoiGianBatDau, 108) end) as [p_ltg_ngaynghi_bd]
				, (case when thdl.isCuoiTuan = 1 then convert(varchar(5), tc.ThoiGianKetThuc, 108) end) as [q_ltg_ngaynghi_kt]
				, (case when thdl.isCuoiTuan = 1 then SoGioDuocDuyet end) as [r_ltg_ngaynghi_sogio]
				, (case when thdl.isCuoiTuan = 1 then MoTa end) as [s_ltg_ngaynghi_noidung]

				, (case when thdl.DiTre <> 0 then DiTre end) as [t_DiTre]
				, (case when thdl.VeSom <> 0 then VeSom end) as [u_VeSom]

				, (select vbn.SoGio * 60 from Hrm.ViecBenNgoais vbn 
					where vbn.NhanVienId = thdl.NhanVienId
					and thdl.NgayLamViec between cast(vbn.ThoiGianBatDau as date) and cast(vbn.ThoiGianKetThuc as date)
					and vbn.LoaiCongTac = N'Cá nhân'
				) as [v_vcn_sophut]
			
				, (select vbn.MoTa from Hrm.ViecBenNgoais vbn 
					where vbn.NhanVienId = thdl.NhanVienId
					and thdl.NgayLamViec between cast(vbn.ThoiGianBatDau as date) and cast(vbn.ThoiGianKetThuc as date)
					and vbn.LoaiCongTac = N'Cá nhân'
				) as [w_vcn_mota]

				, (	select vbn.ThoiGianBatDau, vbn.ThoiGianKetThuc
						, (case
								when NgayLamViec = cast(vbn.ThoiGianBatDau as date) then convert(varchar(5), vbn.ThoiGianBatDau, 108)
								else convert(varchar(5), thdl.CaLamViec_BatDau, 108)
							end) as [TGBD_Display]
						, (case
								when NgayLamViec = cast(vbn.ThoiGianKetThuc as date) then convert(varchar(5), vbn.ThoiGianKetThuc, 108)
								else convert(varchar(5), thdl.CaLamViec_KetThuc, 108)
							end) as [TGKT_Display]
						, vbn.MoTa, vbn.DiaDiem
					from hrm.ViecBenNgoais vbn
					where vbn.NhanVienId = thdl.NhanVienId
						and thdl.NgayLamViec between cast(vbn.ThoiGianBatDau as date) and cast(vbn.ThoiGianKetThuc as date)
						--and (NgayLamViec >= CONVERT(NVARCHAR(10), vbn.ThoiGianBatDau, 25)
						--and NgayLamViec <= CONVERT(NVARCHAR(10), vbn.ThoiGianKetThuc, 25))
						and vbn.TrangThaiXetDuyet = 'approved'
						and vbn.LoaiCongTac = N'Công ty'
					FOR JSON PATH
				) as [x_ViecBenNgoais]
				, thdl.isCuoiTuan as [y_isCuoiTuan]
				, thdl.isNgayLe as [z_isNgayLe]
		from Hrm.TongHopDuLieus thdl
		left join Hrm.TangCas tc on tc.NhanVienId = thdl.NhanVienId and cast(tc.NgayTangCa as date) = cast(thdl.NgayLamViec as date)
		where thdl.NhanVienId = @nhanVienId
			and year(thdl.NgayLamViec) = @nam
			and month(thdl.NgayLamViec) = @thang
		order by thdl.NgayLamViec
		for json path
	) as [Result]
end
GO


