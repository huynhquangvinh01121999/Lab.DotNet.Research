USE [Esuhai.HRMDev]
GO

/****** Object:  StoredProcedure [Hrm].[SP_GetTongHopDuLieuNhanVien]    Script Date: 11/16/2022 5:19:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*==============================================================================================
   Author			Date			Updated					Description
   -----------------------------------------------------------------------------------------
   Vinh Huynh		2022.11.17		2022.12.12				Get thông tin tổng hợp dữ liệu của nhân viên
============================================================================================= */

CREATE procedure [Hrm].[SP_GetTongHopDuLieuNhanVien]
	@nhanVienId nvarchar(max),
	@thoiGian datetime
as
begin

	DECLARE @dateNow DATETIME
	SET @dateNow = GETDATE()

	-- giới hạn tối đa số ngày XD của NXD1
	declare @limitXD1 int
	set @limitXD1 = 7
	
	-- giới hạn tối đa số ngày XD của NXD2
	declare @limitXD2 int
	set @limitXD2 = 11
	
	select * into #tmpThdl
	from Hrm.TongHopDuLieus
	where NhanVienId = @nhanVienId and year(NgayLamViec) = year(@thoiGian) 
								   and month(NgayLamViec) = month(@thoiGian) 
								   and NgayLamViec <= getdate()

	select SoNgayDangKy, TrangThaiNghi into #tmpNp
	from Hrm.NghiPheps
	where NhanVienId = @nhanVienId
		and	year(@thoiGian) between year(ThoiGianBatDau) and year(ThoiGianKetThuc)
		and month(@thoiGian) between month(ThoiGianBatDau) and month(ThoiGianKetThuc) 

	select LoaiCongTac, SoGio into #tmpVbn
	from Hrm.ViecBenNgoais
	where NhanVienId = @nhanVienId
		and	year(@thoiGian) between year(ThoiGianBatDau) and year(ThoiGianKetThuc)
		and month(@thoiGian) between month(ThoiGianBatDau) and month(ThoiGianKetThuc) 

	declare @TongGioCong float = (select sum(Timesheet_GioCong) from #tmpThdl)
	declare @TongNgayCong float = (select sum(NgayCong) from #tmpThdl)
	declare @TongDiTre float = (select sum(DiTre) from #tmpThdl)
	declare @TongVeSom float = (select sum(VeSom) from #tmpThdl)
	declare @TongNghiPhepHopLe float = (select sum(case when TrangThaiNghi = N'Hợp lệ' then SoNgayDangKy else 0 end) from #tmpNp)
	declare @TongNghiPhepKhongHopLe float = (select sum(case when TrangThaiNghi = N'Không hợp lệ' then SoNgayDangKy else 0 end) from #tmpNp)
	declare @TongViecBenNgoaiCaNhan float = (select sum(case when LoaiCongTac = N'Cá nhân' then SoGio else 0 end) from #tmpVbn)
	declare @TongViecBenNgoaiCongTy float = (select sum(case when LoaiCongTac = N'Công ty' then SoGio else 0 end) from #tmpVbn)
	declare @ChamCongOnline bit = (select (case when ChamCongOnline = 1 then 1 else 0 end) from Hrm.NhanViens where Id = @nhanVienId)

	select
		(select round(@TongGioCong,1) as [TongGioCong]
			, @TongNgayCong  as [TongNgayCong]
			, @TongDiTre as [TongDiTre]
			, @TongVeSom as [TongVeSom]
			, @TongNghiPhepHopLe as [TongNghiPhepHopLe]
			, @TongNghiPhepKhongHopLe as [TongNghiPhepKhongHopLe]
			, @TongViecBenNgoaiCaNhan as [TongViecBenNgoaiCaNhan]
			, @TongViecBenNgoaiCongTy as [TongViecBenNgoaiCongTy]
			, @ChamCongOnline as [ChamCongOnline]

			,(select thdl.Id, concat(HoTenDemVN,' ',TenVN) as [HoTenNhanVien]
			
					, thdl.NhanVienId, thdl.NgayLamViec
					, thdl.CaLamViec_BatDau, thdl.CaLamViec_BatDauNghi, thdl.CaLamViec_KetThucNghi, thdl.CaLamViec_KetThuc
					, thdl.NgayCong, thdl.Final_GioCong
					, thdl.DiTre, thdl.VeSom
					--, thdl.Timesheet_GioVao as [Timesheet_GioVao]
					--, thdl.Timesheet_GioRa as [Timesheet_GioRa]

					, ts.GioVao as [Timesheet_GioVao]
					, ts.GioRa as [Timesheet_GioRa]
					, ts.DieuChinh_GioVao as [DieuChinh_GioVao]
					, ts.DieuChinh_GioRa as [DieuChinh_GioRa]
					, ts.DieuChinh_GhiChu as [DieuChinh_GhiChu]
					, ts.HR_TrangThai, ts.HR_GioVao, ts.HR_GioRa
					, DATEDIFF(DAY, thdl.NgayLamViec, getdate()) as [SoNgay]
			
			
					, (
						select tc.ThoiGianBatDau, tc.ThoiGianKetThuc
							, tc.SoGioDangKy, tc.MoTa
						from Hrm.TangCas tc
						where tc.NhanVienId = nv.Id and convert(date, tc.NgayTangCa) = convert(date, thdl.NgayLamViec)
						order by tc.ThoiGianBatDau
						FOR JSON PATH
					) as [TangCas]
			
					, (
						select pc.ThoiGianBatDau, pc.ThoiGianKetThuc, pc.MoTa, lpc.Ten as [LoaiPhuCapTen], lpc.TenJP as [LoaiPhuCapTenJP]
						from Hrm.PhuCaps pc
							inner join LoaiPhuCaps lpc on pc.LoaiPhuCapId = lpc.Id
						where pc.NhanVienId = nv.Id and thdl.NgayLamViec between CONVERT(NVARCHAR(10), pc.ThoiGianBatDau, 25) and CONVERT(NVARCHAR(10), pc.ThoiGianKetThuc, 25)
						order by pc.ThoiGianBatDau
						FOR JSON PATH
					) as [PhuCaps]

					,(
						SELECT np.ThoiGianBatDau, np.ThoiGianKetThuc, np.SoNgayDangKy as [SoNgayDangKy], np.TrangThaiNghi, np.TrangThaiDangKy, np.MoTa
							, (case
								when np.ThoiGianBatDau between thdl.CaLamViec_BatDau and thdl.CaLamViec_BatDauNghi then np.ThoiGianBatDau
								when np.ThoiGianBatDau between thdl.CaLamViec_KetThucNghi and thdl.CaLamViec_KetThuc then thdl.CaLamViec_KetThucNghi
								when np.ThoiGianBatDau < thdl.CaLamViec_BatDau then thdl.CaLamViec_BatDau
							end) as [TGBD_Display]
							, (case
								when np.ThoiGianKetThuc between thdl.CaLamViec_BatDau and thdl.CaLamViec_BatDauNghi then thdl.CaLamViec_BatDauNghi
								when np.ThoiGianKetThuc between thdl.CaLamViec_KetThucNghi and thdl.CaLamViec_KetThuc then np.ThoiGianKetThuc
								when np.ThoiGianKetThuc > thdl.CaLamViec_KetThuc then thdl.CaLamViec_KetThuc
							end) as [TGKT_Display]
						FROM Hrm.NghiPheps np
						where np.NhanVienId = thdl.NhanVienId
							AND np.TrangThaiDangKy <> N'Không nghỉ'
							AND (thdl.NgayLamViec >= CONVERT(NVARCHAR(10), np.ThoiGianBatDau, 25)
							AND thdl.NgayLamViec <= CONVERT(NVARCHAR(10), np.ThoiGianKetThuc, 25))
						ORDER BY [TGBD_Display]
						FOR JSON PATH
					) as [NghiPheps]

					, 
					(	select vbn.ThoiGianBatDau, vbn.ThoiGianKetThuc
							, (case
									when thdl.NgayLamViec = CONVERT(NVARCHAR(10), vbn.ThoiGianBatDau, 25) then vbn.ThoiGianBatDau
									else thdl.CaLamViec_BatDau
							end) as [TGBD_Display]
							, (case
									when thdl.NgayLamViec = CONVERT(NVARCHAR(10), vbn.ThoiGianKetThuc, 25) then vbn.ThoiGianKetThuc
									else thdl.CaLamViec_KetThuc
							end) as [TGKT_Display]
							, vbn.SoGio, vbn.MoTa
							, vbn.LoaiCongTac
						from hrm.ViecBenNgoais vbn
						where vbn.NhanVienId = thdl.NhanVienId
							AND (thdl.NgayLamViec >= CONVERT(NVARCHAR(10), vbn.ThoiGianBatDau, 25)
							AND thdl.NgayLamViec <= CONVERT(NVARCHAR(10), vbn.ThoiGianKetThuc, 25))
							AND vbn.TrangThaiXetDuyet = N'approved'
						ORDER BY [TGBD_Display]
						FOR JSON PATH
					) as [ViecBenNgoais]

					, (case
							when thdl.isNgayLe = 1 then N'd-le'
							when DATEPART(W, thdl.NgayLamViec) = 1 then N'd-cn'
							when DATEPART(W, thdl.NgayLamViec) < 7 and thdl.NgayCong = 1 then N'd-ok'
							when DATEPART(W, thdl.NgayLamViec) = 7 and thdl.NgayCong > 0 then N'd-t7'
							else N'd-mi'
						end) as TrangThai
					, ts.Id as [TimesheetId]
					, ts.TrangThai as [TimesheetTrangThai]
				from Hrm.NhanViens nv
				left join Hrm.TongHopDuLieus thdl on thdl.NhanVienId = nv.Id
				left join Hrm.Timesheets ts on ts.NhanVienId = nv.Id and ts.NgayLamViec = thdl.NgayLamViec
				where 
						year(thdl.NgayLamViec) = year(@thoiGian)
					and
						month(thdl.NgayLamViec) = month(@thoiGian)
					and
						thdl.NgayLamViec <= getdate()
					and
						nv.Id = @nhanVienId
				order by NgayLamViec desc
				for json path) as [Timesheets]
		for json path
	) as [Result]

	DROP TABLE IF EXISTS #tmpThdl
	DROP TABLE IF EXISTS #tmpNp
	DROP TABLE IF EXISTS #tmpVbn
end
GO


